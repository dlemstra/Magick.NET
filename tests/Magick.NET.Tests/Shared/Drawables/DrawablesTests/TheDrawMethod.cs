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

using System;
using System.Collections;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DrawablesTests
    {
        public class TheDrawMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                Assert.Throws<ArgumentNullException>("image", () =>
                {
                    new Drawables().Draw(null);
                });
            }

            [Fact]
            public void ShouldDrawTheDrawables()
            {
                using (var image = new MagickImage(MagickColors.Fuchsia, 100, 100))
                {
                    var drawables = new Drawables()
                      .FillColor(MagickColors.Red)
                      .Rectangle(10, 10, 90, 90);

                    drawables.Draw(image);

                    ColorAssert.Equal(MagickColors.Fuchsia, image, 9, 9);
                    ColorAssert.Equal(MagickColors.Red, image, 10, 10);
                    ColorAssert.Equal(MagickColors.Red, image, 90, 90);
                    ColorAssert.Equal(MagickColors.Fuchsia, image, 91, 91);

                    image.Draw(new Drawables()
                      .FillColor(MagickColors.Green)
                      .Rectangle(15, 15, 85, 85));

                    ColorAssert.Equal(MagickColors.Fuchsia, image, 9, 9);
                    ColorAssert.Equal(MagickColors.Red, image, 10, 10);
                    ColorAssert.Equal(MagickColors.Green, image, 15, 15);
                    ColorAssert.Equal(MagickColors.Green, image, 85, 85);
                    ColorAssert.Equal(MagickColors.Red, image, 90, 90);
                    ColorAssert.Equal(MagickColors.Fuchsia, image, 91, 91);
                }
            }
        }
    }
}