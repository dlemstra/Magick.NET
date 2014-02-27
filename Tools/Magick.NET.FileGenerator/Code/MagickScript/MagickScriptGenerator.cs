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
using System.Collections.Generic;
using System.IO;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class MagickScriptGenerator
	{
		//===========================================================================================
		private List<ExecuteCodeGenerator> _ExecuteCodeGenerators;
		private List<ConstructorCodeGenerator> _ConstructorCodeGenerators;
		private List<SettingsCodeGenerator> _SettingsCodeGenerators;
		//===========================================================================================
		private MagickScriptGenerator()
		{
			InitializeCodeGenerators();
		}
		//===========================================================================================
		private void InitializeCodeGenerators()
		{
			_ExecuteCodeGenerators = new List<ExecuteCodeGenerator>();

			_ExecuteCodeGenerators.Add(new MagickImageGenerator());
			_ExecuteCodeGenerators.Add(new MagickImageCollectionGenerator());
			_ExecuteCodeGenerators.Add(new DrawableGenerator());
			_ExecuteCodeGenerators.Add(new PathsGenerator());

			_ConstructorCodeGenerators = new List<ConstructorCodeGenerator>();
			_ConstructorCodeGenerators.Add(new CoordinateGenerator());
			_ConstructorCodeGenerators.Add(new ImageProfileGenerator());
			_ConstructorCodeGenerators.Add(new PathArcGenerator());
			_ConstructorCodeGenerators.Add(new PathCurvetoGenerator());
			_ConstructorCodeGenerators.Add(new PathQuadraticCurvetoGenerator());

			_SettingsCodeGenerators = new List<SettingsCodeGenerator>();
			_SettingsCodeGenerators.Add(new MagickReadSettingsGenerator());
			_SettingsCodeGenerators.Add(new PixelStorageSettingsGenerator());
		}
		//===========================================================================================
		private void CreateCodeFile()
		{
			using (IndentedTextWriter writer = CreateWriter(@"Execute.cpp"))
			{
				WriteHeader(writer);
				WriteIncludes(writer);
				writer.WriteLine("#pragma warning (disable: 4100)");
				writer.WriteLine("namespace ImageMagick");
				writer.WriteLine("{");
				writer.Indent++;
				WriteCallInitializeExecute(writer);
				WriteInitializeExecute(writer);
				WriteExecuteMethods(writer);
				WriteConstructors(writer);
				WriteSettings(writer);
				writer.Indent--;
				writer.WriteLine("}");
				writer.WriteLine("#pragma warning (default: 4100)");

				writer.InnerWriter.Dispose();
			}
		}
		//===========================================================================================
		private void CreateHeaderFile()
		{
			using (IndentedTextWriter writer = CreateWriter(@"Execute.h"))
			{
				WriteHeader(writer);
				writer.WriteLine("void InitializeExecute();");

				foreach (ExecuteCodeGenerator codeGenerator in _ExecuteCodeGenerators)
				{
					codeGenerator.WriteHeader(writer);
				}

				foreach (ConstructorCodeGenerator codeGenerator in _ConstructorCodeGenerators)
				{
					codeGenerator.WriteHeader(writer);
				}

				foreach (SettingsCodeGenerator codeGenerator in _SettingsCodeGenerators)
				{
					codeGenerator.WriteHeader(writer);
				}

				writer.InnerWriter.Dispose();
			}
		}
		//===========================================================================================
		private static IndentedTextWriter CreateWriter(string fileName)
		{
			string outputFile = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET\Script\Generated\" + fileName);
			Console.WriteLine("Creating: " + outputFile);

			FileStream output = File.Create(outputFile);
			StreamWriter streamWriter = new StreamWriter(output);
			IndentedTextWriter writer = new IndentedTextWriter(streamWriter, "\t");
			return writer;
		}
		//===========================================================================================
		private void WriteCallInitializeExecute(IndentedTextWriter writer)
		{
			writer.WriteLine("void MagickScript::InitializeExecute()");
			writer.WriteLine("{");
			writer.Indent++;

			foreach (ExecuteCodeGenerator codeGenerator in _ExecuteCodeGenerators)
			{
				codeGenerator.WriteCallInitializeExecute(writer);
			}

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private void WriteConstructors(IndentedTextWriter writer)
		{
			foreach (ConstructorCodeGenerator codeGenerator in _ConstructorCodeGenerators)
			{
				codeGenerator.WriteCode(writer);
			}
		}
		//===========================================================================================
		private void WriteExecuteMethods(IndentedTextWriter writer)
		{
			foreach (ExecuteCodeGenerator codeGenerator in _ExecuteCodeGenerators)
			{
				codeGenerator.WriteExecuteMethods(writer);
			}
		}
		//===========================================================================================
		private static void WriteHeader(IndentedTextWriter writer)
		{
			writer.WriteLine("//=================================================================================================");
			writer.WriteLine("// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>");
			writer.WriteLine("//");
			writer.WriteLine("// Licensed under the ImageMagick License (the \"License\"); you may not use this file except in");
			writer.WriteLine("// compliance with the License. You may obtain a copy of the License at");
			writer.WriteLine("//");
			writer.WriteLine("//   http://www.imagemagick.org/script/license.php");
			writer.WriteLine("//");
			writer.WriteLine("// Unless required by applicable law or agreed to in writing, software distributed under the");
			writer.WriteLine("// License is distributed on an \"AS IS\" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either");
			writer.WriteLine("// express or implied. See the License for the specific language governing permissions and");
			writer.WriteLine("// limitations under the License.");
			writer.WriteLine("//=================================================================================================");
		}
		//===========================================================================================
		private void WriteIncludes(IndentedTextWriter writer)
		{
			writer.WriteLine(@"#include ""Stdafx.h""");
			writer.WriteLine(@"#include ""..\..\Helpers\XmlHelper.h""");
			writer.WriteLine(@"#include ""..\MagickScript.h""");

			foreach (ExecuteCodeGenerator codeGenerator in _ExecuteCodeGenerators)
			{
				codeGenerator.WriteIncludes(writer);
			}

			foreach (ConstructorCodeGenerator codeGenerator in _ConstructorCodeGenerators)
			{
				codeGenerator.WriteIncludes(writer);
			}
		}
		//===========================================================================================
		private void WriteInitializeExecute(IndentedTextWriter writer)
		{
			foreach (ExecuteCodeGenerator codeGenerator in _ExecuteCodeGenerators)
			{
				codeGenerator.WriteInitializeExecute(writer);
			}
		}
		//===========================================================================================
		private void WriteSettings(IndentedTextWriter writer)
		{
			foreach (SettingsCodeGenerator codeGenerator in _SettingsCodeGenerators)
			{
				codeGenerator.WriteCode(writer);
			}
		}
		//===========================================================================================
		internal static void Generate()
		{
			MagickScriptGenerator generator = new MagickScriptGenerator();
			generator.CreateHeaderFile();
			generator.CreateCodeFile();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
