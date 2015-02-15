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
		private List<CodeGenerator> _CodeGenerators;
		//===========================================================================================
		private MagickScriptGenerator()
			: base(@"Magick.NET\Script\Generated")
		{
			InitializeCodeGenerators();
		}
		//===========================================================================================
		private void InitializeCodeGenerators()
		{
			_CodeGenerators = new List<CodeGenerator>();

			_CodeGenerators.Add(new MagickImageGenerator());
			_CodeGenerators.Add(new MagickImageCollectionGenerator());
			_CodeGenerators.Add(new DrawableGenerator());
			_CodeGenerators.Add(new PathsGenerator());

			_CodeGenerators.Add(new CoordinateGenerator());
			_CodeGenerators.Add(new ColorProfileGenerator());
			_CodeGenerators.Add(new ImageProfileGenerator());
			_CodeGenerators.Add(new PathArcGenerator());
			_CodeGenerators.Add(new PathCurvetoGenerator());
			_CodeGenerators.Add(new PathQuadraticCurvetoGenerator());
			_CodeGenerators.Add(new SparseColorArg());

			_CodeGenerators.Add(new MagickReadSettingsGenerator());
			_CodeGenerators.Add(new MontageSettingsGenerator());
			_CodeGenerators.Add(new PixelStorageSettingsGenerator());
			_CodeGenerators.Add(new QuantizeSettingsGenerator());

			_CodeGenerators.Add(new IDefinesGenerator());
			_CodeGenerators.Add(new CollectionGenerator());
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
				WriteCode(writer);
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

				foreach (CodeGenerator codeGenerator in _CodeGenerators)
				{
					codeGenerator.WriteHeader(writer);
				}

				Close(writer);
			}
		}
		//===========================================================================================
		private void WriteCode(IndentedTextWriter writer)
		{
			foreach (CodeGenerator codeGenerator in _CodeGenerators)
			{
				codeGenerator.WriteCode(writer);
			}
		}
		//===========================================================================================
		private void WriteIncludes(IndentedTextWriter writer)
		{
			writer.WriteLine(@"#include ""Stdafx.h""");
			writer.WriteLine(@"#include ""..\..\Helpers\XmlHelper.h""");
			writer.WriteLine(@"#include ""..\MagickScript.h""");

			foreach (CodeGenerator codeGenerator in _CodeGenerators)
			{
				codeGenerator.WriteIncludes(writer);
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
