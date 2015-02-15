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
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class CollectionGenerator : CodeGenerator
	{
		//===========================================================================================
		private Type[] _Types;
		//===========================================================================================
		private Type[] Types
		{
			get
			{
				if (_Types == null)
					_Types = new Type[] { MagickNET.GetTypeName("MagickGeometry") };

				return _Types;
			}
		}
		//===========================================================================================
		protected override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			throw new NotImplementedException();
		}
		//===========================================================================================
		public override void WriteCode(IndentedTextWriter writer)
		{
			foreach (Type type in Types)
			{
				string typeName = MagickNET.GetCppTypeName(type);

				writer.Write("Collection<");
				writer.Write(typeName);
				writer.Write(">");
				writer.Write("^ MagickScript::Create");
				writer.Write(type.Name);
				writer.Write("Collection");
				writer.Write("(XmlElement^ element)");
				writer.WriteLine();
				WriteStartColon(writer);
				writer.Write("Collection<");
				writer.Write(typeName);
				writer.Write(">^ collection = gcnew ");
				writer.Write("Collection<");
				writer.Write(typeName);
				writer.WriteLine(">();");
				writer.WriteLine("for each (XmlElement^ elem in element->SelectNodes(\"*\"))");
				WriteStartColon(writer);
				writer.Write("collection->Add(_Variables->GetValue<");
				writer.Write(typeName);
				writer.WriteLine(">(elem, \"value\"));");
				WriteEndColon(writer);
				writer.WriteLine("return collection;");
				WriteEndColon(writer);
			}
		}
		//===========================================================================================
		public override void WriteHeader(IndentedTextWriter writer)
		{
			foreach (Type type in Types)
			{
				string typeName = MagickNET.GetCppTypeName(type);

				writer.Write("Collection<");
				writer.Write(typeName);
				writer.Write(">");
				writer.Write("^ Create");
				writer.Write(type.Name);
				writer.Write("Collection");
				writer.Write("(XmlElement^ element)");
				writer.WriteLine(";");
			}
		}
		//===========================================================================================
		protected override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			throw new NotImplementedException();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
