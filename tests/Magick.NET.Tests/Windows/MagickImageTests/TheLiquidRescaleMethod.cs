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

#if WINDOWS_BUILD

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheLiquidRescaleMethod
        {
            [TestClass]
            public class WithWidthAndHeight
            {
                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.LiquidRescale(128, 64);
                        Assert.AreEqual(64, image.Width);
                        Assert.AreEqual(64, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithWidthAndHeightAndRigidity
            {
                [TestMethod]
                public void ShouldApplyTheRigidity()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.LiquidRescale(64, 64);

                        using (var other = new MagickImage(Files.MagickNETIconPNG))
                        {
                            other.LiquidRescale(64, 64, 5.0, 0.0);

                            Assert.AreEqual(0.5, image.Compare(other, ErrorMetric.RootMeanSquared), 0.1);
                        }

                        using (var other = new MagickImage(Files.MagickNETIconPNG))
                        {
                            other.LiquidRescale(64, 64, 5.0, 10.0);

                            Assert.AreEqual(0.3, image.Compare(other, ErrorMetric.RootMeanSquared), 0.1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithGeometry
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenGeometryIsNull()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("geometry", () =>
                        {
                            image.LiquidRescale(null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        var geometry = new MagickGeometry(128, 64)
                        {
                            IgnoreAspectRatio = true,
                        };

                        image.LiquidRescale(geometry);
                        Assert.AreEqual(128, image.Width);
                        Assert.AreEqual(64, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithPercentage
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenPercentageIsNegative()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        ExceptionAssert.Throws<ArgumentException>("percentage", () =>
                        {
                            image.LiquidRescale(new Percentage(-1));
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenPercentageWidthIsNegative()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        ExceptionAssert.Throws<ArgumentException>("percentageWidth", () =>
                        {
                            image.LiquidRescale(new Percentage(-1), new Percentage(1));
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenPercentageHeightIsNegative()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        ExceptionAssert.Throws<ArgumentException>("percentageHeight", () =>
                        {
                            image.LiquidRescale(new Percentage(1), new Percentage(-1));
                        });
                    }
                }

                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.LiquidRescale(new Percentage(25));
                        Assert.AreEqual(32, image.Width);
                        Assert.AreEqual(32, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldIgnoreTheAspectRatio()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.LiquidRescale(new Percentage(25), new Percentage(10));
                        Assert.AreEqual(32, image.Width);
                        Assert.AreEqual(13, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithPercentageAndRigidity
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenPercentageWidthIsNegative()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        ExceptionAssert.Throws<ArgumentException>("percentageWidth", () =>
                        {
                            image.LiquidRescale(new Percentage(-1), new Percentage(1), 0.0, 0.0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenPercentageHeightIsNegative()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        ExceptionAssert.Throws<ArgumentException>("percentageHeight", () =>
                        {
                            image.LiquidRescale(new Percentage(1), new Percentage(-1), 0.0, 0.0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldApplyTheRigidity()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.LiquidRescale(new Percentage(50), new Percentage(50));

                        using (var other = new MagickImage(Files.MagickNETIconPNG))
                        {
                            other.LiquidRescale(new Percentage(50), new Percentage(50), 5.0, 0.0);

                            Assert.AreEqual(0.5, image.Compare(other, ErrorMetric.RootMeanSquared), 0.1);
                        }

                        using (var other = new MagickImage(Files.MagickNETIconPNG))
                        {
                            other.LiquidRescale(new Percentage(50), new Percentage(50), 5.0, 10.0);

                            Assert.AreEqual(0.3, image.Compare(other, ErrorMetric.RootMeanSquared), 0.1);
                        }
                    }
                }
            }
        }
    }
}

#endif