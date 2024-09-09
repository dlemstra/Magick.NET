// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheGetValueMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenXTooLow()
            => ThrowsArgumentOutOfRangeException("x", -1, 0);

        [Fact]
        public void ShouldThrowExceptionWhenXTooHigh()
            => ThrowsArgumentOutOfRangeException("x", 6, 0);

        [Fact]
        public void ShouldThrowExceptionWhenYTooLow()
            => ThrowsArgumentOutOfRangeException("y", 0, -1);

        [Fact]
        public void ShouldThrowExceptionWhenYTooHigh()
            => ThrowsArgumentOutOfRangeException("y", 0, 11);

        [Fact]
        public void ShouldReturnCorrectValue()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            using var pixels = image.GetPixels();
            var pixel = pixels.GetValue(0, 0);

            Assert.NotNull(pixel);
            Assert.Equal(3, pixel.Length);
            Assert.Equal(Quantum.Max, pixel[0]);
            Assert.Equal(0, pixel[1]);
            Assert.Equal(0, pixel[2]);
        }

        private static void ThrowsArgumentOutOfRangeException(string paramName, int x, int y)
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>(paramName, () => pixels.GetValue(x, y));
        }
    }
}
