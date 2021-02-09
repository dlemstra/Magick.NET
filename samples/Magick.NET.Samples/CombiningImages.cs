// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;

namespace Magick.NET.Samples
{
    public static class CombiningImagesSamples
    {
        public static void MergeMultipleImages()
        {
            using (var images = new MagickImageCollection())
            {
                // Add the first image
                var first = new MagickImage(SampleFiles.SnakewarePng);
                images.Add(first);

                // Add the second image
                var second = new MagickImage(SampleFiles.SnakewarePng);
                images.Add(second);

                // Create a mosaic from both images
                using (var result = images.Mosaic())
                {
                    // Save the result
                    result.Write(SampleFiles.OutputDirectory + "Mosaic.png");
                }
            }
        }

        public static void CreateAnimatedGif()
        {
            using (var collection = new MagickImageCollection())
            {
                // Add first image and set the animation delay to 100ms
                collection.Add(SampleFiles.SnakewarePng);
                collection[0].AnimationDelay = 100;

                // Add second image, set the animation delay to 100ms and flip the image
                collection.Add(SampleFiles.SnakewarePng);
                collection[1].AnimationDelay = 100;
                collection[1].Flip();

                // Optionally reduce colors
                var settings = new QuantizeSettings();
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
