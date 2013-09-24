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
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal abstract class CodeGenerator
	{
		//===========================================================================================
		private MagickNET _MagickNET;
		//===========================================================================================
		private static void WriteAttributeForEach(IndentedTextWriter writer, ParameterInfo[] allParameters)
		{
			ParameterInfo[] parameters = allParameters.Where(p => XsdGenerator.GetAttributeType(p) != null).ToArray();
			if (parameters.Length == 0)
				return;

			parameters = parameters.OrderBy(p => p.Name).ToArray();

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
					string xsdName = XsdGenerator.GetName(parameters[i]);

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
		private void WriteCallIfElse(IndentedTextWriter writer, MethodBase[] methods)
		{
			for (int i = 0; i < methods.Length; i++)
			{
				ParameterInfo[] parameters = methods[i].GetParameters();

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
				WriteHashtableCall(writer, methods[i], parameters);
				writer.Indent--;
			}
		}
		//===========================================================================================
		private static void WriteElementForEach(IndentedTextWriter writer, ParameterInfo[] allParameters)
		{
			ParameterInfo[] parameters = allParameters.Where(p => XsdGenerator.GetAttributeType(p) == null).ToArray();
			if (parameters.Length == 0)
				return;

			writer.WriteLine("for each(XmlElement^ elem in element->SelectNodes(\"*\"))");
			writer.WriteLine("{");
			writer.Indent++;

			if (parameters.DistinctBy(p => MagickNET.GetTypeName(p)).Count() == 1)
			{
				writer.Write("arguments[elem->Name] = ");
				WriteCreateMethod(writer, MagickNET.GetTypeName(parameters[0]));
				writer.WriteLine("(elem);");
			}
			else
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					string xsdName = XsdGenerator.GetName(parameters[i]);

					if (i > 0)
						writer.Write("else ");

					writer.Write("if (elem->Name == \"");
					writer.Write(xsdName);
					writer.WriteLine("\")");
					writer.Indent++;
					writer.Write("arguments[\"");
					writer.Write(xsdName);
					writer.Write("\"] = ");
					WriteCreateMethod(writer, MagickNET.GetTypeName(parameters[i]));
					writer.WriteLine("(elem);");
					writer.Indent--;
				}
			}

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private static void WriteGetValue(IndentedTextWriter writer, ParameterInfo parameter)
		{
			string typeName = MagickNET.GetTypeName(parameter);
			string xsdTypeName = XsdGenerator.GetAttributeType(parameter);

			if (xsdTypeName != null)
			{
				WriteGetAttributeValue(writer, typeName, parameter.Name);
			}
			else
			{
				WriteCreateMethod(writer, typeName);
				writer.Write("(");
				WriteSelectElement(writer, typeName, parameter.Name);
				writer.WriteLine(");");
			}
		}
		//===========================================================================================
		protected static void WriteHashtableParameters(IndentedTextWriter writer, ParameterInfo[] parameters)
		{
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
		}
		//===========================================================================================
		private static void WriteInvalidCombinations(IndentedTextWriter writer, MethodBase[] methods)
		{
			writer.WriteLine("else");
			writer.Indent++;
			writer.Write("throw gcnew ArgumentException(\"Invalid argument combination for '" + XsdGenerator.GetName(methods[0]) + "', allowed combinations are:");
			foreach (MethodBase method in methods)
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
		private void WriteMethod(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			foreach (ParameterInfo parameter in parameters)
			{
				string typeName = MagickNET.GetTypeName(parameter);

				writer.Write(typeName);
				writer.Write(" ");
				writer.Write(parameter.Name);
				writer.Write("_ = ");
				WriteGetValue(writer, parameter);
			}

			WriteCall(writer, method, parameters);
		}
		//===========================================================================================
		private void WriteMethod(IndentedTextWriter writer, MethodBase[] method, ParameterInfo[] parameters)
		{
			writer.WriteLine("System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();");

			WriteAttributeForEach(writer, parameters);
			WriteElementForEach(writer, parameters);
			WriteCallIfElse(writer, method);
			WriteInvalidCombinations(writer, method);
		}
		//===========================================================================================
		protected static void WriteParameters(IndentedTextWriter writer, ParameterInfo[] parameters)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				writer.Write(parameters[i].Name);
				writer.Write("_");

				if (i != parameters.Length - 1)
					writer.Write(", ");
			}
		}
		//===========================================================================================
		protected CodeGenerator()
		{
			_MagickNET = new MagickNET(QuantumDepth.Q16);
		}
		//===========================================================================================
		protected MagickNET MagickNET
		{
			get
			{
				return _MagickNET;
			}
		}
		//===========================================================================================
		protected abstract void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters);
		//===========================================================================================
		protected abstract void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters);
		//===========================================================================================
		protected static bool IsStatic(MethodBase[] methods)
		{
			return !methods.Any(method => method.GetParameters().Any(parameter => MagickNET.GetTypeName(parameter) == "MagickImage^"));
		}
		//===========================================================================================
		protected static void WriteCreateMethod(IndentedTextWriter writer, string typeName)
		{
			switch (typeName)
			{
				case "Coordinate":
					writer.Write("CreateCoordinate");
					break;
				case "PathArc^":
					writer.Write("CreateArc");
					break;
				case "IEnumerable<Coordinate>^":
					writer.Write("CreateCoordinates");
					break;
				case "IEnumerable<PathBase^>^":
					writer.Write("CreatePaths");
					break;
				case "IEnumerable<PathArc^>^":
					writer.Write("CreatePathArcs");
					break;
				case "IEnumerable<PathCurveto^>^":
					writer.Write("CreatePathCurvetos");
					break;
				case "IEnumerable<PathQuadraticCurveto^>^":
					writer.Write("CreatePathQuadraticCurvetos");
					break;
				case "ImageProfile^":
					writer.Write("CreateProfile");
					break;
				case "MagickImage^":
					writer.Write("CreateMagickImage");
					break;
				case "MagickGeometry^":
					writer.Write("CreateMagickGeometry");
					break;
				case "PixelStorageSettings^":
					writer.Write("CreatePixelStorageSettings");
					break;
				default:
					throw new NotImplementedException(typeName);
			}
		}
		//===========================================================================================
		protected static void WriteGetAttributeValue(IndentedTextWriter writer, string typeName, string attributeName)
		{
			writer.Write("XmlHelper::GetAttribute<");
			writer.Write(typeName);
			writer.Write(">(element, \"");
			writer.Write(attributeName);
			writer.WriteLine("\");");
		}
		//===========================================================================================
		protected void WriteMethod(IndentedTextWriter writer, MethodBase[] methods)
		{
			ParameterInfo[] parameters = (from method in methods
													from param in method.GetParameters()
													select param).DistinctBy(p => p.Name).ToArray();

			if (methods.Length == 1)
				WriteMethod(writer, methods[0], parameters);
			else
				WriteMethod(writer, methods, parameters);
		}
		//===========================================================================================
		protected static void WriteSelectElement(IndentedTextWriter writer, string typeName, string elementName)
		{
			switch (typeName)
			{
				case "IEnumerable<Coordinate>^":
				case "IEnumerable<Drawable^>^":
				case "IEnumerable<PathBase^>^":
				case "IEnumerable<PathArc^>^":
				case "IEnumerable<PathCurveto^>^":
				case "IEnumerable<PathQuadraticCurveto^>^":
				case "ImageProfile^":
					writer.Write("element");
					break;
				case "MagickImage^":
					writer.Write("(XmlElement^)element->SelectSingleNode(\"");
					if (string.IsNullOrEmpty(elementName))
						writer.Write("read");
					else
						writer.Write(elementName);
					writer.Write("\")");
					break;
				case "MagickGeometry^":
					writer.Write("(XmlElement^)element->SelectSingleNode(\"");
					if (string.IsNullOrEmpty(elementName))
						writer.Write("geometry");
					else
						writer.Write(elementName);
					writer.Write("\")");
					break;
				case "PixelStorageSettings^":
					writer.Write("(XmlElement^)element->SelectSingleNode(\"");
					writer.Write(elementName);
					writer.Write("\")");
					break;
				default:
					throw new NotImplementedException(typeName);
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
