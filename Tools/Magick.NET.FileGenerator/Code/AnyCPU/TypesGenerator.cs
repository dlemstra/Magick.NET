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
using System.Linq;
using System.CodeDom.Compiler;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class TypesGenerator : FileGenerator
	{
		//===========================================================================================
		private Type[] _Types;
		//===========================================================================================
		private TypesGenerator()
			: base(@"Magick.NET.AnyCPU\Generated")
		{
			_Types = GetTypes(true);
		}
		//===========================================================================================
		private void CreateFile()
		{
			using (IndentedTextWriter writer = CreateWriter("Types.cs"))
			{
				WriteHeader(writer);
				WriteUsing(writer);
				WriteStartNamespace(writer);
				writer.WriteLine("internal static class Types");
				WriteStartColon(writer);
				foreach (Type type in _Types)
				{
					writer.Write("public static readonly Type ");
					WriteType(writer, type);
					writer.Write(" = AssemblyHelper.GetType(\"ImageMagick.");
					WriteType(writer, type);
					writer.WriteLine("\");");

					if (IsUsedAsIEnumerable(_Types, type))
						WriteIEnumerable(writer, type);

					if (IsUsedAsNullable(_Types, type))
						WriteNullable(writer, type);
				}
				WriteNullable(writer, typeof(int));
				WriteEndColon(writer);
				WriteEndColon(writer);
				Close(writer);
			}
		}
		//===========================================================================================
		private static void WriteIEnumerable(IndentedTextWriter writer, Type type)
		{
			writer.Write("public static readonly Type IEnumerable");
			WriteType(writer, type);
			writer.Write(" = typeof(IEnumerable<>).MakeGenericType(");
			WriteType(writer, type);
			writer.WriteLine(");");
		}
		//===========================================================================================
		private void WriteNullable(IndentedTextWriter writer, Type type)
		{
			writer.Write("public static readonly Type Nullable");
			WriteType(writer, type);
			writer.Write(" = typeof(Nullable<>).MakeGenericType(");
			if (type == typeof(int))
				writer.Write("typeof(Int32)");
			else
				WriteType(writer, type);
			writer.WriteLine(");");
		}
		//===========================================================================================
		private static void WriteUsing(IndentedTextWriter writer)
		{
			writer.WriteLine("using System;");
			writer.WriteLine("using System.Collections.Generic;");
			writer.WriteLine();
		}
		//===========================================================================================
		public static void Generate()
		{
			TypesGenerator generator = new TypesGenerator();
			generator.CreateFile();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
