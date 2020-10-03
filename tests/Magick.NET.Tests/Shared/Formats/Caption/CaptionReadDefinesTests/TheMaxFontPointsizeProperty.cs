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
using ImageMagick.Formats.Caption;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class CaptionReadDefinesTests
    {
        public class TheMaxFontPointsizeProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings()
                {
                    Width = 100,
                    Height = 100,
                    Defines = new CaptionReadDefines()
                    {
                        MaxFontPointsize = 42,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read("caption:123", settings);

                    Assert.Equal("42", image.Settings.GetDefine(MagickFormat.Caption, "max-pointsize"));
                }
            }

            [Fact]
            public void ShouldLimitTheFontSize()
            {
                var settings = new MagickReadSettings()
                {
                    Width = 100,
                    Height = 80,
                    Defines = new CaptionReadDefines()
                    {
                        MaxFontPointsize = 15,
                    },
                };

                using (var image = new MagickImage("caption:testing 1 2 3", settings))
                {
                    ColorAssert.Equal(MagickColors.White, image, 32, 64);
                }
            }
        }
    }
}
