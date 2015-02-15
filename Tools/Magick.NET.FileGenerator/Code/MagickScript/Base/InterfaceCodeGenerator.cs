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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal abstract class InterfaceCodeGenerator : SwitchCodeGenerator
	{
		//===========================================================================================
		private InterfaceGenerator[] _InterfaceGenerators;
		//===========================================================================================
		private InterfaceGenerator[] InterfaceGenerators
		{
			get
			{
				if (_InterfaceGenerators == null)
					_InterfaceGenerators = (from type in MagickNET.GetInterfaceTypes(ClassName)
													select new InterfaceGenerator(ClassName, type.Name)).ToArray();

				return _InterfaceGenerators;
			}
		}
		//===========================================================================================
		private void WriteCode(IndentedTextWriter writer, string className)
		{
			writer.Write(className);
			writer.Write("^ MagickScript::Create");
			writer.Write(className);
			writer.WriteLine("(XmlElement^ parent)");
			WriteStartColon(writer);
			CheckNull(writer, "parent");
			writer.WriteLine("XmlElement^ element = (XmlElement^)parent->FirstChild;");
			CheckNull(writer, "element");
			WriteSwitch(writer, from type in MagickNET.GetInterfaceTypes(className)
									  select MagickNET.GetXsdName(type));
			WriteEndColon(writer);
		}
		//===========================================================================================
		protected abstract string ClassName
		{
			get;
		}
		//===========================================================================================
		protected override void WriteCase(IndentedTextWriter writer, string name)
		{
			writer.Write("return Create");
			writer.Write(name[0].ToString().ToUpperInvariant());
			writer.Write(name.Substring(1));
			writer.WriteLine("(element);");
		}
		//===========================================================================================
		protected override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			throw new NotImplementedException();
		}
		//===========================================================================================
		protected override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			throw new NotImplementedException();
		}
		//===========================================================================================
		protected void WriteHeader(IndentedTextWriter writer, string className)
		{
			writer.Write(className);
			writer.Write("^ Create");
			writer.Write(className);
			writer.WriteLine("(XmlElement^ parent);");
		}
		//===========================================================================================
		protected virtual void WriteIncludes(IndentedTextWriter writer, InterfaceGenerator generator)
		{
			throw new NotImplementedException();
		}
		//===========================================================================================
		public override void WriteCode(IndentedTextWriter writer)
		{
			WriteCode(writer, ClassName);

			foreach (InterfaceGenerator generator in InterfaceGenerators)
			{
				generator.WriteCode(writer);
			}
		}
		//===========================================================================================
		public override void WriteHeader(IndentedTextWriter writer)
		{
			WriteHeader(writer, ClassName);

			foreach (InterfaceGenerator generator in InterfaceGenerators)
			{
				generator.WriteHeader(writer);
			}
		}
		//===========================================================================================
		public override void WriteIncludes(IndentedTextWriter writer)
		{
			base.WriteIncludes(writer);

			foreach (InterfaceGenerator generator in InterfaceGenerators)
			{
				WriteIncludes(writer, generator);
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
