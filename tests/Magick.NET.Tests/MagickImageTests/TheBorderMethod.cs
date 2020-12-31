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
        public class TheBorderMethod
        {
            [Fact]
            public void ShouldAddBorderOnAllSides()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    image.BorderColor = MagickColors.Green;
                    image.Border(3);

                    Assert.Equal(7, image.Width);
                    Assert.Equal(7, image.Height);
                    ColorAssert.Equal(MagickColors.Green, image, 1, 1);
                }
            }

            [Fact]
            public void ShouldOnlyAddVerticalBorderWhenOnlyWidthIsSpecified()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    image.Border(3, 0);

                    Assert.Equal(7, image.Width);
                    Assert.Equal(1, image.Height);
                }
            }

            [Fact]
            public void ShouldOnlyAddHorizontalBorderWhenOnlyHeightIsSpecified()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    image.Border(0, 3);

                    Assert.Equal(1, image.Width);
                    Assert.Equal(7, image.Height);
                }
            }
        }
    }
}
