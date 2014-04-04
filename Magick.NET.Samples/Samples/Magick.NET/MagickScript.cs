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

using System.IO;
using ImageMagick;

namespace RootNamespace.Samples.MagickNET
{
	public static class MagickScriptSamples
	{
		private static void OnScriptRead(object sender, ScriptReadEventArgs arguments)
		{
			arguments.Image = new MagickImage(SampleFiles.SnakewareJpg);
		}

		private static void OnScriptWrite(object sender, ScriptWriteEventArgs arguments)
		{
			arguments.Image.Write(SampleFiles.SnakewarePng);
		}

		public static void Resize()
		{
			MagickScript script = new MagickScript(SampleFiles.ResizeMsl);
			script.Execute();
		}

		public static void ReuseSameScript()
		{
			MagickScript script = new MagickScript(SampleFiles.WaveMsl);

			string[] files = new string[] { SampleFiles.FujiFilmFinePixS1ProJpg, SampleFiles.SnakewareJpg };
			foreach (string fileName in files)
			{
				using (MagickImage image = new MagickImage(fileName))
				{
					script.Execute(image);
					image.Write(SampleFiles.OutputDirectory + fileName + ".wave.jpg");
				}
			}
		}

		public static void ReadWriteEvents()
		{
			MagickScript script = new MagickScript(SampleFiles.CropMsl);
			script.Read += OnScriptRead;
			script.Write += OnScriptWrite;
			script.Execute();
		}

		public static void WriteMultipleOutputFiles()
		{
			MagickScript script = new MagickScript(SampleFiles.CloneMsl);
			script.Execute();
		}
	}
}
