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

#if WINDOWS_BUILD

using ImageMagick;
using ImageMagick.Formats.Pdf;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PdfReadDefinesTests
    {
        public class TheFitPageProperty
        {
            [Fact]
            public void ShouldSetTheDefineWhenValueIsSet()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        FitPage = new MagickGeometry(1, 2, 3, 4),
                    });

                    Assert.Equal("3x4+1+2", image.Settings.GetDefine(MagickFormat.Pdf, "fit-page"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenValueIsNotSet()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        FitPage = null,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Pdf, "fit-page"));
                }
            }

            [Fact]
            public void ShouldLimitTheDimensions()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PdfReadDefines
                    {
                        FitPage = new MagickGeometry(50, 40),
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.Coders.CartoonNetworkStudiosLogoAI, settings);

                    Assert.True(image.Width <= 50);
                    Assert.True(image.Height <= 40);
                }
            }
        }
    }
}

#endif