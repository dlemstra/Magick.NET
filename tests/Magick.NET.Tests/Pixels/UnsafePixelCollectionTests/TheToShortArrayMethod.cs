// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class UnsafePixelCollectionTests
{
    public class TheToShortArrayMethod
    {
        [Fact]
        public void ShouldReturnPixelsWhenXTooLow()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();

            var result = pixels.ToShortArray(-1, 0, 1, 1, "RGB");
            Assert.Equal(new ushort[] { 65535, 65535, 65535 }, result);
        }

        [Fact]
        public void ShouldReturnPixelsWhenAreaIsCorrect()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray(60, 60, 63, 58, "RGBA");
            var length = 63 * 58 * 4;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnPixelsWhenAreaIsCorrectAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray(60, 60, 63, 58, PixelMapping.RGBA);
            var length = 63 * 58 * 4;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnNullWhenGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray(null!, "RGB");

            Assert.Null(values);
        }

        [Fact]
        public void ShouldReturnNullWhenGeometryIsSpecifiedAndMappingIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), null!);

            Assert.Null(values);
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsEmpty()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickResourceLimitErrorException>(() => pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), string.Empty));
        }

        [Fact]
        public void ShouldReturnArrayWhenGeometryIsCorrect()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray(new MagickGeometry(10, 10, 113, 108), "RG");
            var length = 113 * 108 * 2;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnArrayWhenGeometryIsCorrectAndMappingIsEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray(new MagickGeometry(10, 10, 113, 108), PixelMapping.RGB);
            var length = 113 * 108 * 3;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnNullWhenMappingIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray(null!);

            Assert.Null(values);
        }

        [Fact]
        public void ShouldThrowExceptionWhenMappingIsEmpty()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickResourceLimitErrorException>(() => pixels.ToShortArray(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenMappingIsInvalid()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickOptionErrorException>(() => pixels.ToShortArray("FOO"));
        }

        [Fact]
        public void ShouldReturnArrayWhenTwoChannelsAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray("RG");
            var length = (int)image.Width * image.Height * 2;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }

        [Fact]
        public void ShouldReturnArrayWhenTwoChannelsAreSuppliedAsPixelMappingEnum()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = pixels.ToShortArray(PixelMapping.RGB);
            var length = (int)image.Width * image.Height * 3;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }
    }
}
