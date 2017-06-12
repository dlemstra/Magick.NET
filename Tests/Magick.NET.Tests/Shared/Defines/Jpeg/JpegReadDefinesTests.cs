//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

using ImageMagick;
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class JpegReadDefinesTests
    {
        [TestMethod]
        public void Test_Empty()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new JpegReadDefines()
                {
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "block-smoothing"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "colors"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "dct-method"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "fancy-upsampling"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "size"));
                Assert.AreEqual(null, image.Settings.GetDefine("profile:skip"));
            }
        }

        [TestMethod]
        public void Test_BlockSmoothing_DctMethod_FancyUpsampling()
        {
            MagickReadSettings settings = new MagickReadSettings()
            {
                Defines = new JpegReadDefines()
                {
                    BlockSmoothing = true,
                    DctMethod = DctMethod.Slow,
                    FancyUpsampling = false,
                    SkipProfiles = (ProfileTypes)64
                }
            };

            using (IMagickImage image = new MagickImage())
            {
                image.Read(Files.ImageMagickJPG, settings);

                Assert.AreEqual("True", image.Settings.GetDefine(MagickFormat.Jpeg, "block-smoothing"));
                Assert.AreEqual("Slow", image.Settings.GetDefine(MagickFormat.Jpeg, "dct-method"));
                Assert.AreEqual("False", image.Settings.GetDefine(MagickFormat.Jpeg, "fancy-upsampling"));
                Assert.AreEqual(null, image.Settings.GetDefine("profile:skip"));
            }
        }

        [TestMethod]
        public void Test_Colors_Size()
        {
            MagickReadSettings settings = new MagickReadSettings()
            {
                Defines = new JpegReadDefines()
                {
                    Colors = 100,
                    Size = new MagickGeometry(61, 59)
                }
            };

            using (IMagickImage image = new MagickImage())
            {
                image.Read(Files.ImageMagickJPG, settings);

                Assert.IsTrue(image.TotalColors <= 100);
                Assert.AreEqual(100, image.TotalColors, 1);
                Assert.AreEqual(62, image.Width);
                Assert.AreEqual(59, image.Height);
            }
        }

        [TestMethod]
        public void Test_SkipProfiles()
        {
            MagickReadSettings settings = new MagickReadSettings()
            {
                Defines = new JpegReadDefines()
                {
                    SkipProfiles = ProfileTypes.Iptc | ProfileTypes.Icc
                }
            };

            using (IMagickImage image = new MagickImage())
            {
                image.Read(Files.FujiFilmFinePixS1ProJPG);
                Assert.IsNotNull(image.GetIptcProfile());

                image.Read(Files.FujiFilmFinePixS1ProJPG, settings);
                Assert.IsNull(image.GetIptcProfile());
                Assert.AreEqual("Icc,Iptc", image.Settings.GetDefine("profile:skip"));
            }
        }
    }
}
