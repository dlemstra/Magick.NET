// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
        public class TheAlphaMethod
        {
            [Fact]
            public void ShouldMakeImageTransparent()
            {
                using (var image = new MagickImage(Files.Builtin.Wizard))
                {
                    Assert.False(image.HasAlpha);

                    image.Alpha(AlphaOption.Transparent);

                    Assert.True(image.HasAlpha);
                    ColorAssert.Equal(new MagickColor("#fff0"), image, 0, 0);
                }
            }

            [Fact]
            public void ShouldUseTheBackgroundColor()
            {
                using (var image = new MagickImage(Files.Builtin.Wizard))
                {
                    image.Alpha(AlphaOption.Transparent);

                    image.BackgroundColor = new MagickColor("red");
                    image.Alpha(AlphaOption.Background);
                    image.Alpha(AlphaOption.Off);

                    Assert.False(image.HasAlpha);
                    ColorAssert.Equal(new MagickColor(Quantum.Max, 0, 0), image, 0, 0);
                }
            }
        }
    }
}
