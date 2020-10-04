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
    public partial class MagickSettingsTests
    {
        public class TheFontFamilyProperty
        {
            [Fact]
            public void ShouldChangeTheFont()
            {
                using (var image = new MagickImage())
                {
                    Assert.Null(image.Settings.FontFamily);
                    Assert.Equal(0, image.Settings.FontPointsize);
                    Assert.Equal(FontStyleType.Undefined, image.Settings.FontStyle);
                    Assert.Equal(FontWeight.Undefined, image.Settings.FontWeight);

                    image.Settings.FontFamily = "Courier New";
                    image.Settings.FontPointsize = 40;
                    image.Settings.FontStyle = FontStyleType.Oblique;
                    image.Settings.FontWeight = FontWeight.ExtraBold;
                    image.Read("label:Test");

                    Assert.Equal(98, image.Width);
                    Assert.Equal(48, image.Height);
                    ColorAssert.Equal(MagickColors.Black, image, 16, 16);
                }
            }
        }
    }
}
