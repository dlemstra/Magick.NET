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
	internal abstract class PropertyCodeGenerator : CodeGenerator
	{
		//===========================================================================================
		private PropertyInfo[] _Properties;
		//===========================================================================================
		private PropertyInfo[] Properties
		{
			get
			{
				if (_Properties == null)
					_Properties = MagickNET.GetProperties(ClassName).ToArray();

				return _Properties;
			}
		}
		//===========================================================================================
		private void WriteGetValue(IndentedTextWriter writer, PropertyInfo property)
		{
			string typeName = MagickNET.GetTypeName(property);
			string xsdTypeName = XsdGenerator.GetAttributeType(property);

			if (xsdTypeName != null)
			{
				WriteGetAttributeValue(writer, typeName, XsdGenerator.GetName(property));
			}
			else
			{
				WriteCreateMethod(writer, typeName);
				writer.Write("(");
				WriteSelectElement(writer, typeName, XsdGenerator.GetName(property));
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
			writer.WriteLine("{");
			writer.Indent++;
			writer.Write(ClassName);
			writer.Write("^ result = gcnew ");
			writer.Write(ClassName);
			writer.WriteLine("();");
			WriteSetProperties(writer);
			writer.WriteLine("return result;");
			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		public void WriteHeader(IndentedTextWriter writer)
		{
			writer.Write("static ");
			writer.Write(ClassName);
			writer.Write("^ Create");
			writer.Write(ClassName);
			writer.WriteLine("(XmlElement^ element);");
		}
		//===========================================================================================
	}
	//==============================================================================================
}
