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
        public class TheTrimVerticalMethod
        {
            [Fact]
            public void ShouldTrimTheBackgroundVertically()
            {
                using (var image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    image.Extent(3, 3, Gravity.Center, MagickColors.White);

                    image.TrimVertical();

                    Assert.Equal(3, image.Width);
                    Assert.Equal(1, image.Height);

                    ColorAssert.Equal(MagickColors.White, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Red, image, 1, 0);
                    ColorAssert.Equal(MagickColors.White, image, 2, 0);
                }
            }
        }
    }
}
