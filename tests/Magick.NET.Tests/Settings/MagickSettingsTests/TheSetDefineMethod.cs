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
        public class TheSetDefineMethod
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefine(MagickFormat.Jpeg, "optimize-coding", "test");

                    Assert.Equal("test", image.Settings.GetDefine(MagickFormat.Jpg, "optimize-coding"));
                    Assert.Equal("test", image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
                }
            }

            [Fact]
            public void ShouldChangeTheBooleanToString()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefine(MagickFormat.Jpeg, "optimize-coding", true);

                    Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
                }
            }

            [Fact]
            public void ShouldUseTheSpecifiedName()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefine("profile:skip", "ICC");
                    Assert.Equal("ICC", image.Settings.GetDefine("profile:skip"));
                }
            }
        }
    }
}
