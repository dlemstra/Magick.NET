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
using System.Linq;

namespace Magick.NET.FileGenerator.AnyCPU
{
	//==============================================================================================
	internal sealed class ExceptionGenerator : FileGenerator
	{
		//===========================================================================================
		private Type[] _Exceptions;
		//===========================================================================================
		private ExceptionGenerator()
			: base(@"Magick.NET.AnyCPU\Generated")
		{
			_Exceptions = (from type in MagickNET.GetPublicTypes(false)
								where type.IsSubclassOf(typeof(Exception))
								select type).ToArray();
		}
		//===========================================================================================
		private void CreateFile()
		{
			using (IndentedTextWriter writer = CreateWriter("ExceptionHelper.cs"))
			{
				WriteHeader(writer);
				WriteUsing(writer);
				WriteStartNamespace(writer);
				WriteHelper(writer);
				WriteExceptions(writer);
				WriteEndColon(writer);
				Close(writer);
			}
		}
		//===========================================================================================
		private void WriteExceptions(IndentedTextWriter writer)
		{
			foreach (Type type in _Exceptions)
			{
				if (type.IsSealed)
					writer.Write("public sealed class ");
				else
					writer.Write("public class ");
				WriteType(writer, type);
				writer.Write(" : ");
				WriteType(writer, type.BaseType);
				WriteStartColon(writer);
				writer.Write("internal ");
				WriteType(writer, type);
				writer.WriteLine("(Exception exception)");
				writer.Indent++;
				writer.Write(": base(");
				if (type.BaseType == typeof(Exception))
					writer.Write("exception.Message, ");
				writer.WriteLine("exception)");
				writer.Indent--;
				WriteStartColon(writer);
				WriteEndColon(writer);
				WriteEndColon(writer);
			}
		}
		//===========================================================================================
		private void WriteHelper(IndentedTextWriter writer)
		{
			writer.WriteLine("public static class ExceptionHelper");
			WriteStartColon(writer);
			writer.WriteLine("public static Exception Create(Exception exception)");
			WriteStartColon(writer);
			writer.WriteLine("if (ReferenceEquals(exception, null))");
			writer.Indent++;
			writer.WriteLine("return null;");
			writer.Indent--;

			writer.WriteLine("switch(exception.GetType().Name)");
			WriteStartColon(writer);
			foreach (Type exception in _Exceptions)
			{
				writer.Write("case \"");
				writer.Write(exception.Name);
				writer.WriteLine("\":");
				writer.Indent++;
				writer.Write("return new ");
				writer.Write(exception.Name);
				writer.WriteLine("(exception);");
				writer.Indent--;
			}
			writer.WriteLine("default:");
			writer.Indent++;
			writer.WriteLine("return exception;");
			writer.Indent--;
			WriteEndColon(writer);
			WriteEndColon(writer);
			WriteEndColon(writer);
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
			ExceptionGenerator generator = new ExceptionGenerator();
			generator.CreateFile();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
