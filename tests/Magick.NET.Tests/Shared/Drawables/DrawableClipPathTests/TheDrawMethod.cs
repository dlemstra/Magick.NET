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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DrawableClipPathTests
    {
        public class TheDrawMethod
        {
            [Fact]
            public void ShouldSetTheClipMask()
            {
                using (var image = new MagickImage(MagickColors.Thistle, 100, 100))
                {
                    SetClipMask(image, 20, 20, 80, 80);

                    new Drawables()
                        .StrokeColor(MagickColors.Red)
                        .FillColor(MagickColors.Green)
                        .Rectangle(0, 0, 99, 99)
                        .Draw(image);

                    SetClipMask(image, 40, 40, 60, 60);

                    new Drawables()
                        .StrokeColor(MagickColors.Red)
                        .StrokeWidth(10)
                        .Line(0, 0, 99, 99)
                        .Draw(image);

                    ColorAssert.Equal(MagickColors.Thistle, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Green, image, 20, 20);
                    ColorAssert.Equal(MagickColors.Green, image, 80, 80);
                    ColorAssert.Equal(MagickColors.Red, image, 50, 50);
                }
            }

            private void SetClipMask(MagickImage image, int x0, int y0, int x1, int y1)
            {
                var drawables = new Drawables();

                var paths = drawables.Paths()
                    .MoveToAbs(x0, y0)
                    .LineToAbs(x1, y0)
                    .LineToAbs(x1, y1)
                    .LineToAbs(x0, y1)
                    .LineToAbs(x0, y0);

                var pathId = nameof(SetClipMask);

                drawables
                    .PushClipPath(pathId)
                    .Path(paths)
                    .PopClipPath()
                    .ClipPath(pathId)
                    .Draw(image);
            }
        }
    }
}
