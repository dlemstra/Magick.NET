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
        public class TheTextKerningProperty
        {
            [Fact]
            public void ShouldDefaultToZero()
            {
                using (var image = new MagickImage())
                {
                    Assert.Equal(0, image.Settings.TextKerning);
                }
            }

            [Fact]
            public void ShouldBeUsedWhenRenderingText()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.TextKerning = 10;
                    image.Read("label:First");

                    Assert.Equal(65, image.Width);
                    Assert.Equal(15, image.Height);

                    image.Settings.TextKerning = 20;
                    image.Read("label:First");

                    Assert.Equal(105, image.Width);
                    Assert.Equal(15, image.Height);
                }
            }
        }
    }
}
