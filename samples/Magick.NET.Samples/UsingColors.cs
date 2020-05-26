// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;

namespace Magick.NET.Samples
{
    public static class UsingColorsSamples
    {
        public static void UsingColors()
        {
            using (var image = new MagickImage(SampleFiles.SnakewarePng))
            {
                image.TransparentChroma(MagickColors.Black, MagickColors.Blue);
                image.BackgroundColor = new ColorMono(true).ToMagickColor();

                // Q16 (Blue):
                image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, 0, Quantum.Max));
                image.TransparentChroma(new ColorRGB(0, 0, 0).ToMagickColor(), new ColorRGB(0, 0, Quantum.Max).ToMagickColor());
                image.BackgroundColor = new MagickColor("#00f");
                image.BackgroundColor = new MagickColor("#0000ff");
                image.BackgroundColor = new MagickColor("#00000000ffff");

                // With transparency (Red):
                image.BackgroundColor = new MagickColor(0, 0, Quantum.Max, 0);
                image.BackgroundColor = new MagickColor("#0000ff80");

                // Q8 (Green):
                image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, Quantum.Max, 0));
                image.TransparentChroma(new ColorRGB(0, 0, 0).ToMagickColor(), new ColorRGB(0, Quantum.Max, 0).ToMagickColor());
                image.BackgroundColor = new MagickColor("#0f0");
                image.BackgroundColor = new MagickColor("#00ff00");
            }
        }
    }
}
