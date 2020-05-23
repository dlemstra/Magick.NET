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
using System;
using System.IO;

namespace Magick.NET.Samples
{
    public static class DrawSamples
    {
        public static void DrawText()
        {
            using (var image = new MagickImage(new MagickColor("#ff00ff"), 512, 128))
            {
                new Drawables()
                  // Draw text on the image
                  .FontPointSize(72)
                  .Font("Comic Sans")
                  .StrokeColor(new MagickColor("yellow"))
                  .FillColor(MagickColors.Orange)
                  .TextAlignment(TextAlignment.Center)
                  .Text(256, 64, "Magick.NET")
                  // Add an ellipse
                  .StrokeColor(new MagickColor(0, Quantum.Max, 0))
                  .FillColor(MagickColors.SaddleBrown)
                  .Ellipse(256, 96, 192, 8, 0, 360)
                  .Draw(image);
            }
        }

        public static void AddTextToExistingImage()
        {
            var pathToBackgroundImage = SampleFiles.SampleBackground;
            var pathToNewImage = Path.Combine(SampleFiles.OutputDirectory, "2FD-WithAddedText.jpg");
            var textToWrite = "Insert This Text Into Image";

            // These settings will create a new caption
            // which automatically resizes the text to best
            // fit within the box.

            var readSettings = new MagickReadSettings()
            {
                Font = "Calibri",
                TextGravity = Gravity.Center,
                BackgroundColor = MagickColors.Transparent,
                Height = 250, // height of text box
                Width = 680 // width of text box
            };

            using (var image = new MagickImage(pathToBackgroundImage))
            {
                using (var caption = new MagickImage($"caption:{textToWrite}", readSettings))
                {
                    // Add the caption layer on top of the background image
                    // at position 590,450
                    image.Composite(caption, 590, 450, CompositeOperator.Over);

                    image.Write(pathToNewImage);
                }
            }
        }
    }
}
