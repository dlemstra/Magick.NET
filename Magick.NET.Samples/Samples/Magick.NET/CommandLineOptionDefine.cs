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

using ImageMagick;

namespace RootNamespace.Samples.MagickNET
{
	public static class CommandLineOptionDefineSamples
	{
		public static void CommandLineOptionDefine()
		{
			using (MagickImage image = new MagickImage(SampleFiles.SnakewarePng))
			{
				image.SetDefine(MagickFormat.Dds, "compression", "dxt1");
				image.Write(SampleFiles.OutputDirectory + "Snakeware.dds");
			}
		}

		public static void DefinesThatNeedToBeSetBeforeReadingAnImage()
		{
			MagickReadSettings settings = new MagickReadSettings();
			settings.SetDefine(MagickFormat.Jpeg, "size", "32x32");
			using (MagickImage image = new MagickImage(SampleFiles.SnakewareJpg))
			{
				image.Thumbnail(32, 32);
				image.Write(SampleFiles.OutputDirectory +"Snakeware.tiff");
			}
		}
	}
}
