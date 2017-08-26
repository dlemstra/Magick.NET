// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Tests
{
    [TestClass]
    public class DdsWriteDefinesTests
    {
        [TestMethod]
        public void ClusterFit_NotSet_DefineIsNotSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines();

                image.Settings.SetDefines(defines);

                Assert.AreEqual(null, defines.ClusterFit);
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "cluster-fit"));
            }
        }

        [TestMethod]
        public void ClusterFit_IsSet_DefineIsSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines
                {
                    ClusterFit = true,
                };

                image.Settings.SetDefines(defines);

                Assert.AreEqual("True", image.Settings.GetDefine(MagickFormat.Dds, "cluster-fit"));
            }
        }

        [TestMethod]
        public void Compression_NotSet_DefineIsNotSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines();

                image.Settings.SetDefines(defines);

                Assert.AreEqual(null, defines.Compression);
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "compression"));
            }
        }

        [TestMethod]
        public void Compression_IsSet_DefineIsSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines
                {
                    Compression = DdsCompression.Dxt1,
                };

                image.Settings.SetDefines(defines);

                Assert.AreEqual("Dxt1", image.Settings.GetDefine(MagickFormat.Dds, "compression"));
            }
        }

        [TestMethod]
        public void Compression_NotSet_CompressionMethodIsDXT1()
        {
            using (IMagickImage input = new MagickImage(Files.Builtin.Logo))
            {
                using (IMagickImage output = WriteDds(input))
                {
                    Assert.AreEqual(CompressionMethod.DXT1, output.CompressionMethod);
                }
            }
        }

        [TestMethod]
        public void Compression_SetToNone_CompressionMethodIsNo()
        {
            using (IMagickImage input = new MagickImage(Files.Builtin.Logo))
            {
                input.Settings.SetDefines(new DdsWriteDefines()
                {
                    Compression = DdsCompression.None,
                });

                using (IMagickImage output = WriteDds(input))
                {
                    Assert.AreEqual(CompressionMethod.NoCompression, output.CompressionMethod);
                }
            }
        }

        [TestMethod]
        public void Compression_SetToDxt1_CompressionMethodIsDxt1()
        {
            using (IMagickImage input = new MagickImage(Files.Builtin.Logo))
            {
                input.Settings.SetDefines(new DdsWriteDefines()
                {
                    Compression = DdsCompression.Dxt1,
                });

                using (IMagickImage output = WriteDds(input))
                {
                    Assert.AreEqual(CompressionMethod.DXT1, output.CompressionMethod);
                }
            }
        }

        [TestMethod]
        public void FastMipMaps_NotSet_DefineIsNotSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines();

                image.Settings.SetDefines(defines);

                Assert.AreEqual(null, defines.FastMipmaps);
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "fast-mipmaps"));
            }
        }

        [TestMethod]
        public void FastMipMaps_IsSet_DefineIsSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines
                {
                    FastMipmaps = true,
                };

                image.Settings.SetDefines(defines);

                Assert.AreEqual("True", image.Settings.GetDefine(MagickFormat.Dds, "fast-mipmaps"));
            }
        }

        [TestMethod]
        public void MipMaps_NotSet_DefineIsNotSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines();

                image.Settings.SetDefines(defines);

                Assert.AreEqual(null, defines.Mipmaps);
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "mipmaps"));
            }
        }

        [TestMethod]
        public void MipMaps_IsSet_DefineIsSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines
                {
                    Mipmaps = 2,
                };

                image.Settings.SetDefines(defines);

                Assert.AreEqual("2", image.Settings.GetDefine(MagickFormat.Dds, "mipmaps"));
            }
        }

        [TestMethod]
        public void WeightByAlpha_NotSet_DefineIsNotSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines();

                image.Settings.SetDefines(defines);

                Assert.AreEqual(null, defines.WeightByAlpha);
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "weight-by-alpha"));
            }
        }

        [TestMethod]
        public void WeightByAlpha_IsSet_DefineIsSet()
        {
            using (IMagickImage image = new MagickImage())
            {
                var defines = new DdsWriteDefines
                {
                    WeightByAlpha = false,
                };

                image.Settings.SetDefines(defines);

                Assert.AreEqual("False", image.Settings.GetDefine(MagickFormat.Dds, "weight-by-alpha"));
            }
        }

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
    }
}
