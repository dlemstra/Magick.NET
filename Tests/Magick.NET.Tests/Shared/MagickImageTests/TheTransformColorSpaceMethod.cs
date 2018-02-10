﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheTransformColorSpaceMethod
        {
            [TestMethod]
            public void ShouldReturnFalseWhenTheImageHasNoProfile()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    var result = image.TransformColorSpace(ColorProfile.AdobeRGB1998);

                    Assert.IsFalse(result);
                }
            }

            [TestMethod]
            public void ShouldReturnTrueWhenTheImageHasProfile()
            {
                using (IMagickImage image = new MagickImage(Files.PictureJPG))
                {
                    var result = image.TransformColorSpace(ColorProfile.SRGB);

                    Assert.IsTrue(result);
                }
            }

            [TestMethod]
            public void ShouldReturnFalseWhenSourceProfileColorSpaceIsIncorrect()
            {
                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    var result = image.TransformColorSpace(ColorProfile.USWebCoatedSWOP, ColorProfile.AdobeRGB1998);

                    Assert.IsFalse(result);
                }
            }

            [TestMethod]
            public void ShouldReturnTrueWhenSourceProfileColorSpaceIsCorrect()
            {
                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    var result = image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.AdobeRGB1998);

                    Assert.IsTrue(result);
                }
            }

            [TestMethod]
            public void ShouldReturnTrueWhenSourceProfileColorSpaceIsCorrectAndTheImageHasNoProfile()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    var result = image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.AdobeRGB1998);

                    Assert.IsTrue(result);
                }
            }

            [TestMethod]
            public void ShouldChangeTheColorSpace()
            {
                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    Assert.AreEqual(ColorSpace.sRGB, image.ColorSpace);

                    image.TransformColorSpace(ColorProfile.USWebCoatedSWOP, ColorProfile.USWebCoatedSWOP);
                    Assert.AreEqual(ColorSpace.sRGB, image.ColorSpace);

                    image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.USWebCoatedSWOP);
                    Assert.AreEqual(ColorSpace.CMYK, image.ColorSpace);
                }
            }

            [TestMethod]
            public void ShouldClampPixels()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 1, 1))
                {
                    image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.AdobeRGB1998);
#if Q8
                    ColorAssert.AreEqual(new MagickColor("#ffffff"), image, 1, 1);
#elif Q16
                    ColorAssert.AreEqual(new MagickColor("#fffffffbffffffff"), image, 1, 1);
#elif Q16HDRI
                    ColorAssert.AreEqual(new MagickColor(65538f, 65531f, 65542f, 65535f), image, 1, 1);
#else
#error Not implemented!
#endif
                }
            }
        }
    }
}
