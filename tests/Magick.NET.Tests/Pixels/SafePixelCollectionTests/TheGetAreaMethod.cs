// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheGetAreaMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenXTooLow()
            => ThrowsArgumentException("x", -1, 0, 1, 1);

        [Fact]
        public void ShouldThrowExceptionWhenXTooHigh()
            => ThrowsArgumentException("x", 6, 0, 1, 1);

        [Fact]
        public void ShouldThrowExceptionWhenYTooLow()
            => ThrowsArgumentException("y", 0, -1, 1, 1);

        [Fact]
        public void ShouldThrowExceptionWhenYTooHigh()
            => ThrowsArgumentException("y", 0, 11, 1, 1);

        [Fact]
        public void ShouldThrowExceptionWhenWidthZero()
            => ThrowsArgumentException("width", 0, 0, 0, 1);

        [Fact]
        public void ShouldThrowExceptionWhenHeightZero()
            => ThrowsArgumentException("height", 0, 0, 1, 0);

        [Fact]
        public void ShouldThrowExceptionWhenWidthAndOffsetTooHigh()
            => ThrowsArgumentException("width", 4, 0, 2, 1);

        [Fact]
        public void ShouldThrowExceptionWhenHeightAndOffsetTooHigh()
            => ThrowsArgumentException("height", 0, 9, 1, 2);

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNull()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("geometry", () => pixels.GetArea(null));
        }

        [Fact]
        public void ShouldReturnAreaWhenAreaIsValid()
        {
            using var image = new MagickImage(Files.CirclePNG);
            using var pixels = image.GetPixels();
            var area = pixels.GetArea(28, 28, 2, 3);
            var length = 2 * 3 * 4; // width * height * channelCount
            var color = new MagickColor(area[0], area[1], area[2], area[3]);

            Assert.Equal(length, area.Length);
            ColorAssert.Equal(new MagickColor("#ffffff9f"), color);
        }

        [Fact]
        public void ShouldReturnAreaWhenGeometryIsValid()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var pixels = image.GetPixels();
            var area = pixels.GetArea(new MagickGeometry(0, 0, 6, 5));
            var length = 6 * 5 * 4; // width * height * channelCount
            var color = new MagickColor(area[0], area[1], area[2], area[3]);

            Assert.Equal(length, area.Length);
            ColorAssert.Equal(MagickColors.Red, color);
        }

        private static void ThrowsArgumentException(string paramName, int x, int y, uint width, uint height)
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>(paramName, () => pixels.GetArea(x, y, width, height));
        }
    }
}
