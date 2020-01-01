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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheFloodFillMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenColorIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("color", () =>
                {
                    using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                    {
                        image.FloodFill((MagickColor)null, 0, 0);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPointDColorIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("color", () =>
                {
                    using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                    {
                        image.FloodFill((MagickColor)null, default);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTargetColorIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("target", () =>
                {
                    using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                    {
                        image.FloodFill(MagickColors.Purple, 0, 0, null);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPointDTargetColorIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("target", () =>
                {
                    using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                    {
                        image.FloodFill(MagickColors.Purple, default, null);
                    }
                });
            }

            [TestMethod]
            public void ShouldChangeTheColors()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    image.FloodFill(MagickColors.Red, 0, 0);

                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.Red, image, 1, 0);
                    ColorAssert.AreEqual(MagickColors.Red, image, 1, 1);
                }
            }

            [TestMethod]
            public void ShouldChangeThePointDColors()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    image.FloodFill(MagickColors.Red, new PointD(0, 0));

                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.Red, image, 1, 0);
                    ColorAssert.AreEqual(MagickColors.Red, image, 1, 1);
                }
            }

            [TestMethod]
            public void ShouldChangeTheTargetColors()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    using (IMagickImage green = new MagickImage(MagickColors.Green, 1, 1))
                    {
                        image.Composite(green, 0, 0, CompositeOperator.Over);
                    }

                    image.FloodFill(MagickColors.Red, 0, 0, MagickColors.Green);

                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                }
            }

            [TestMethod]
            public void ShouldChangeThePoinDTargetColors()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    using (IMagickImage green = new MagickImage(MagickColors.Green, 1, 1))
                    {
                        image.Composite(green, new PointD(0, 0), CompositeOperator.Over);
                    }

                    image.FloodFill(MagickColors.Red, new PointD(0, 0), MagickColors.Green);

                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                }
            }

            [TestMethod]
            public void ShouldChangeTheNeighboursWithTargetColor()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    using (IMagickImage green = new MagickImage(MagickColors.Green, 1, 1))
                    {
                        image.Composite(green, 0, 1, CompositeOperator.Over);
                    }

                    image.FloodFill(MagickColors.Red, 0, 0, MagickColors.Green);

                    ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                }
            }

            [TestMethod]
            public void ShouldChangeTheNeighboursWithPointDTargetColor()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    using (IMagickImage green = new MagickImage(MagickColors.Green, 1, 1))
                    {
                        image.Composite(green, new PointD(0, 1), CompositeOperator.Over);
                    }

                    image.FloodFill(MagickColors.Red, new PointD(0, 0), MagickColors.Green);

                    ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                }
            }

            [TestMethod]
            public void ShouldNotChangeTheTargetColors()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    using (IMagickImage green = new MagickImage(MagickColors.Green, 1, 1))
                    {
                        image.Composite(green, 1, 1, CompositeOperator.Over);
                    }

                    image.FloodFill(MagickColors.Red, 0, 0, MagickColors.Green);

                    ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 0);
                    ColorAssert.AreEqual(MagickColors.Green, image, 1, 1);
                }
            }

            [TestMethod]
            public void ShouldNotChangeThePoinDTargetColors()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    using (IMagickImage green = new MagickImage(MagickColors.Green, 1, 1))
                    {
                        image.Composite(green, new PointD(1, 1), CompositeOperator.Over);
                    }

                    image.FloodFill(MagickColors.Red, new PointD(0, 0), MagickColors.Green);

                    ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 0);
                    ColorAssert.AreEqual(MagickColors.Green, image, 1, 1);
                }
            }

            [TestMethod]
            public void ShouldChangeTheColorsWithTheSameTransparency()
            {
                using (IMagickImage image = new MagickImage(MagickColors.White, 2, 2))
                {
                    image.HasAlpha = true;

                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        var pixel = pixels.GetPixel(1, 1);
                        pixel.SetChannel(3, 0);

                        pixels.SetPixel(pixel);
                    }

                    image.Settings.FillColor = MagickColors.Purple;
                    image.FloodFill(0, 0, 0);

                    ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 0, 1);
                    ColorAssert.AreEqual(MagickColors.White, image, 1, 0);
                    ColorAssert.AreEqual(new MagickColor("#ffffff00"), image, 1, 1);
                }
            }
        }
    }
}
