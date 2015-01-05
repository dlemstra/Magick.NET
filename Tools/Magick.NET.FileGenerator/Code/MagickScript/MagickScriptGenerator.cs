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
using System.Collections.Generic;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class MagickScriptGenerator : FileGenerator
	{
		//===========================================================================================
		private List<ExecuteCodeGenerator> _ExecuteCodeGenerators;
		private List<ConstructorCodeGenerator> _ConstructorCodeGenerators;
		private List<SettingsCodeGenerator> _SettingsCodeGenerators;
		//===========================================================================================
		private MagickScriptGenerator()
			: base(@"Magick.NET\Script\Generated")
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
			_ConstructorCodeGenerators.Add(new ColorProfileGenerator());
			_ConstructorCodeGenerators.Add(new ImageProfileGenerator());
			_ConstructorCodeGenerators.Add(new PathArcGenerator());
			_ConstructorCodeGenerators.Add(new PathCurvetoGenerator());
			_ConstructorCodeGenerators.Add(new PathQuadraticCurvetoGenerator());
			_ConstructorCodeGenerators.Add(new SparseColorArg());

			_SettingsCodeGenerators = new List<SettingsCodeGenerator>();
			_SettingsCodeGenerators.Add(new MagickReadSettingsGenerator());
			_SettingsCodeGenerators.Add(new MontageSettingsGenerator());
			_SettingsCodeGenerators.Add(new PixelStorageSettingsGenerator());
			_SettingsCodeGenerators.Add(new QuantizeSettingsGenerator());
		}
		//===========================================================================================
		private void CreateCodeFile()
		{
			using (IndentedTextWriter writer = CreateWriter(@"Execute.cpp"))
			{
				WriteHeader(writer);
				WriteIncludes(writer);
				writer.WriteLine("#pragma warning (disable: 4100)");
				WriteStartNamespace(writer);
				WriteExecuteMethods(writer);
				WriteConstructors(writer);
				WriteSettings(writer);
				WriteEndColon(writer);
				writer.WriteLine("#pragma warning (default: 4100)");

				Close(writer);
			}
		}
		//===========================================================================================
		private void CreateHeaderFile()
		{
			using (IndentedTextWriter writer = CreateWriter(@"Execute.h"))
			{
				WriteHeader(writer);

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

				Close(writer);
			}
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
		private void WriteSettings(IndentedTextWriter writer)
		{
			foreach (SettingsCodeGenerator codeGenerator in _SettingsCodeGenerators)
			{
				codeGenerator.WriteCode(writer);
			}
		}
		//===========================================================================================
		public static void Generate()
		{
			MagickScriptGenerator generator = new MagickScriptGenerator();
			generator.CreateHeaderFile();
			generator.CreateCodeFile();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
