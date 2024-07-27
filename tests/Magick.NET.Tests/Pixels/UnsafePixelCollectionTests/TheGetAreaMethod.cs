// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class UnsafePixelCollectionTests
{
    public class TheGetAreaMethod
    {
        [Fact]
        public void ShouldTNothrowExceptionWhenXTooLow()
            => ThrowsNoException(-1, 0, 1, 1);

        [Fact]
        public void ShouldNotThrowExceptionWhenXTooHigh()
            => ThrowsNoException(6, 0, 1, 1);

        [Fact]
        public void ShouldNotThrowExceptionWhenYTooLow()
            => ThrowsNoException(0, -1, 1, 1);

        [Fact]
        public void ShouldNotThrowExceptionWhenYTooHigh()
            => ThrowsNoException(0, 11, 1, 1);

        [Fact]
        public void ShouldThrowExceptionWhenWidthZero()
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickCacheErrorException>(() => pixels.GetArea(0, 0, 0, 1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightZero()
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickCacheErrorException>(() => pixels.GetArea(0, 0, 1, 0));
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenWidthAndOffsetTooHigh()
            => ThrowsNoException(4, 0, 2, 1);

        [Fact]
        public void ShouldNotThrowExceptionWhenHeightAndOffsetTooHigh()
            => ThrowsNoException(0, 9, 1, 2);

        [Fact]
        public void ShouldReturnAreaWhenAreaIsValid()
        {
            using var image = new MagickImage(Files.CirclePNG);
            using var pixels = image.GetPixelsUnsafe();
            var area = pixels.GetArea(28, 28, 2, 3);
            var length = 2 * 3 * 4; // width * height * channelCount
            var color = new MagickColor(area[0], area[1], area[2], area[3]);

            Assert.Equal(length, area.Length);
            ColorAssert.Equal(new MagickColor("#ffffff9f"), color);
        }

        [Fact]
        public void ShouldReturnNullWhenGeometryIsNull()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var pixels = image.GetPixelsUnsafe();
            var area = pixels.GetArea(null);

            Assert.Null(area);
        }

        [Fact]
        public void ShouldReturnAreaWhenGeometryIsValid()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var pixels = image.GetPixelsUnsafe();
            var area = pixels.GetArea(new MagickGeometry(0, 0, 6, 5));
            var length = 6 * 5 * 4; // width * height * channelCount
            var color = new MagickColor(area[0], area[1], area[2], area[3]);

            Assert.Equal(length, area.Length);
            ColorAssert.Equal(MagickColors.Red, color);
        }

        private static void ThrowsNoException(int x, int y, uint width, uint height)
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            using var pixels = image.GetPixelsUnsafe();
            pixels.GetArea(x, y, width, height);
        }
    }
}
