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
    public partial class MagickSettingsTests
    {
        public class TheTextAntiAliasProperty
        {
            [Fact]
            public void ShouldDisableTextAntialiasingWhenFalse()
            {
                using (var image = new MagickImage(MagickColors.Azure, 300, 300))
                {
                    Assert.True(image.Settings.TextAntiAlias);

                    image.Settings.TextAntiAlias = false;
                    image.Settings.FontPointsize = 100;
                    image.Annotate("TEST", Gravity.Center);

                    ColorAssert.Equal(MagickColors.Azure, image, 158, 125);
                    ColorAssert.Equal(MagickColors.Black, image, 158, 126);
                    ColorAssert.Equal(MagickColors.Azure, image, 209, 127);
                    ColorAssert.Equal(MagickColors.Black, image, 209, 128);
                }
            }
        }
    }
}
