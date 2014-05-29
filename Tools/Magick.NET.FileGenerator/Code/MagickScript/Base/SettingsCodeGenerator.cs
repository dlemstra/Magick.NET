//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
	internal abstract class SettingsCodeGenerator : CodeGenerator
	{
		//===========================================================================================
		private MethodBase[] Methods
		{
			get
			{
				return MagickNET.GetMethods(ClassName).ToArray();
			}
		}
		//===========================================================================================
		private PropertyInfo[] Properties
		{
			get
			{
				return MagickNET.GetProperties(ClassName).ToArray();
			}
		}
		//===========================================================================================
		private void WriteCallMethods(IndentedTextWriter writer)
		{
			foreach (MethodBase method in Methods)
			{
				string xsdMethodName = MagickNET.GetXsdName(method);
				ParameterInfo[] parameters = method.GetParameters();

				writer.Write("XmlElement^ ");
				writer.Write(xsdMethodName);
				writer.Write(" = (XmlElement^)element->SelectSingleNode(\"");
				writer.Write(xsdMethodName);
				writer.WriteLine("\");");

				writer.Write("if (");
				writer.Write(xsdMethodName);
				writer.WriteLine(" != nullptr)");
				writer.WriteLine("{");

				writer.Indent++;

				foreach (ParameterInfo parameter in parameters)
				{
					string typeName = MagickNET.GetCppTypeName(parameter);

					writer.Write(typeName);
					writer.Write(" ");
					writer.Write(parameter.Name);
					writer.Write("_ = XmlHelper::GetAttribute<");
					writer.Write(typeName);
					writer.Write(">(");
					writer.Write(xsdMethodName);
					writer.Write(", \"");
					writer.Write(parameter.Name);
					writer.WriteLine("\");");
				}

				writer.Write("result->");
				writer.Write(method.Name);
				writer.Write("(");

				for (int i = 0; i < parameters.Length; i++)
				{
					if (i > 0)
						writer.Write(",");

					writer.Write(parameters[i].Name);
					writer.Write("_");
				}

				writer.WriteLine(");");
				writer.Indent--;

				writer.WriteLine("}");
			}
		}
		//===========================================================================================
		private void WriteGetValue(IndentedTextWriter writer, PropertyInfo property)
		{
			string typeName = MagickNET.GetCppTypeName(property);
			string xsdTypeName = MagickNET.GetXsdAttributeType(property);

			if (xsdTypeName != null)
			{
				WriteGetElementValue(writer, typeName, MagickNET.GetXsdName(property));
			}
			else
			{
				WriteCreateMethod(writer, typeName);
				writer.Write("(");
				WriteSelectElement(writer, typeName, MagickNET.GetXsdName(property));
				writer.WriteLine(");");
			}
		}
		//===========================================================================================
		private void WriteSetProperties(IndentedTextWriter writer)
		{
			foreach (PropertyInfo property in Properties)
			{
				writer.Write("result->");
				writer.Write(property.Name);
				writer.Write(" = ");
				WriteGetValue(writer, property);
			}
		}
		//===========================================================================================
		protected abstract string ClassName
		{
			get;
		}
		//===========================================================================================
		protected sealed override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			throw new NotImplementedException();
		}
		//===========================================================================================
		protected sealed override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			throw new NotImplementedException();
		}
		//===========================================================================================
		public void WriteCode(IndentedTextWriter writer)
		{
			writer.Write(ClassName);
			writer.Write("^ MagickScript::Create");
			writer.Write(ClassName);
			writer.WriteLine("(XmlElement^ element)");
			WriteStartColon(writer);
			writer.WriteLine("if (element == nullptr)");
			writer.Indent++;
			writer.WriteLine("return nullptr;");
			writer.Indent--;
			writer.Write(ClassName);
			writer.Write("^ result = gcnew ");
			writer.Write(ClassName);
			writer.WriteLine("();");
			WriteSetProperties(writer);
			WriteCallMethods(writer);
			writer.WriteLine("return result;");
			WriteEndColon(writer);
		}
		//===========================================================================================
		public void WriteHeader(IndentedTextWriter writer)
		{
			writer.Write(ClassName);
			writer.Write("^ Create");
			writer.Write(ClassName);
			writer.WriteLine("(XmlElement^ element);");
		}
		//===========================================================================================
	}
	//==============================================================================================
}
