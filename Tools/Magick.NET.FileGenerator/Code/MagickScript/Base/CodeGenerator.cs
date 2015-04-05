//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
		private static void CheckDuplicateParameterNames(MethodBase[] methods)
		{
			int count = (from method in methods
							 let name = string.Join(",", from parameter in method.GetParameters()
																  orderby parameter.Name
																  select parameter.Name)
							 select name).Distinct().Count();

			if (count != methods.Length)
				throw new InvalidOperationException("Duplicate names detected for: " + methods[0].Name);
		}
		//===========================================================================================
		private void WriteAttributeForEach(IndentedTextWriter writer, ParameterInfo[] allParameters)
		{
			ParameterInfo[] parameters = allParameters.Where(p => MagickTypes.GetXsdAttributeType(p) != null).ToArray();
			if (parameters.Length == 0)
				return;

			parameters = parameters.OrderBy(p => p.Name).ToArray();

			writer.WriteLine("foreach (XmlAttribute attribute in element.Attributes)");
			WriteStartColon(writer);

			if (parameters.DistinctBy(p => GetName(p)).Count() == 1)
			{
				writer.Write("arguments[attribute.Name] = Variables.GetValue<");
				writer.Write(GetName(parameters[0]));
				writer.WriteLine(">(attribute);");
			}
			else
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					string xsdName = MagickTypes.GetXsdName(parameters[i]);

					if (i > 0)
						writer.Write("else ");

					writer.Write("if (attribute.Name == \"");
					writer.Write(xsdName);
					writer.WriteLine("\")");
					writer.Indent++;
					writer.Write("arguments[\"");
					writer.Write(xsdName);
					writer.Write("\"] = ");
					WriteGetAttributeValue(writer, GetName(parameters[i]));
					writer.Indent--;
				}
			}

			WriteEndColon(writer);
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
					writer.Write("arguments.Count == 0");
				}

				writer.WriteLine(")");
				writer.Indent++;
				WriteHashtableCall(writer, methods[i], parameters);
				writer.Indent--;
			}
		}
		//===========================================================================================
		private void WriteElementForEach(IndentedTextWriter writer, ParameterInfo[] allParameters)
		{
			ParameterInfo[] parameters = allParameters.Where(p => MagickTypes.GetXsdAttributeType(p) == null).ToArray();
			if (parameters.Length == 0)
				return;

			writer.WriteLine("foreach (XmlElement elem in element.SelectNodes(\"*\"))");
			WriteStartColon(writer);

			if (parameters.DistinctBy(p => GetName(p)).Count() == 1)
			{
				writer.Write("arguments[elem.Name] = ");
				WriteCreateMethod(writer, GetName(parameters[0]));
				writer.WriteLine("(elem);");
			}
			else
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					string xsdName = MagickTypes.GetXsdName(parameters[i]);

					if (i > 0)
						writer.Write("else ");

					writer.Write("if (elem.Name == \"");
					writer.Write(xsdName);
					writer.WriteLine("\")");
					writer.Indent++;
					writer.Write("arguments[\"");
					writer.Write(xsdName);
					writer.Write("\"] = ");
					WriteCreateMethod(writer, GetName(parameters[i]));
					writer.WriteLine("(elem);");
					writer.Indent--;
				}
			}

			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteGetValue(IndentedTextWriter writer, ParameterInfo parameter)
		{
			string typeName = GetName(parameter);
			string xsdTypeName = MagickTypes.GetXsdAttributeType(parameter);

			if (xsdTypeName != null)
			{
				WriteGetElementValue(writer, typeName, parameter.Name);
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
		private static void WriteHeader(IndentedTextWriter writer)
		{
			writer.WriteLine("//=================================================================================================");
			writer.WriteLine("// Copyright 2013-" + DateTime.Now.Year + " Dirk Lemstra <https://magick.codeplex.com/>");
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
		private static void WriteInvalidCombinations(IndentedTextWriter writer, MethodBase[] methods)
		{
			writer.WriteLine("else");
			writer.Indent++;
			writer.Write("throw new ArgumentException(\"Invalid argument combination for '" + MagickTypes.GetXsdName(methods[0]) + "', allowed combinations are:");
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
				string typeName = GetName(parameter);

				writer.Write(typeName);
				writer.Write(" ");
				writer.Write(parameter.Name);
				writer.Write("_ = ");
				WriteGetValue(writer, parameter);
			}

			WriteCall(writer, method, parameters);
		}
		//===========================================================================================
		private void WriteMethod(IndentedTextWriter writer, MethodBase[] methods, ParameterInfo[] parameters)
		{
			CheckDuplicateParameterNames(methods);

			writer.WriteLine("Hashtable arguments = new Hashtable();");

			WriteAttributeForEach(writer, parameters);
			WriteElementForEach(writer, parameters);
			WriteCallIfElse(writer, methods);
			WriteInvalidCombinations(writer, methods);
		}
		//===========================================================================================
		private static void WriteStartNamespace(IndentedTextWriter writer)
		{
			writer.WriteLine("namespace ImageMagick");
			WriteStartColon(writer);
		}
		//===========================================================================================
		private static void WriteUsing(IndentedTextWriter writer)
		{
			writer.WriteLine("");
			writer.WriteLine("using System;");
			writer.WriteLine("using System.Collections;");
			writer.WriteLine("using System.Collections.Generic;");
			writer.WriteLine("using System.Collections.ObjectModel;");
			writer.WriteLine("using System.Diagnostics.CodeAnalysis;");
			writer.WriteLine("using System.Text;");
			writer.WriteLine("using System.Xml;");
			writer.WriteLine("");
			writer.WriteLine("#if Q8");
			writer.WriteLine("using QuantumType = System.Byte;");
			writer.WriteLine("#elif Q16");
			writer.WriteLine("using QuantumType = System.UInt16;");
			writer.WriteLine("#elif Q16HDRI");
			writer.WriteLine("using QuantumType = System.Single;");
			writer.WriteLine("#else");
			writer.WriteLine("#error Not implemented!");
			writer.WriteLine("#endif");
			writer.WriteLine("");
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
		protected static void WriteSeparator(IndentedTextWriter writer)
		{
			int length = 98 - (writer.Indent * 3);
			writer.Write("//");
			writer.WriteLine(new string('=', length));
		}
		//===========================================================================================
		protected CodeGenerator()
		{
		}
		//===========================================================================================
		protected MagickTypes Types
		{
			get;
			private set;
		}
		//===========================================================================================
		protected static string GetName(MemberInfo member)
		{
			return MagickTypes.GetName(member);
		}
		//===========================================================================================
		protected string GetName(ParameterInfo parameterInfo)
		{
			return Types.GetName(parameterInfo.ParameterType);
		}
		//===========================================================================================
		protected string GetName(PropertyInfo propertyInfo)
		{
			return Types.GetName(propertyInfo.PropertyType);
		}
		//===========================================================================================
		protected abstract void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters);
		//===========================================================================================
		protected abstract void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters);
		//===========================================================================================
		protected static void CheckNull(IndentedTextWriter writer, string name)
		{
			writer.Write("if (");
			writer.Write(name);
			writer.WriteLine(" == null)");
			writer.Indent++;
			writer.WriteLine("return null;");
			writer.Indent--;
		}
		//===========================================================================================
		protected void WriteHashtableParameters(IndentedTextWriter writer, ParameterInfo[] parameters)
		{
			for (int k = 0; k < parameters.Length; k++)
			{
				writer.Write("(");
				writer.Write(GetName(parameters[k]));
				writer.Write(")arguments[\"");
				writer.Write(parameters[k].Name);
				writer.Write("\"]");

				if (k != parameters.Length - 1)
					writer.Write(", ");
			}
		}
		//===========================================================================================
		protected static void WriteCreateMethod(IndentedTextWriter writer, string typeName)
		{
			switch (typeName)
			{
				case "Double[]":
					writer.Write("Variables.GetDoubleArray");
					break;
				case "Coordinate":
					writer.Write("CreateCoordinate");
					break;
				case "PathArc":
					writer.Write("CreateArc");
					break;
				case "IDefines":
					writer.Write("CreateIDefines");
					break;
				case "IEnumerable<Coordinate>":
					writer.Write("CreateCoordinates");
					break;
				case "IEnumerable<MagickGeometry>":
					writer.Write("CreateMagickGeometryCollection");
					break;
				case "IEnumerable<IPath>":
					writer.Write("CreatePaths");
					break;
				case "IEnumerable<PathArc>":
					writer.Write("CreatePathArcs");
					break;
				case "IEnumerable<PathCurveto>":
					writer.Write("CreatePathCurvetos");
					break;
				case "IEnumerable<PathQuadraticCurveto>":
					writer.Write("CreatePathQuadraticCurvetos");
					break;
				case "IEnumerable<SparseColorArg>":
					writer.Write("CreateSparseColorArgs");
					break;
				case "ImageProfile":
					writer.Write("CreateProfile");
					break;
				case "IReadDefines":
					writer.Write("CreateIReadDefines");
					break;
				case "MagickImage":
					writer.Write("CreateMagickImage");
					break;
				case "MagickGeometry":
					writer.Write("CreateMagickGeometry");
					break;
				case "MontageSettings":
					writer.Write("CreateMontageSettings");
					break;
				case "PixelStorageSettings":
					writer.Write("CreatePixelStorageSettings");
					break;
				case "QuantizeSettings":
					writer.Write("CreateQuantizeSettings");
					break;
				default:
					throw new NotImplementedException(typeName);
			}
		}
		//===========================================================================================
		protected static void WriteEndColon(IndentedTextWriter writer)
		{
			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		protected static void WriteGetElementValue(IndentedTextWriter writer, string typeName, string attributeName)
		{
			writer.Write("Variables.GetValue<");
			writer.Write(typeName);
			writer.Write(">(element, \"");
			writer.Write(attributeName);
			writer.WriteLine("\");");
		}
		//===========================================================================================
		protected static void WriteGetAttributeValue(IndentedTextWriter writer, string typeName)
		{
			writer.Write("Variables.GetValue<");
			writer.Write(typeName);
			writer.WriteLine(">(attribute);");
		}
		//===========================================================================================
		protected void WriteMethod(IndentedTextWriter writer, MethodBase[] methods)
		{
			ParameterInfo[] parameters = (from method in methods
													from param in method.GetParameters()
													select param).DistinctBy(p => p.Name).ToArray();

			if (methods.Length == 1)
			{
				WriteMethod(writer, methods[0], parameters);
			}
			else
			{
				MethodBase[] sortedMethods = (from method in methods
														orderby string.Join(" ", from parameter in method.GetParameters()
																						 select parameter.Name)
														select method).ToArray();
				WriteMethod(writer, sortedMethods, parameters);
			}
		}
		//===========================================================================================
		protected static void WriteSelectElement(IndentedTextWriter writer, string typeName, string elementName)
		{
			switch (typeName)
			{
				case "Double[]":
				case "MagickImage":
					writer.Write("element");
					if (!string.IsNullOrEmpty(elementName))
					{
						writer.Write("[\"");
						writer.Write(elementName);
						writer.Write("\"]");
					}
					break;
				case "IEnumerable<Coordinate>":
				case "IEnumerable<Drawable>":
				case "IEnumerable<MagickGeometry>":
				case "IEnumerable<IPath>":
				case "IEnumerable<PathArc>":
				case "IEnumerable<PathCurveto>":
				case "IEnumerable<PathQuadraticCurveto>":
				case "ImageProfile":
					writer.Write("element");
					break;
				case "IDefines":
				case "IReadDefines":
				case "MontageSettings":
				case "PixelStorageSettings":
				case "QuantizeSettings":
					writer.Write("element[\"");
					writer.Write(elementName);
					writer.Write("\"]");
					break;
				default:
					throw new NotImplementedException(typeName);
			}
		}
		//===========================================================================================
		protected static void WriteStartColon(IndentedTextWriter writer)
		{
			writer.WriteLine("{");
			writer.Indent++;
		}
		//===========================================================================================
		public abstract string Name
		{
			get;
		}
		//===========================================================================================
		protected abstract void WriteCode(IndentedTextWriter writer);
		//===========================================================================================
		public void Write(IndentedTextWriter writer, MagickTypes types)
		{
			Types = types;

			WriteHeader(writer);
			WriteUsing(writer);
			WriteStartNamespace(writer);
			WriteSeparator(writer);
			writer.WriteLine("public sealed partial class MagickScript");
			WriteStartColon(writer);
			WriteSeparator(writer);
			WriteCode(writer);
			WriteSeparator(writer);
			WriteEndColon(writer);
			WriteSeparator(writer);
			WriteEndColon(writer);
		}
		//===========================================================================================
		public void WriteCode(IndentedTextWriter writer, MagickTypes types)
		{
			Types = types;
			WriteCode(writer);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
