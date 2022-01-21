// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;
using OperatingSystem = ImageMagick.OperatingSystem;

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
