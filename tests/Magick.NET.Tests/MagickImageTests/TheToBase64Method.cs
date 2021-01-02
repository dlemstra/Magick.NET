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

using System;
using ImageMagick;
using ImageMagick.Formats.Tiff;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheToBase64Method
        {
            [Fact]
            public void ShouldReturnBase64EncodedString()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var base64 = image.ToBase64();
                    Assert.NotNull(base64);
                    Assert.Equal(11704, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.NotNull(bytes);
                    Assert.Equal(8778, bytes.Length);
                }
            }

            [Fact]
            public void ShouldReturnBase64EncodedStringUsingTheSpecifiedFormat()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var base64 = image.ToBase64(MagickFormat.Jpeg);
                    Assert.NotNull(base64);
                    Assert.Equal(1140, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.NotNull(bytes);
                    Assert.Equal(853, bytes.Length);
                }
            }

            [Fact]
            public void ShouldReturnBase64EncodedStringUsingTheSpecifiedDefines()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var defines = new TiffWriteDefines
                    {
                        PreserveCompression = true,
                    };

                    var base64 = image.ToBase64(defines);
                    Assert.NotNull(base64);
                    Assert.Equal(10800, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.NotNull(bytes);
                    Assert.Equal(8100, bytes.Length);
                }
            }
        }
    }
}
