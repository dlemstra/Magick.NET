// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheToByteArrayMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenXTooLow()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>("x", () => pixels.ToByteArray(-1, 0, 1, 1, "RGB"));
        }

        [Fact]
        public void ShouldReturnPixelsWhenAreaIsCorrect()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToByteArray(60, 60, 63, 58, "RGBA");
            var length = 63 * 58 * 4;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnPixelsWhenAreaIsCorrectAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToByteArray(60, 60, 63, 58, PixelMapping.RGBA);
            var length = 63 * 58 * 4;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("geometry", () => pixels.ToByteArray(null!, "RGB"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNullAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("geometry", () => pixels.ToByteArray(null!, PixelMapping.RGB));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("mapping", () => pixels.ToByteArray(new MagickGeometry(1, 2, 3, 4), null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsEmpty()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentException>("mapping", () => pixels.ToByteArray(new MagickGeometry(1, 2, 3, 4), string.Empty));
        }

        [Fact]
        public void ShouldReturnArrayWhenGeometryIsCorrect()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToByteArray(new MagickGeometry(10, 10, 113, 108), "RG");
            var length = 113 * 108 * 2;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnArrayWhenGeometryIsCorrectAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToByteArray(new MagickGeometry(10, 10, 113, 108), PixelMapping.RGB);
            var length = 113 * 108 * 3;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldThrowExceptionWhenMappingIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("mapping", () => pixels.ToByteArray(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenMappingIsEmpty()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentException>("mapping", () => pixels.ToByteArray(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenMappingIsInvalid()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<MagickOptionErrorException>(() => pixels.ToByteArray("FOO"));
        }

        [Fact]
        public void ShouldReturnArrayWhenTwoChannelsAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToByteArray("RG");
            var length = (int)image.Width * image.Height * 2;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnArrayWhenTwoChannelsAreSuppliedAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = pixels.ToByteArray(PixelMapping.RGB);
            var length = (int)image.Width * image.Height * 3;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }
    }
}
