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
using System.CodeDom.Compiler;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class IDefinesGenerator : InterfaceCodeGenerator
	{
		//===========================================================================================
		protected override string ClassName
		{
			get
			{
				return "IDefines";
			}
		}
		//===========================================================================================
		protected override void WriteIncludes(IndentedTextWriter writer, InterfaceGenerator generator)
		{
			string type = generator.ClassName.Replace("ReadDefines", "");
			type = type.Replace("WriteDefines", "");

			writer.Write(@"#include ""..\..\Defines\");
			writer.Write(type);
			writer.Write("\\");
			writer.Write(generator.ClassName);
			writer.WriteLine(@".h""");
		}
		//===========================================================================================
		public override void WriteCode(IndentedTextWriter writer)
		{
			writer.WriteLine("IReadDefines^ MagickScript::CreateIReadDefines(XmlElement^ parent)");
			WriteStartColon(writer);
			writer.WriteLine("return dynamic_cast<IReadDefines^>(CreateIDefines(parent));");
			WriteEndColon(writer);

			base.WriteCode(writer);
		}
		//===========================================================================================
		public override void WriteHeader(IndentedTextWriter writer)
		{
			WriteHeader(writer, "IReadDefines");

			base.WriteHeader(writer);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
