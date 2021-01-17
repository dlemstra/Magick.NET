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
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PsdReadDefinesTests
    {
        public class TheAlphaUnblendProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PsdReadDefines
                    {
                        AlphaUnblend = false,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.Coders.PlayerPSD, settings);

                    var define = image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend");
                    Assert.Equal("false", define);
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenTheValueIsTrue()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PsdReadDefines
                    {
                        AlphaUnblend = true,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend"));
                }
            }
        }
    }
}
