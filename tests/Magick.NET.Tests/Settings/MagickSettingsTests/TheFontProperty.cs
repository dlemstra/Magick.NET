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
    public partial class MagickSettingsTests
    {
        public class TheFontProperty
        {
            [Fact]
            public void ShouldSetTheFontWhenReadingImage()
            {
                using (var image = new MagickImage())
                {
                    Assert.Null(image.Settings.Font);

                    image.Settings.Font = "Courier New";
                    image.Settings.FontPointsize = 40;
                    image.Read("pango:Test");

                    // Different result on MacOS
                    if (image.Width != 40)
                    {
                        Assert.Equal(128, image.Width);
                        Assert.Contains(image.Height, new[] { 58, 62 });
                        ColorAssert.Equal(MagickColors.Black, image, 26, 22);
                    }
                }
            }
        }
    }
}
