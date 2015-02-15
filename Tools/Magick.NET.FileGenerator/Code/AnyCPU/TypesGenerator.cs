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

namespace Magick.NET.FileGenerator.AnyCPU
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
		private static void WriteMember(IndentedTextWriter writer, Type type, string suffix)
		{
			writer.Write("private static Type _");
			if (!string.IsNullOrEmpty(suffix))
				writer.Write(suffix);
			WriteType(writer, type);
			writer.WriteLine(";");
		}
		//===========================================================================================
		private static void WritePropertyStart(IndentedTextWriter writer, Type type, string suffix)
		{
			writer.Write("public static Type ");
			if (!string.IsNullOrEmpty(suffix))
				writer.Write(suffix);
			WriteType(writer, type);
			writer.WriteLine();
			WriteStartColon(writer);
			writer.WriteLine("get");
			WriteStartColon(writer);
			writer.Write("if (_");
			if (!string.IsNullOrEmpty(suffix))
				writer.Write(suffix);
			WriteType(writer, type);
			writer.WriteLine(" == null)");
			writer.Indent++;
			writer.Write("_");
			if (!string.IsNullOrEmpty(suffix))
				writer.Write(suffix);
			WriteType(writer, type);
			writer.Write(" = ");
		}
		//===========================================================================================
		private static void WritePropertyEnd(IndentedTextWriter writer, Type type, string suffix)
		{
			writer.Indent--;
			writer.Write("return _");
			if (!string.IsNullOrEmpty(suffix))
				writer.Write(suffix);
			WriteType(writer, type);
			writer.WriteLine(";");
			WriteEndColon(writer);
			WriteEndColon(writer);
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
					WriteMember(writer, type, null);
					WritePropertyStart(writer, type, null);
					writer.Write("AssemblyHelper.GetType(\"ImageMagick.");
					WriteType(writer, type);
					writer.WriteLine("\");");
					WritePropertyEnd(writer, type, null);

					if (IsUsedAsIEnumerable(_Types, type))
						WriteIEnumerable(writer, type);

					if (IsUsedAsNullable(_Types, type))
						WriteNullable(writer, type);
				}
				WriteNullable(writer, typeof(Boolean));
				WriteNullable(writer, typeof(int));
				WriteEndColon(writer);
				WriteEndColon(writer);
				Close(writer);
			}
		}
		//===========================================================================================
		private static void WriteIEnumerable(IndentedTextWriter writer, Type type)
		{
			WriteMember(writer, type, "IEnumerable");
			WritePropertyStart(writer, type, "IEnumerable");
			writer.Write("typeof(IEnumerable<>).MakeGenericType(");
			WriteType(writer, type);
			writer.WriteLine(");");
			WritePropertyEnd(writer, type, "IEnumerable");
		}
		//===========================================================================================
		private void WriteNullable(IndentedTextWriter writer, Type type)
		{
			WriteMember(writer, type, "Nullable");
			WritePropertyStart(writer, type, "Nullable");
			writer.Write("typeof(Nullable<>).MakeGenericType(");
			if (type == typeof(Boolean))
				writer.Write("typeof(Boolean)");
			else if (type == typeof(int))
				writer.Write("typeof(Int32)");
			else
				WriteType(writer, type);
			writer.WriteLine(");");
			WritePropertyEnd(writer, type, "Nullable");
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
