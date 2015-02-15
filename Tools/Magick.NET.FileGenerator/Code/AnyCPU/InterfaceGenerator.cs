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
using System.Linq;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Magick.NET.FileGenerator.AnyCPU
{
	//==============================================================================================
	internal sealed class InterfaceGenerator : FileGenerator
	{
		//===========================================================================================
		private InterfaceGenerator()
			: base(@"Magick.NET.AnyCPU\Generated")
		{
		}
		//===========================================================================================
		private void CreateFiles()
		{
			foreach (Type type in MagickNET.Interfaces)
			{
				using (IndentedTextWriter writer = CreateWriter(type.Name + ".cs"))
				{
					WriteHeader(writer);
					WriteUsing(writer);
					WriteStartNamespace(writer, type);
					WriteInterfaceStart(writer, type);
					WriteStartColon(writer);
					WriteProperties(writer, type);
					WriteEndColon(writer);
					WriteEndColon(writer);
					Close(writer);
				}
			}
		}
		//===========================================================================================
		private void WriteInterfaceStart(IndentedTextWriter writer, Type type)
		{
			bool writeColon = true;

			writer.Write("public interface " + type.Name);
			foreach (Type interfaceType in type.GetInterfaces())
			{
				if (writeColon)
				{
					writer.Write(": ");
					writeColon = false;
				}
				else
				{
					writer.Write(", ");
				}

				WriteType(writer, interfaceType);
			}
			writer.WriteLine();
		}
		//===========================================================================================
		private void WriteProperties(IndentedTextWriter writer, Type type)
		{
			foreach (PropertyInfo property in type.GetProperties().OrderBy(p => p.Name))
			{
				WriteType(writer, property.PropertyType);
				writer.Write(" ");
				writer.Write(property.Name);
				writer.WriteLine();
				WriteStartColon(writer);
				if (property.GetMethod != null)
					writer.WriteLine("get;");
				if (property.SetMethod != null)
					writer.WriteLine("set;");
				WriteEndColon(writer);
			}
		}
		//===========================================================================================
		private void WriteUsing(IndentedTextWriter writer)
		{
			writer.WriteLine("using System;");
			writer.WriteLine("using System.Collections.Generic;");
			writer.WriteLine();
		}
		//===========================================================================================
		public static void Generate()
		{
			InterfaceGenerator generator = new InterfaceGenerator();
			generator.CreateFiles();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
