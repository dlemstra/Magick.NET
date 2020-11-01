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

using System.IO;
using ImageMagick;
using ImageMagick.Formats.Jpeg;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegWriteDefinesTests
    {
        public class TheArithmeticCodingProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var defines = new JpegWriteDefines
                {
                    ArithmeticCoding = false,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Jpeg, "arithmetic-coding"));
                }
            }

            [Fact]
            public void ShouldEncodeTheImagArarithmetic()
            {
                var defines = new JpegWriteDefines
                {
                    ArithmeticCoding = true,
                };

                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    using (var memStream = new MemoryStream())
                    {
                        input.Write(memStream, defines);

                        Assert.Equal("true", input.Settings.GetDefine(MagickFormat.Jpeg, "arithmetic-coding"));

                        memStream.Position = 0;
                        using (var output = new MagickImage(memStream))
                        {
                            var coding = output.GetAttribute("jpeg:coding");
                            Assert.Equal("arithmetic", coding);
                        }
                    }
                }
            }
        }
    }
}
