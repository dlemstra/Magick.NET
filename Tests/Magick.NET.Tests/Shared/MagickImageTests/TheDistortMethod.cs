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
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheDistortMethod
        {
            [TestMethod]
            public void ShouldThrowAnExceptionWhenArgumentsIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("arguments", () =>
                    {
                        image.Distort(DistortMethod.Perspective, (double[])null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenArgumentsIsNullAndSettingsIsNot()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("arguments", () =>
                    {
                        image.Distort(DistortMethod.Perspective, new DistortSettings(), null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenArgumentsIsEmpty()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentException("arguments", () =>
                    {
                        image.Distort(DistortMethod.Perspective, new double[] { });
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenArgumentsIsEmptyAndSettingsIsNot()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentException("arguments", () =>
                    {
                        image.Distort(DistortMethod.Perspective, new DistortSettings(), new double[] { });
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenSettingsIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("settings", () =>
                    {
                        image.Distort(DistortMethod.Perspective, null, new double[] { 0 });
                    });
                }
            }

            [TestMethod]
            public void ShouldSetAnArtifactWhenTheScaleOfTheSettingsIsNotNull()
            {
                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    DistortSettings settings = new DistortSettings()
                    {
                        Scale = 5.2,
                    };

                    image.Distort(DistortMethod.Barrel, settings, new double[] { 0, 0, 0, 0, 0 });

                    Assert.AreEqual("5.2", image.GetArtifact("distort:scale"));
                }
            }

            [TestMethod]
            public void ShouldSetAnArtifactWhenTheViewportOfTheSettingsIsNotNull()
            {
                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    DistortSettings settings = new DistortSettings()
                    {
                        Viewport = new MagickGeometry(1, 2, 300, 400),
                    };

                    image.Distort(DistortMethod.Barrel, settings, new double[] { 0, 0, 0, 0, 0 });

                    Assert.AreEqual("300x400+1+2", image.GetArtifact("distort:viewport"));
                }
            }

            [TestMethod]
            public void ShouldBeAbleToPerformPerspectiveDistortion()
            {
                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    image.BackgroundColor = MagickColors.Cornsilk;
                    image.VirtualPixelMethod = VirtualPixelMethod.Background;
                    image.Distort(DistortMethod.Perspective, new double[] { 0, 0, 0, 0, 0, 90, 0, 90, 90, 0, 90, 25, 90, 90, 90, 65 });
                    image.Clamp();

                    ColorAssert.AreEqual(new MagickColor("#0000"), image, 1, 64);
                    ColorAssert.AreEqual(MagickColors.Cornsilk, image, 104, 50);
                    ColorAssert.AreEqual(new MagickColor("#aa4de148f9cb"), image, 66, 62);
                }
            }
        }
    }
}
