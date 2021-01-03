// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Tests
{
    public partial class UnsafePixelCollectionTests
    {
        public class TheGetValueMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenXTooLow()
            {
                ThrowsOverflowException(-1, 0);
            }

            [Fact]
            public void ShouldThrowExceptionWhenXTooHigh()
            {
                ThrowsNoException(6, 0);
            }

            [Fact]
            public void ShouldThrowExceptionWhenYTooLow()
            {
                ThrowsOverflowException(0, -1);
            }

            [Fact]
            public void ShouldThrowExceptionWhenYTooHigh()
            {
                ThrowsNoException(0, 11);
            }

            [Fact]
            public void ShouldReturnCorrectValue()
            {
                using (var image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        var pixel = pixels.GetValue(0, 0);

                        Assert.Equal(3, pixel.Length);
                        Assert.Equal(Quantum.Max, pixel[0]);
                        Assert.Equal(0, pixel[1]);
                        Assert.Equal(0, pixel[2]);
                    }
                }
            }

            private static void ThrowsOverflowException(int x, int y)
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        if (OperatingSystem.Is64Bit)
                        {
                            pixels.GetValue(x, y);
                        }
                        else
                        {
                            Assert.Throws<OverflowException>(() =>
                            {
                                pixels.GetValue(x, y);
                            });
                        }
                    }
                }
            }

            private static void ThrowsNoException(int x, int y)
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        pixels.GetValue(x, y);
                    }
                }
            }
        }
    }
}
