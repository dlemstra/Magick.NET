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

            [Fact]
            public void ShouldTrimTheBackgroundHorizontally()
            {
                using (var image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    image.Extent(3, 3, Gravity.Center, MagickColors.White);

                    image.Trim(Gravity.East, Gravity.West);

                    Assert.Equal(1, image.Width);
                    Assert.Equal(3, image.Height);

                    ColorAssert.Equal(MagickColors.White, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Red, image, 0, 1);
                    ColorAssert.Equal(MagickColors.White, image, 0, 2);
                }
            }

            [Fact]
            public void ShouldTrimTheBackgroundVertically()
            {
                using (var image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    image.Extent(3, 3, Gravity.Center, MagickColors.White);

                    image.Trim(Gravity.North, Gravity.South);

                    Assert.Equal(3, image.Width);
                    Assert.Equal(1, image.Height);

                    ColorAssert.Equal(MagickColors.White, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Red, image, 1, 0);
                    ColorAssert.Equal(MagickColors.White, image, 2, 0);
                }
            }

            [Theory]
            [InlineData(Gravity.North, 3, 2, 1, 0)]
            [InlineData(Gravity.Northeast, 2, 2, 1, 0)]
            [InlineData(Gravity.East, 2, 3, 1, 1)]
            [InlineData(Gravity.Southeast, 2, 2, 1, 1)]
            [InlineData(Gravity.South, 3, 2, 1, 1)]
            [InlineData(Gravity.Southwest, 2, 2, 0, 1)]
            [InlineData(Gravity.West, 2, 3, 0, 1)]
            [InlineData(Gravity.Northwest, 2, 2, 0, 0)]
            public void ShouldTrimTheSpecifiedEdge(Gravity edge, int width, int height, int redX, int redY)
            {
                using (var image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    image.Extent(3, 3, Gravity.Center, MagickColors.White);

                    image.Trim(edge);

                    Assert.Equal(width, image.Width);
                    Assert.Equal(height, image.Height);

                    ColorAssert.Equal(MagickColors.Red, image, redX, redY);
                }
            }
        }
    }
}
