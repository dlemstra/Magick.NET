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
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class UnsafePixelCollectionTests
    {
        public class TheGetAreaPointerMethod
        {
            private static bool Is64Bit => IntPtr.Size == 8;

            [Fact]
            public void ShouldThrowExceptionWhen32BitAndXTooLow()
            {
                ThrowsOverflowExceptionWhen32Bit(-1, 0, 1, 1);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenXTooHigh()
            {
                ThrowsNoException(6, 0, 1, 1);
            }

            [Fact]
            public void ShouldThrowExceptionWhen32BitAndYTooLow()
            {
                ThrowsOverflowExceptionWhen32Bit(0, -1, 1, 1);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenYTooHigh()
            {
                ThrowsNoException(0, 11, 1, 1);
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthTooLow()
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        if (Is64Bit)
                        {
                            Assert.Throws<MagickCacheErrorException>(() =>
                            {
                                pixels.GetArea(0, 0, -1, 1);
                            });
                        }
                        else
                        {
                            Assert.Throws<OverflowException>(() =>
                            {
                                pixels.GetArea(0, 0, -1, 1);
                            });
                        }
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthZero()
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        Assert.Throws<MagickCacheErrorException>(() =>
                        {
                            pixels.GetAreaPointer(0, 0, 0, 1);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightTooLow()
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        if (Is64Bit)
                        {
                            Assert.Throws<MagickCacheErrorException>(() =>
                            {
                                pixels.GetAreaPointer(0, 0, 1, -1);
                            });
                        }
                        else
                        {
                            Assert.Throws<OverflowException>(() =>
                            {
                                pixels.GetAreaPointer(0, 0, 1, -1);
                            });
                        }
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightZero()
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        Assert.Throws<MagickCacheErrorException>(() =>
                        {
                            pixels.GetAreaPointer(0, 0, 1, 0);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenWidthAndOffsetTooHigh()
            {
                ThrowsNoException(4, 0, 2, 1);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenHeightAndOffsetTooHigh()
            {
                ThrowsNoException(0, 9, 1, 2);
            }

            [Fact]
            public void ShouldReturnAreaWhenAreaIsValid()
            {
                using (var image = new MagickImage(Files.CirclePNG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        var area = pixels.GetAreaPointer(28, 28, 2, 3);
                        unsafe
                        {
                            var channel = (QuantumType*)area;
                            var color = new MagickColor(*channel, *(channel + 1), *(channel + 2), *(channel + 3));

                            ColorAssert.Equal(new MagickColor("#ffffff9f"), color);
                        }
                    }
                }
            }

            [Fact]
            public void ShouldReturnIntPtrZeroWhenGeometryIsNull()
            {
                using (var image = new MagickImage(Files.RedPNG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        var area = pixels.GetAreaPointer(null);
                        Assert.Equal(area, IntPtr.Zero);
                    }
                }
            }

            [Fact]
            public void ShouldReturnAreaWhenGeometryIsValid()
            {
                using (var image = new MagickImage(Files.RedPNG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        var area = pixels.GetAreaPointer(new MagickGeometry(0, 0, 6, 5));
                        unsafe
                        {
                            var channel = (QuantumType*)area;
                            var color = new MagickColor(*channel, *(channel + 1), *(channel + 2), *(channel + 3));
                            ColorAssert.Equal(MagickColors.Red, color);
                        }
                    }
                }
            }

            private static void ThrowsOverflowExceptionWhen32Bit(int x, int y, int width, int height)
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        if (Is64Bit)
                        {
                            pixels.GetAreaPointer(x, y, width, height);
                        }
                        else
                        {
                            Assert.Throws<OverflowException>(() =>
                            {
                                pixels.GetAreaPointer(x, y, width, height);
                            });
                        }
                    }
                }
            }

            private static void ThrowsNoException(int x, int y, int width, int height)
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        pixels.GetAreaPointer(x, y, width, height);
                    }
                }
            }
        }
    }
}
