//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System.IO;
using ImageMagick;
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class DdsWriteDefinesTests
    {
        private static IMagickImage WriteDds(IMagickImage input)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                input.Format = MagickFormat.Dds;
                input.Write(memStream);
                memStream.Position = 0;

                return new MagickImage(memStream);
            }
        }

        [TestMethod]
        public void Test_Empty()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new DdsWriteDefines()
                {
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "cluster-fit"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "compression"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "mipmaps"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "weight-by-alpha"));
            }
        }

        [TestMethod]
        public void Test_ClusterFit_Mipmaps_WeightByAlpha()
        {
            DdsWriteDefines defines = new DdsWriteDefines()
            {
                ClusterFit = true,
                Mipmaps = 0,
                WeightByAlpha = false,
            };

            using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
            {
                image.Settings.SetDefines(defines);

                Assert.AreEqual("True", image.Settings.GetDefine(MagickFormat.Dds, "cluster-fit"));
                Assert.AreEqual("0", image.Settings.GetDefine(MagickFormat.Dds, "mipmaps"));
                Assert.AreEqual("False", image.Settings.GetDefine(MagickFormat.Dds, "weight-by-alpha"));
            }
        }

        [TestMethod]
        public void Test_Compression()
        {
            DdsWriteDefines defines = new DdsWriteDefines()
            {
                Compression = DdsCompression.None
            };

            using (IMagickImage input = new MagickImage(Files.Builtin.Logo))
            {
                using (IMagickImage output = WriteDds(input))
                {
                    Assert.AreEqual(CompressionMethod.DXT1, output.CompressionMethod);
                }

                input.Settings.SetDefines(defines);

                using (IMagickImage output = WriteDds(input))
                {
                    Assert.AreEqual(CompressionMethod.NoCompression, output.CompressionMethod);
                }
            }
        }
    }
}
