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
using System.Collections.Generic;

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
					WriteConverters(writer, type);
					WriteEndColon(writer);
					Close(writer);
				}
			}
		}
		//===========================================================================================
		private void WriteConverters(IndentedTextWriter writer, Type type)
		{
			IEnumerable<Type> types = MagickNET.GetInterfaceTypes(type.Name);
			if (types.Count() == 0)
				return;

			writer.Write("internal static class ");
			writer.Write(type.Name);
			writer.WriteLine("Converter");
			WriteStartColon(writer);
			writer.Write("public static object GetInstance(");
			writer.Write(type.Name);
			writer.WriteLine(" obj)");
			WriteStartColon(writer);
			WriteNullCheck(writer);
			WriteConverterGetSwitch(writer, types);
			WriteEndColon(writer);
			writer.Write("public static object CreateInstance(");
			writer.WriteLine("object obj)");
			WriteStartColon(writer);
			WriteConverterInstanceSwitch(writer, types);
			WriteEndColon(writer);
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteConverterGetSwitch(IndentedTextWriter writer, IEnumerable<Type> types)
		{
			writer.WriteLine("switch (obj.GetType().FullName)");
			WriteStartColon(writer);
			foreach (Type type in types)
			{
				writer.Write("case \"");
				writer.Write(type.FullName);
				writer.WriteLine("\":");
				writer.Indent++;
				writer.Write("return ");
				writer.Write(type.Name);
				writer.WriteLine(".GetInstance((object)obj);");
				writer.Indent--;
			}
			writer.WriteLine("default:");
			writer.Indent++;
			writer.WriteLine("throw new InvalidOperationException(\"Custom interface implementation is not supported in AnyCPU.\");");
			writer.Indent--;
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteConverterInstanceSwitch(IndentedTextWriter writer, IEnumerable<Type> types)
		{
			writer.WriteLine("switch (obj.GetType().FullName)");
			WriteStartColon(writer);
			foreach (Type type in types)
			{
				writer.Write("case \"");
				writer.Write(type.FullName);
				writer.WriteLine("\":");
				writer.Indent++;
				writer.Write("return new ");
				writer.Write(type.Name);
				writer.WriteLine("(obj);");
				writer.Indent--;
			}
			writer.WriteLine("default:");
			writer.Indent++;
			writer.WriteLine("throw new NotImplementedException();");
			writer.Indent--;
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteInterfaceStart(IndentedTextWriter writer, Type type)
		{
			bool writeColon = true;

			writer.Write("public interface ");
			writer.Write(type.Name);
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
		private static void WriteNullCheck(IndentedTextWriter writer)
		{
			writer.WriteLine("if (obj == null)");
			writer.Indent++;
			writer.WriteLine("return null;");
			writer.Indent--;
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
