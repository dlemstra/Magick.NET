//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class CppGenerator
	{
		//===========================================================================================
		private MagickNET _MagickNET;
		//===========================================================================================
		private CppGenerator()
		{
			_MagickNET = new MagickNET(QuantumDepth.Q16);
		}
		//===========================================================================================
		private static void AddMethod(IndentedTextWriter writer, string hashtableName, string xsdName, string delegateName)
		{
			writer.Write(hashtableName);
			writer.Write("[\"");
			writer.Write(xsdName);
			writer.Write("\"] = gcnew ");
			writer.Write(delegateName);
			writer.WriteLine(";");
		}
		//===========================================================================================
		private void CreateCodeFile()
		{
			using (IndentedTextWriter writer = CreateWriter(@"Execute.cpp"))
			{
				WriteHeader(writer);
				writer.WriteLine(@"#include ""Stdafx.h""");
				writer.WriteLine(@"#include ""..\..\Helpers\XmlHelper.h""");
				writer.WriteLine(@"#include ""..\MagickScript.h""");
				writer.WriteLine("namespace ImageMagick");
				writer.WriteLine("{");
				writer.Indent++;
				WriteInitializeMethods(writer);
				WriteExecuteMethods(writer);
				writer.Indent--;
				writer.WriteLine("}");

				writer.InnerWriter.Dispose();
			}
		}
		//===========================================================================================
		private void CreateHeaderFile()
		{
			using (IndentedTextWriter writer = CreateWriter(@"Execute.h"))
			{
				WriteHeader(writer);

				foreach (PropertyInfo property in _MagickNET.GetProperties("MagickImage"))
				{
					WriteProperty(writer, property);
				}

				foreach (MethodInfo[] overloads in _MagickNET.GetGroupedMethods("MagickImage"))
				{
					WriteMethod(writer, overloads);
				}

				writer.InnerWriter.Dispose();
			}
		}
		//===========================================================================================
		private static IndentedTextWriter CreateWriter(string fileName)
		{
			string outputFile = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET\Script\Generated\" + fileName);
			Console.WriteLine("Creating: " + outputFile);

			FileStream output = File.Create(outputFile);
			StreamWriter streamWriter = new StreamWriter(output);
			IndentedTextWriter writer = new IndentedTextWriter(streamWriter, "\t");
			return writer;
		}
		//===========================================================================================
		private static bool IsStatic(MethodInfo[] overloads)
		{
			return !overloads.Any(method => method.GetParameters().Any(parameter => MagickNET.GetTypeName(parameter) == "MagickImage^"));
		}
		//===========================================================================================
		private static bool IsStatic(PropertyInfo property)
		{
			return MagickNET.GetTypeName(property) != "MagickImage^";
		}
		//===========================================================================================
		private static bool UsesElement(MethodInfo[] overloads)
		{
			return (overloads.Length != 1 || overloads[0].GetParameters().Count() > 0);
		}
		//===========================================================================================
		private static void WriteAttributeForEach(IndentedTextWriter writer, ParameterInfo[] allParameters)
		{
			ParameterInfo[] parameters = allParameters.Where(p => XsdGenerator.GetXsdAttributeType(p) != null).ToArray();
			if (parameters.Length == 0)
				return;

			writer.WriteLine("for each(XmlAttribute^ attribute in element->Attributes)");
			writer.WriteLine("{");
			writer.Indent++;

			if (parameters.DistinctBy(p => MagickNET.GetTypeName(p)).Count() == 1)
			{
				writer.Write("arguments[attribute->Name] = XmlHelper::GetValue<");
				writer.Write(MagickNET.GetTypeName(parameters[0]));
				writer.WriteLine(">(attribute);");
			}
			else
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					string xsdName = XsdGenerator.GetXsdName(parameters[i]);

					if (i > 0)
						writer.Write("else ");

					writer.Write("if (attribute->Name == \"");
					writer.Write(xsdName);
					writer.WriteLine("\")");
					writer.Indent++;
					writer.Write("arguments[\"");
					writer.Write(xsdName);
					writer.Write("\"] = ");
					WriteGetAttributeValue(writer, MagickNET.GetTypeName(parameters[i]), xsdName);
					writer.Indent--;
				}
			}

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private static void WriteElementForEach(IndentedTextWriter writer, ParameterInfo[] allParameters)
		{
			ParameterInfo[] parameters = allParameters.Where(p => XsdGenerator.GetXsdAttributeType(p) == null).ToArray();
			if (parameters.Length == 0)
				return;

			writer.WriteLine("for each(XmlElement^ elem in element->SelectNodes(\"*\"))");
			writer.WriteLine("{");
			writer.Indent++;

			if (parameters.DistinctBy(p => MagickNET.GetTypeName(p)).Count() == 1)
			{
				writer.Write("arguments[elem->Name] = ");
				WriteGetElementValue(writer, MagickNET.GetTypeName(parameters[0]), "elem");
			}
			else
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					string xsdName = XsdGenerator.GetXsdName(parameters[i]);

					if (i > 0)
						writer.Write("else ");

					writer.Write("if (elem->Name == \"");
					writer.Write(xsdName);
					writer.WriteLine("\")");
					writer.Indent++;
					writer.Write("arguments[\"");
					writer.Write(xsdName);
					writer.Write("\"] = ");
					WriteGetElementValue(writer, MagickNET.GetTypeName(parameters[i]), "elem");
					writer.Indent--;
				}
			}

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private void WriteExecuteMethods(IndentedTextWriter writer)
		{
			foreach (PropertyInfo property in _MagickNET.GetProperties("MagickImage"))
			{
				WriteExecute(writer, property);
			}

			foreach (MethodInfo[] overloads in _MagickNET.GetGroupedMethods("MagickImage"))
			{
				WriteExecute(writer, overloads);
			}
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, PropertyInfo property)
		{
			writer.Write("void MagickScript::Execute");
			writer.Write(property.Name);
			writer.WriteLine("(XmlElement^ element, MagickImage^ image)");
			writer.WriteLine("{");
			writer.Indent++;
			writer.Write("image->");
			writer.Write(property.Name);
			writer.Write(" = ");
			WriteGetValue(writer, property);
			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, MethodInfo[] overloads)
		{
			writer.Write("void MagickScript::Execute");
			writer.Write(overloads[0].Name);
			writer.Write("(");

			if (UsesElement(overloads))
				writer.Write("XmlElement^ element, ");

			writer.WriteLine("MagickImage^ image)");
			writer.WriteLine("{");
			writer.Indent++;

			ParameterInfo[] parameters = (from method in overloads
													from param in method.GetParameters()
													select param).DistinctBy(p => p.Name).ToArray();

			if (overloads.Length == 1)
				WriteMethod(writer, overloads[0], parameters);
			else
				WriteMethod(writer, overloads, parameters);

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private static void WriteExecuteIfElse(IndentedTextWriter writer, MethodInfo[] overloads)
		{
			for (int i = 0; i < overloads.Length; i++)
			{
				ParameterInfo[] parameters = overloads[i].GetParameters();

				if (i > 0)
					writer.Write("else ");

				writer.Write("if (");
				if (parameters.Length > 0)
				{
					writer.Write("OnlyContains(arguments");
					foreach (ParameterInfo parameter in parameters)
					{
						writer.Write(", \"");
						writer.Write(parameter.Name);
						writer.Write("\"");
					}
					writer.Write(")");
				}
				else
				{
					writer.Write("arguments->Count == 0");
				}

				writer.WriteLine(")");
				writer.Indent++;
				writer.Write("image->");
				writer.Write(overloads[i].Name);
				writer.Write("(");

				for (int k = 0; k < parameters.Length; k++)
				{
					writer.Write("(");
					writer.Write(MagickNET.GetTypeName(parameters[k]));
					writer.Write(")arguments[\"");
					writer.Write(parameters[k].Name);
					writer.Write("\"]");

					if (k != parameters.Length - 1)
						writer.Write(", ");
				}
				writer.WriteLine(");");
				writer.Indent--;
			}
		}
		//===========================================================================================
		private static void WriteGetAttributeValue(IndentedTextWriter writer, string typeName, string attributeName)
		{
			writer.Write("XmlHelper::GetAttribute<");
			writer.Write(typeName);
			writer.Write(">(element, \"");
			writer.Write(attributeName);
			writer.WriteLine("\");");
		}
		//===========================================================================================
		private static void WriteGetElementValue(IndentedTextWriter writer, string typeName, string elementName)
		{
			switch (typeName)
			{
				case "MagickImage^":
					writer.Write("CreateMagickImage(");
					if (!string.IsNullOrEmpty(elementName))
						writer.Write(elementName);
					else
						writer.Write("(XmlElement^)element->SelectSingleNode(\"read\")");
					writer.WriteLine(");");
					break;
				case "MagickGeometry^":
					writer.Write("CreateMagickGeometry(");
					if (!string.IsNullOrEmpty(elementName))
						writer.Write(elementName);
					else
						writer.Write("(XmlElement^)element->SelectSingleNode(\"geometry\")");
					writer.WriteLine(");");
					break;
				default:
					throw new NotImplementedException(typeName);
			}
		}
		//===========================================================================================
		private static void WriteGetValue(IndentedTextWriter writer, PropertyInfo property)
		{
			string typeName = MagickNET.GetTypeName(property);
			string xsdTypeName = XsdGenerator.GetXsdAttributeType(property);

			WriteGetValue(writer, typeName, xsdTypeName, "value");
		}
		//===========================================================================================
		private static void WriteGetValue(IndentedTextWriter writer, string typeName, string xsdTypeName, string attributeName)
		{
			if (xsdTypeName != null)
			{
				WriteGetAttributeValue(writer, typeName, attributeName);
				return;
			}

			WriteGetElementValue(writer, typeName, null);
		}
		//===========================================================================================
		private static void WriteHeader(IndentedTextWriter writer)
		{
			writer.WriteLine("//=================================================================================================");
			writer.WriteLine("// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>");
			writer.WriteLine("//");
			writer.WriteLine("// Licensed under the ImageMagick License (the \"License\"); you may not use this file except in");
			writer.WriteLine("// compliance with the License. You may obtain a copy of the License at");
			writer.WriteLine("//");
			writer.WriteLine("//   http://www.imagemagick.org/script/license.php");
			writer.WriteLine("//");
			writer.WriteLine("// Unless required by applicable law or agreed to in writing, software distributed under the");
			writer.WriteLine("// License is distributed on an \"AS IS\" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either");
			writer.WriteLine("// express or implied. See the License for the specific language governing permissions and");
			writer.WriteLine("// limitations under the License.");
			writer.WriteLine("//=================================================================================================");
		}
		//===========================================================================================
		private void WriteInitializeMethods(IndentedTextWriter writer)
		{
			writer.WriteLine("void MagickScript::InitializeExecuteMethods()");
			writer.WriteLine("{");
			writer.Indent++;
			writer.WriteLine("_ExecuteMethods = gcnew Hashtable();");
			WriteExecuteMethods(writer, "_ExecuteMethods", false);
			AddMethod(writer, "_ExecuteMethods", "copy", "ExecuteElementImage(this, &MagickScript::ExecuteCopy)");
			AddMethod(writer, "_ExecuteMethods", "write", "ExecuteElementImage(this, &MagickScript::ExecuteWrite)");
			writer.Indent--;
			writer.WriteLine("}");
			writer.WriteLine("Hashtable^ MagickScript::InitializeStaticExecuteMethods()");
			writer.WriteLine("{");
			writer.Indent++;
			writer.WriteLine("Hashtable^ result = gcnew Hashtable();");
			WriteExecuteMethods(writer, "result", true);
			writer.WriteLine("return result;");
			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private static void WriteInvalidCombinations(IndentedTextWriter writer, MethodInfo[] overloads)
		{
			writer.WriteLine("else");
			writer.Indent++;
			writer.Write("throw gcnew ArgumentException(\"Invalid argument combination for '" + overloads[0].Name + "', allowed combinations are:");
			foreach (MethodInfo method in overloads)
			{
				writer.Write(" [");
				ParameterInfo[] parameters = method.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					writer.Write(parameters[i].Name);
					if (i != parameters.Length - 1)
						writer.Write(", ");
				}
				writer.Write("]");
			}
			writer.WriteLine("\");");
			writer.Indent--;
		}
		//===========================================================================================
		private static void WriteMethod(IndentedTextWriter writer, MethodInfo[] overloads, ParameterInfo[] parameters)
		{
			writer.WriteLine("Hashtable^ arguments = gcnew Hashtable();");

			WriteAttributeForEach(writer, parameters);
			WriteElementForEach(writer, parameters);
			WriteExecuteIfElse(writer, overloads);
			WriteInvalidCombinations(writer, overloads);
		}
		//===========================================================================================
		private static void WriteMethod(IndentedTextWriter writer, MethodInfo methodInfo, ParameterInfo[] parameters)
		{
			foreach (ParameterInfo parameter in parameters)
			{
				string typeName = MagickNET.GetTypeName(parameter);

				writer.Write(typeName);
				writer.Write(" ");
				writer.Write(parameter.Name);
				writer.Write("_ = ");
				WriteGetValue(writer, typeName, XsdGenerator.GetXsdName(parameter), parameter.Name);
			}

			writer.Write("image->");
			writer.Write(methodInfo.Name);
			writer.Write("(");
			for (int i = 0; i < parameters.Length; i++)
			{
				writer.Write(parameters[i].Name);
				writer.Write("_");

				if (i != parameters.Length - 1)
					writer.Write(", ");
			}

			writer.WriteLine(");");
		}
		//===========================================================================================
		private void WriteExecuteMethods(IndentedTextWriter writer, string hashtableName, bool isHashTableStatic)
		{
			string delegateName = "Execute{0}(";
			if (!isHashTableStatic)
				delegateName += "this, &";
			delegateName += "MagickScript::Execute{1})";

			foreach (PropertyInfo property in _MagickNET.GetProperties("MagickImage"))
			{
				bool isStatic = IsStatic(property);
				if (isStatic != isHashTableStatic)
					continue;

				string propertyDelegate = string.Format(delegateName, "ElementImage", property.Name);

				AddMethod(writer, hashtableName, XsdGenerator.GetXsdName(property), propertyDelegate);
			}

			foreach (MethodInfo[] overloads in _MagickNET.GetGroupedMethods("MagickImage"))
			{
				bool isStatic = IsStatic(overloads);
				if (isStatic != isHashTableStatic)
					continue;

				string args = !UsesElement(overloads) ? "Image" : "ElementImage";
				string methodDelegate = string.Format(delegateName, args, overloads[0].Name);

				AddMethod(writer, hashtableName, XsdGenerator.GetXsdName(overloads[0]), methodDelegate);
			}
		}
		//===========================================================================================
		private static void WriteMethod(IndentedTextWriter writer, MethodInfo[] overloads)
		{
			if (IsStatic(overloads))
				writer.Write("static ");

			writer.Write("void Execute");
			writer.Write(overloads[0].Name);
			writer.Write("(");

			if (UsesElement(overloads))
				writer.Write("XmlElement^ element, ");

			writer.WriteLine("MagickImage^ image);");
		}
		//===========================================================================================
		private static void WriteProperty(IndentedTextWriter writer, PropertyInfo property)
		{
			if (IsStatic(property))
				writer.Write("static ");

			writer.Write("void Execute");
			writer.Write(property.Name);
			writer.WriteLine("(XmlElement^ element, MagickImage^ image);");
		}
		//===========================================================================================
		public static void Generate()
		{
			CppGenerator generator = new CppGenerator();
			generator.CreateHeaderFile();
			generator.CreateCodeFile();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
