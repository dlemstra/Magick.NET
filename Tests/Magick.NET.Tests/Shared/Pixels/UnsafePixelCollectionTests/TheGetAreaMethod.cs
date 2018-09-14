// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public partial class UnsafePixelCollectionTests
    {
        [TestClass]
        public class TheGetAreaMethod
        {
            private static bool Is64Bit => IntPtr.Size == 8;

            [TestMethod]
            public void ShouldThrowExceptionWhen32BitAndXTooLow()
            {
                ThrowsOverflowExceptionWhen32Bit(-1, 0, 1, 1);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenXTooHigh()
            {
                ThrowsNoException(6, 0, 1, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhen32BitAndYTooLow()
            {
                ThrowsOverflowExceptionWhen32Bit(0, -1, 1, 1);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenYTooHigh()
            {
                ThrowsNoException(0, 11, 1, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWidthTooLow()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        ExceptionAssert.Throws<OverflowException>(() =>
                        {
                            pixels.GetArea(0, 0, -1, 1);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWidthZero()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        ExceptionAssert.Throws<InvalidOperationException>(() =>
                        {
                            pixels.GetArea(0, 0, 0, 1);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightTooLow()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        if (Is64Bit)
                        {
                            ExceptionAssert.Throws<MagickResourceLimitErrorException>(() =>
                            {
                                pixels.GetArea(0, 0, 1, -1);
                            });
                        }
                        else
                        {
                            ExceptionAssert.Throws<OverflowException>(() =>
                            {
                                pixels.GetArea(0, 0, 1, -1);
                            });
                        }
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenHeightZero()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        ExceptionAssert.Throws<InvalidOperationException>(() =>
                        {
                            pixels.GetArea(0, 0, 1, 0);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenWidthAndOffsetTooHigh()
            {
                ThrowsNoException(4, 0, 2, 1);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenHeightAndOffsetTooHigh()
            {
                ThrowsNoException(0, 9, 1, 2);
            }

            [TestMethod]
            public void ShouldReturnAreaWhenAreaIsValid()
            {
                using (IMagickImage image = new MagickImage(Files.CirclePNG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var area = pixels.GetArea(28, 28, 2, 3);
                        int length = 2 * 3 * 4; // width * height * channelCount
                        MagickColor color = new MagickColor(area[0], area[1], area[2], area[3]);

                        Assert.AreEqual(length, area.Length);
                        ColorAssert.AreEqual(new MagickColor("#ffffff9f"), color);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnNullWhenGeometryIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.RedPNG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var area = pixels.GetArea(null);
                        Assert.IsNull(area);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnAreaWhenGeometryIsValid()
            {
                using (IMagickImage image = new MagickImage(Files.RedPNG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var area = pixels.GetArea(new MagickGeometry(0, 0, 6, 5));
                        int length = 6 * 5 * 4; // width * height * channelCount
                        MagickColor color = new MagickColor(area[0], area[1], area[2], area[3]);

                        Assert.AreEqual(length, area.Length);
                        ColorAssert.AreEqual(MagickColors.Red, color);
                    }
                }
            }

            private static void ThrowsOverflowExceptionWhen32Bit(int x, int y, int width, int height)
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        if (Is64Bit)
                        {
                            pixels.GetArea(x, y, width, height);
                        }
                        else
                        {
                            ExceptionAssert.Throws<OverflowException>(() =>
                            {
                                pixels.GetArea(x, y, width, height);
                            });
                        }
                    }
                }
            }

            private static void ThrowsNoException(int x, int y, int width, int height)
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.GetArea(x, y, width, height);
                    }
                }
            }
        }
    }
}
