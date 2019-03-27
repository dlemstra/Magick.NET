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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Defines.Jpeg.JpegWriteDefinesTests
{
    public partial class JpegWriteDefinesTests
    {
        [TestClass]
        public class TheSamplingFactorsProperty
        {
            [TestMethod]
            public void ShouldSetTheDefine()
            {
                var defines = new JpegWriteDefines()
                {
                    SamplingFactors = new MagickGeometry[]
                    {
                        new MagickGeometry(5, 10),
                        new MagickGeometry(15, 20),
                    },
                };

                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.AreEqual("5x10,15x20", image.Settings.GetDefine(MagickFormat.Jpeg, "sampling-factor"));
                }
            }

            [TestMethod]
            public void ShouldWriteJpegWithTheCorrectSamplingFactor()
            {
                var defines = new JpegWriteDefines()
                {
                    SamplingFactors = new MagickGeometry[]
                    {
                        new MagickGeometry(2, 2),
                        new MagickGeometry(1, 1),
                        new MagickGeometry(1, 1),
                    },
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
        }
    }
}
