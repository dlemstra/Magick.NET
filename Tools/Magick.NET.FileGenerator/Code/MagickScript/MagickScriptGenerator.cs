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
using System.IO;

namespace Magick.NET.FileGenerator
{
  //==============================================================================================
  internal sealed class MagickScriptGenerator
  {
    private string _OutputFolder;
    private MagickTypes _Types;

    private MagickScriptGenerator()
    {
      _OutputFolder = SetOutputFolder(@"Magick.NET\Script\Generated");
      _Types = new MagickTypes(QuantumDepth.Q16HDRI);
    }

    private void Cleanup()
    {
      foreach (string fileName in Directory.GetFiles(_OutputFolder))
      {
        File.Delete(fileName);
      }
    }

    private static void Close(IndentedTextWriter writer)
    {
      writer.InnerWriter.Dispose();
    }

    private void CreateCodeFile(CodeGenerator generator)
    {
      using (IndentedTextWriter writer = CreateWriter(generator.Name + ".cs"))
      {
        generator.Write(writer, _Types);
        Close(writer);
      }
    }

    private IndentedTextWriter CreateWriter(string fileName)
    {
      string outputFile = Path.GetFullPath(_OutputFolder + @"\" + fileName);
      Console.WriteLine("Creating: " + outputFile);

      FileStream output = File.Create(outputFile);
      StreamWriter streamWriter = new StreamWriter(output);
      IndentedTextWriter writer = new IndentedTextWriter(streamWriter, "  ");
      return writer;
    }

    private static string SetOutputFolder(string outputFolder)
    {
      string result = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\";
      result += outputFolder;
      if (result[result.Length - 1] != '\\')
        result += "\\";

      return result;
    }

    private void WriteCollection()
    {
      CreateCodeFile(new CollectionGenerator());
    }

    private void WriteConstructors()
    {
      CreateCodeFile(new ColorProfileGenerator());
      CreateCodeFile(new CoordinateGenerator());
      CreateCodeFile(new ImageProfileGenerator());
      CreateCodeFile(new PathArcGenerator());
      CreateCodeFile(new PathCurvetoGenerator());
      CreateCodeFile(new PathQuadraticCurvetoGenerator());
      CreateCodeFile(new SparseColorArg());
    }

    private void WriteExecute()
    {
      CreateCodeFile(new DrawableGenerator());
      CreateCodeFile(new PathsGenerator());
      CreateCodeFile(new MagickImageCollectionGenerator());
      CreateCodeFile(new MagickImageGenerator());
    }

    private void WriteInterfaces()
    {
      CreateCodeFile(new IDefinesGenerator());
    }

    private void WriteSettings()
    {
      CreateCodeFile(new MagickReadSettingsGenerator());
      CreateCodeFile(new MontageSettingsGenerator());
      CreateCodeFile(new PixelStorageSettingsGenerator());
      CreateCodeFile(new QuantizeSettingsGenerator());
    }

    public static void Generate()
    {
      MagickScriptGenerator generator = new MagickScriptGenerator();

      generator.Cleanup();

      generator.WriteCollection();
      generator.WriteConstructors();
      generator.WriteExecute();
      generator.WriteInterfaces();
      generator.WriteSettings();
    }
  }
}
