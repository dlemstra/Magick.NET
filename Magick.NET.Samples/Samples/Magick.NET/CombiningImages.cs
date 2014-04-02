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
	public static class CombiningImagesSamples
	{
		public static void MergeMultipleImages()
		{
			using (MagickImageCollection images = new MagickImageCollection())
			{
				MagickImage first = new MagickImage(SampleFiles.SnakewarePng);
				images.Add(first);

				MagickImage second = new MagickImage(SampleFiles.SnakewarePng);
				images.Add(second);

				using (MagickImage result = images.Mosaic())
				{
					result.Write(SampleFiles.OutputDirectory + "Mosaic.png");
				}
			}
		}

		public static void CreateAnimatedGif()
		{
			using (MagickImageCollection collection = new MagickImageCollection())
			{
				collection.Add("Snakeware.png");
				collection[0].AnimationDelay = 100;

				collection.Add("Snakeware.png");
				collection[1].AnimationDelay = 100;
				collection[1].Flip();

				// Optionally reduce colors
				QuantizeSettings settings = new QuantizeSettings();
				settings.Colors = 256;
				collection.Quantize(settings);

				collection.Optimize();

				collection.Write(SampleFiles.OutputDirectory + "Snakeware.Animated.gif");
			}
		}
	}
}
