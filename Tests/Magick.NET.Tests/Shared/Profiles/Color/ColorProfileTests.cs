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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public sealed class ColorProfileTests
    {
        [TestMethod]
        public void Test_ColorSpace()
        {
            Assert.AreEqual(ColorSpace.sRGB, ColorProfile.AdobeRGB1998.ColorSpace);
            Assert.AreEqual(ColorSpace.sRGB, ColorProfile.AppleRGB.ColorSpace);
            Assert.AreEqual(ColorSpace.CMYK, ColorProfile.CoatedFOGRA39.ColorSpace);
            Assert.AreEqual(ColorSpace.sRGB, ColorProfile.ColorMatchRGB.ColorSpace);
            Assert.AreEqual(ColorSpace.sRGB, ColorProfile.SRGB.ColorSpace);
            Assert.AreEqual(ColorSpace.CMYK, ColorProfile.USWebCoatedSWOP.ColorSpace);
        }

        [TestMethod]
        public void Test_EmbeddedResources()
        {
            TestEmbeddedResource(ColorProfile.AdobeRGB1998);
            TestEmbeddedResource(ColorProfile.AppleRGB);
            TestEmbeddedResource(ColorProfile.CoatedFOGRA39);
            TestEmbeddedResource(ColorProfile.ColorMatchRGB);
            TestEmbeddedResource(ColorProfile.SRGB);
            TestEmbeddedResource(ColorProfile.USWebCoatedSWOP);
        }

        [TestMethod]
        public void Test_ICM()
        {
            using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
            {
                ColorProfile profile = image.GetColorProfile();
                Assert.IsNull(profile);

                image.AddProfile(new ImageProfile("icm", ColorProfile.SRGB.ToByteArray()));
                TestProfile(image.GetColorProfile(), "icm");
            }
        }

        [TestMethod]
        public void Test_Info()
        {
            using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.AddProfile(ColorProfile.USWebCoatedSWOP);

                Assert.AreEqual("U.S. Web Coated (SWOP) v2", image.GetAttribute("icc:description"));
                Assert.AreEqual("U.S. Web Coated (SWOP) v2", image.GetAttribute("icc:manufacturer"));
                Assert.AreEqual("U.S. Web Coated (SWOP) v2", image.GetAttribute("icc:model"));
                Assert.AreEqual("Copyright 2000 Adobe Systems, Inc.", image.GetAttribute("icc:copyright"));
            }
        }

        [TestMethod]
        public void Test_Remove()
        {
            using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
            {
                ColorProfile profile = image.GetColorProfile();
                Assert.IsNull(profile);

                image.AddProfile(ColorProfile.SRGB);

                Assert.IsNull(image.GetProfile("icm"));

                profile = image.GetColorProfile();
                Assert.IsNotNull(profile);

                image.RemoveProfile(profile.Name);

                profile = image.GetColorProfile();
                Assert.IsNull(profile);
            }
        }

        [TestMethod]
        public void Test_WithImage()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.AddProfile(ColorProfile.USWebCoatedSWOP);
                ExceptionAssert.Throws<MagickCacheErrorException>(() =>
                {
                    image.ColorSpace = ColorSpace.CMYK;
                });
                image.Read(Files.SnakewarePNG);

                ColorProfile profile = image.GetColorProfile();
                Assert.IsNull(profile);

                image.AddProfile(ColorProfile.SRGB);
                TestProfile(image.GetColorProfile(), "icc");
            }
        }

        private static void TestEmbeddedResource(ColorProfile profile)
        {
            Assert.IsNotNull(profile);
            Assert.AreEqual("icc", profile.Name);
        }

        private static void TestProfile(ColorProfile profile, string name)
        {
            Assert.IsNotNull(profile);
            Assert.AreEqual(name, profile.Name);
            Assert.AreEqual(3144, profile.ToByteArray().Length);
        }
    }
}
