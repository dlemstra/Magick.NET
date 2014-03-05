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

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class EnumGenerator : FileGenerator
	{
		//===========================================================================================
		private EnumGenerator()
			: base(@"Magick.NET.AnyCPU\Generated\Enums")
		{
		}
		//===========================================================================================
		private void CreateFiles()
		{
			foreach (Type type in MagickNET.Enums)
			{
				using (IndentedTextWriter writer = CreateWriter(type.Name + ".cs"))
				{
					WriteHeader(writer);
					WriteUsing(writer);
					WriteStartNamespace(writer);
					object[] attributes = type.GetCustomAttributes(false);
					if (attributes.Length == 1 && attributes[0].GetType() == typeof(FlagsAttribute))
						writer.WriteLine("[Flags]");

					writer.WriteLine("public enum " + type.Name);
					WriteStartColon(writer);
					foreach (string name in type.GetEnumNames())
					{
						writer.Write(name);
						writer.Write(" = ");
						writer.Write((int)Enum.Parse(type, name));
						writer.WriteLine(",");
					}
					WriteEndColon(writer);
					WriteEndColon(writer);
					Close(writer);
				}
			}
		}
		//===========================================================================================
		private void WriteUsing(IndentedTextWriter writer)
		{
			writer.WriteLine("using System;");
			writer.WriteLine();
		}
		//===========================================================================================
		public static void Generate()
		{
			EnumGenerator generator = new EnumGenerator();
			generator.CreateFiles();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
