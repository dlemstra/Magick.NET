// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Defines.Jpeg.JpegWriteDefinesTests
{
    public partial class JpegWriteDefinesTests
    {
        [TestClass]
        public class TheSamplingFactorProperty
        {
            [TestMethod]
            public void ShouldSetTheDefine()
            {
                AssertSetDefine("4x2,1x1,1x1", SamplingFactor.Ratio410);
                AssertSetDefine("4x1,1x1,1x1", SamplingFactor.Ratio411);
                AssertSetDefine("2x2,1x1,1x1", SamplingFactor.Ratio420);
                AssertSetDefine("2x1,1x1,1x1", SamplingFactor.Ratio422);
                AssertSetDefine("1x2,1x1,1x1", SamplingFactor.Ratio440);
                AssertSetDefine("1x1,1x1,1x1", SamplingFactor.Ratio444);
            }

            [TestMethod]
            public void ShouldWriteJpegWithTheCorrectSamplingFactor()
            {
                var defines = new JpegWriteDefines()
                {
                    SamplingFactor = SamplingFactor.Ratio420,
                };

                using (IMagickImage input = new MagickImage(Files.Builtin.Logo))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        input.Write(memoryStream, defines);

                        memoryStream.Position = 0;
                        using (IMagickImage output = new MagickImage(memoryStream))
                        {
                            output.Read(memoryStream);

                            Assert.AreEqual("2x2,1x1,1x1", output.GetAttribute("jpeg:sampling-factor"));
                        }
                    }
                }
            }

            private static void AssertSetDefine(string expected, SamplingFactor samplingFactor)
            {
                var defines = new JpegWriteDefines()
                {
                    SamplingFactor = samplingFactor,
                };

                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.AreEqual(expected, image.Settings.GetDefine(MagickFormat.Jpeg, "sampling-factor"));
                }
            }
        }
    }
}
