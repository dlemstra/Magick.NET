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
	public static class ConvertPDFSamples
	{
		/*
			You need to install the latest version of GhostScript before you can convert a pdf using Magick.NET.

			Make sure you only install the version of GhostScript with the same platform. If you use the 64-bit
			version of Magick.NET you should also install the 64-bit version of Ghostscript. You can use the 32-bit
			version together with the 64-version but you will get a better performance if you keep the platforms the same.
		*/

		public static void ConvertPDFToMultipleImages()
		{
			MagickReadSettings settings = new MagickReadSettings();
			settings.Density = new MagickGeometry(300, 300);

			using (MagickImageCollection images = new MagickImageCollection())
			{
				images.Read(SampleFiles.SnakewarePdf, settings);

				int page = 1;
				foreach (MagickImage image in images)
				{
					image.Write(SampleFiles.OutputDirectory + "Snakeware.Page" + page + ".png");
					// Writing to a specific format works the same as for a single image
					image.Format = MagickFormat.Ptif;
					image.Write(SampleFiles.OutputDirectory + "Snakeware.Page" + page + ".tif");
					page++;
				}
			}
		}

		public static void ConvertPDFTOneImage()
		{
			MagickReadSettings settings = new MagickReadSettings();
			settings.Density = new MagickGeometry(300, 300);

			using (MagickImageCollection images = new MagickImageCollection())
			{
				images.Read(SampleFiles.SnakewarePdf, settings);

				MagickImage horizontal = images.AppendHorizontally();
				horizontal.Write(SampleFiles.OutputDirectory + "Snakeware.horizontal.png");

				MagickImage vertical = images.AppendVertically();
				vertical.Write(SampleFiles.OutputDirectory + "Snakeware.vertical.png");
			}
		}

		public static void CreatePDFFromTwoImages()
		{
			using (MagickImageCollection collection = new MagickImageCollection())
			{
				collection.Add(new MagickImage(SampleFiles.SnakewareJpg));
				collection.Add(new MagickImage(SampleFiles.SnakewareJpg));

				collection.Write(SampleFiles.OutputDirectory + "Snakeware.pdf");
			}
		}

		public static void CreatePDFFromSingleImage()
		{
			using (MagickImage image = new MagickImage(SampleFiles.SnakewareJpg))
			{
				image.Write(SampleFiles.OutputDirectory + "Snakeware.pdf");
			}
		}

		public static void ReadSinglePageFromPDF()
		{
			using (MagickImageCollection collection = new MagickImageCollection())
			{
				MagickReadSettings settings = new MagickReadSettings();
				settings.FrameIndex = 0; // First page
				settings.FrameCount = 1; // Number of pages

				collection.Read(SampleFiles.OutputDirectory + "Snakeware.pdf", settings);
			}
		}
	}
}
