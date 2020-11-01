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
    public partial class MagickImageTests
    {
        public class TheTrimMethod
        {
            [Fact]
            public void ShouldTrimTheBackground()
            {
                using (var image = new MagickImage("xc:fuchsia", 50, 50))
                {
                    ColorAssert.Equal(MagickColors.Fuchsia, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Fuchsia, image, 49, 49);

                    image.Extent(100, 60, Gravity.Center, MagickColors.Gold);

                    Assert.Equal(100, image.Width);
                    Assert.Equal(60, image.Height);
                    ColorAssert.Equal(MagickColors.Gold, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Fuchsia, image, 50, 30);

                    image.Trim();

                    Assert.Equal(50, image.Width);
                    Assert.Equal(50, image.Height);
                    ColorAssert.Equal(MagickColors.Fuchsia, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Fuchsia, image, 49, 49);
                }
            }

            [Fact]
            public void ShouldTrimTheBackgroundWithThePercentage()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.BackgroundColor = MagickColors.Black;
                    image.Rotate(10);

                    image.Trim(new Percentage(5));
#if Q8 || Q16
                    Assert.Equal(558, image.Width);
                    Assert.Equal(318, image.Height);
#else
                    Assert.Equal(560, image.Width);
                    Assert.Equal(320, image.Height);
#endif
                }
            }
        }
    }
}
