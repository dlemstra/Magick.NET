//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
        // Add the first image
        MagickImage first = new MagickImage(SampleFiles.SnakewarePng);
        images.Add(first);

        // Add the second image
        MagickImage second = new MagickImage(SampleFiles.SnakewarePng);
        images.Add(second);

        // Create a mosaic from both images
        using (IMagickImage result = images.Mosaic())
        {
          // Save the result
          result.Write(SampleFiles.OutputDirectory + "Mosaic.png");
        }
      }
    }

    public static void CreateAnimatedGif()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        // Add first image and set the animation delay to 100ms
        collection.Add(SampleFiles.SnakewarePng);
        collection[0].AnimationDelay = 100;

        // Add second image, set the animation delay to 100ms and flip the image
        collection.Add(SampleFiles.SnakewarePng);
        collection[1].AnimationDelay = 100;
        collection[1].Flip();

        // Optionally reduce colors
        QuantizeSettings settings = new QuantizeSettings();
        settings.Colors = 256;
        collection.Quantize(settings);

        // Optionally optimize the images (images should have the same size).
        collection.Optimize();

        // Save gif
        collection.Write(SampleFiles.OutputDirectory + "Snakeware.Animated.gif");
      }
    }
  }
}
