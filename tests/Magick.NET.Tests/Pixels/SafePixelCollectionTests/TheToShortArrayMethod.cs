// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheToShortArrayMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenXTooLow()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>("x", () => pixels.ToShortArray(-1, 0, 1, 1, "RGB"));
        }

        [Fact]
        public void ShouldReturnPixelsWhenAreaIsCorrect()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToShortArray(60, 60, 63, 58, "RGBA");
            var length = 63 * 58 * 4;

            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnPixelsWhenAreaIsCorrectAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToShortArray(60, 60, 63, 58, PixelMapping.RGBA);
            var length = 63 * 58 * 4;

            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("geometry", () => pixels.ToShortArray(null, "RGB"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNullAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("geometry", () => pixels.ToShortArray(null, PixelMapping.RGB));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("mapping", () => pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsEmpty()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentException>("mapping", () => pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), string.Empty));
        }

        [Fact]
        public void ShouldReturnArrayWhenGeometryIsCorrect()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToShortArray(new MagickGeometry(10, 10, 113, 108), "RG");
            var length = 113 * 108 * 2;

            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnArrayWhenGeometryIsCorrectAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToShortArray(new MagickGeometry(10, 10, 113, 108), PixelMapping.RGB);
            var length = 113 * 108 * 3;

            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldThrowExceptionWhenMappingIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("mapping", () => pixels.ToShortArray(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenMappingIsEmpty()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentException>("mapping", () => pixels.ToShortArray(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenMappingIsInvalid()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<MagickOptionErrorException>(() => pixels.ToShortArray("FOO"));
        }

        [Fact]
        public void ShouldReturnArrayWhenTwoChannelsAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToShortArray("RG");
            var length = (int)image.Width * image.Height * 2;

            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnArrayWhenTwoChannelsAreSuppliedAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToShortArray(PixelMapping.RGB);
            var length = (int)image.Width * image.Height * 3;

            Assert.Equal(length, values.Length);
        }
    }
}
