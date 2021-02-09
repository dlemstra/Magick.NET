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

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSteganoMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenWatermarkIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("watermark", () => image.Stegano(null));
                }
            }

            [Fact]
            public void ShouldAddDigitalWatermark()
            {
                using (var message = new MagickImage("label:Magick.NET is the best!", 200, 20))
                {
                    using (var image = new MagickImage(Files.Builtin.Wizard))
                    {
                        image.Stegano(message);

                        using (var temporaryFile = new TemporaryFile(".png"))
                        {
                            image.Write(temporaryFile);

                            var settings = new MagickReadSettings
                            {
                                Format = MagickFormat.Stegano,
                                Width = 200,
                                Height = 20,
                            };

                            using (var hiddenMessage = new MagickImage(temporaryFile.FullName, settings))
                            {
                                Assert.InRange(message.Compare(hiddenMessage, ErrorMetric.RootMeanSquared), 0, 0.001);
                            }
                        }
                    }
                }
            }
        }
    }
}
