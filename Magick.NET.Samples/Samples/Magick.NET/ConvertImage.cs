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
using System.IO;
using ImageMagick;

namespace RootNamespace.Samples.MagickNET
{
	public static class ConvertImageSamples
	{
		public static void ConvertImageFromOneFormatToAnother()
		{
			// Write to file
			using (MagickImage image = new MagickImage(SampleFiles.SnakewareGif))
			{
				image.Write(SampleFiles.OutputDirectory + "Snakeware.jpg");
			}

			// Write to stream
			MagickReadSettings settings = new MagickReadSettings();
			settings.Width = 800;
			settings.Height = 600;

			using (MemoryStream memStream = new MemoryStream())
			{
				using (MagickImage image = new MagickImage("xc:purple", settings))
				{
					image.Format = MagickFormat.Png;
					image.Write(memStream);
				}
			}

			// Convert to byte array
			using (MagickImage image = new MagickImage(SampleFiles.SnakewarePng))
			{
				image.Format = MagickFormat.Jpeg;
				byte[] data = image.ToByteArray();
			}
		}

		public static void ConvertCmykToRgb()
		{
			// Uses sRGB.icm, eps/pdf produce better result when you set this before loading.
			MagickReadSettings settings = new MagickReadSettings();
			settings.ColorSpace = ColorSpace.RGB;

			using (MagickImage image = new MagickImage())
			{
				image.AddProfile(ColorProfile.SRGB);
				image.Read(SampleFiles.SnakewareEps, settings);
				image.Write("Snakeware.tiff");
			}

			// Convert CMYK jpeg to RGB.
			using (MagickImage image = new MagickImage(SampleFiles.SnakewareJpg))
			{
				image.AddProfile(ColorProfile.SRGB);
				image.ColorSpace = ColorSpace.sRGB;
				image.Write(SampleFiles.OutputDirectory + "Snakeware.png");
			}

			// Use custom color profile
			using (MagickImage image = new MagickImage(SampleFiles.SnakewareJpg))
			{
				image.AddProfile(new ColorProfile(SampleFiles.YourProfileIcc));
				image.ColorSpace = ColorSpace.sRGB;
				image.Write(SampleFiles.OutputDirectory + "Snakeware.tiff");
			}
		}
	}
}
