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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheAdaptiveResizeMethod
        {
            [Fact]
            public void ShouldNotEnlargeTheImage()
            {
                using (var image = new MagickImage(MagickColors.Black, 512, 1))
                {
                    image.AdaptiveResize(512, 512);

                    Assert.Equal(1, image.Height);
                }
            }

            [Fact]
            public void ShouldEnlargeTheImageWhenAspectRatioIsIgnored()
            {
                using (var image = new MagickImage(MagickColors.Black, 512, 1))
                {
                    var geometry = new MagickGeometry(512, 512)
                    {
                        IgnoreAspectRatio = true,
                    };

                    image.AdaptiveResize(geometry);

                    Assert.Equal(512, image.Height);
                }
            }

            [Fact]
            public void ShouldResizeTheImage()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    image.AdaptiveResize(100, 80);

                    Assert.Equal(80, image.Width);
                    Assert.Equal(80, image.Height);

                    ColorAssert.Equal(new MagickColor("#347bbd"), image, 23, 42);
                    ColorAssert.Equal(new MagickColor("#a8dff8"), image, 42, 42);
                }
            }
        }
    }
}
