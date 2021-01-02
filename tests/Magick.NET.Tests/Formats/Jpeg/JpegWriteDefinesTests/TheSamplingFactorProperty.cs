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

using System.IO;
using ImageMagick;
using ImageMagick.Formats.Jpeg;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegWriteDefinesTests
    {
        public class TheSamplingFactorProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                AssertSetDefine("4x2,1x1,1x1", JpegSamplingFactor.Ratio410);
                AssertSetDefine("4x1,1x1,1x1", JpegSamplingFactor.Ratio411);
                AssertSetDefine("2x2,1x1,1x1", JpegSamplingFactor.Ratio420);
                AssertSetDefine("2x1,1x1,1x1", JpegSamplingFactor.Ratio422);
                AssertSetDefine("1x2,1x1,1x1", JpegSamplingFactor.Ratio440);
                AssertSetDefine("1x1,1x1,1x1", JpegSamplingFactor.Ratio444);
            }

            [Fact]
            public void ShouldWriteJpegWithTheCorrectSamplingFactor()
            {
                var defines = new JpegWriteDefines
                {
                    SamplingFactor = JpegSamplingFactor.Ratio420,
                };

                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        input.Write(memoryStream, defines);

                        memoryStream.Position = 0;
                        using (var output = new MagickImage(memoryStream))
                        {
                            output.Read(memoryStream);

                            Assert.Equal("2x2,1x1,1x1", output.GetAttribute("jpeg:sampling-factor"));
                        }
                    }
                }
            }

            private static void AssertSetDefine(string expected, JpegSamplingFactor samplingFactor)
            {
                var defines = new JpegWriteDefines
                {
                    SamplingFactor = samplingFactor,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal(expected, image.Settings.GetDefine(MagickFormat.Jpeg, "sampling-factor"));
                }
            }
        }
    }
}
