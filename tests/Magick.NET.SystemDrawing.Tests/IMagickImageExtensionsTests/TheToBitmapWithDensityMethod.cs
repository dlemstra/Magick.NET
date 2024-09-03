// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests;

public partial class IMagickImageExtensionsTests
{
    public class TheToBitmapWithDensityMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenImageFormatIsExif()
            => AssertUnsupportedImageFormat(ImageFormat.Exif);

        [Fact]
        public void ShouldThrowExceptionWhenImageFormatIsEmf()
            => AssertUnsupportedImageFormat(ImageFormat.Emf);

        [Fact]
        public void ShouldThrowExceptionWhenImageFormatIsWmf()
            => AssertUnsupportedImageFormat(ImageFormat.Wmf);

        [Fact]
        public void ShouldReturnBitmapWhenFormatIsBmp()
            => AssertSupportedImageFormat(ImageFormat.Bmp);

        [Fact]
        public void ShouldReturnBitmapWhenFormatIsGif()
            => AssertSupportedImageFormat(ImageFormat.Gif);

        [Fact]
        public void ShouldReturnBitmapWhenFormatIsIcon()
            => AssertSupportedImageFormat(ImageFormat.Icon);

        [Fact]
        public void ShouldReturnBitmapWhenFormatIsJpeg()
            => AssertSupportedImageFormat(ImageFormat.Jpeg);

        [Fact]
        public void ShouldReturnBitmapWhenFormatIsPng()
            => AssertSupportedImageFormat(ImageFormat.Png);

        [Fact]
        public void ShouldReturnBitmapWhenFormatIsTiff()
            => AssertSupportedImageFormat(ImageFormat.Tiff);

        [Fact]
        public void ShouldChangeTheColorSpaceToSrgb()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.ColorSpace = ColorSpace.YCbCr;

            using var bitmap = image.ToBitmapWithDensity();
            ColorAssert.Equal(MagickColors.Red, ToMagickColor(bitmap.GetPixel(0, 0)));

            Assert.Equal(ColorSpace.YCbCr, image.ColorSpace);
        }

        [Fact]
        public void ShouldBeAbleToConvertGrayImage()
        {
            using var image = new MagickImage(MagickColors.Magenta, 5, 1);
            image.Extent(5, 2, MagickColors.Honeydew);

            image.ColorType = ColorType.Bilevel;
            image.ClassType = ClassType.Direct;

            Assert.Equal(ColorSpace.Gray, image.ColorSpace);

            using var bitmap = image.ToBitmapWithDensity();
            for (var i = 0; i < image.Width; i++)
                ColorAssert.Equal(MagickColors.Black, ToMagickColor(bitmap.GetPixel(i, 0)));

            for (var i = 0; i < image.Width; i++)
                ColorAssert.Equal(MagickColors.White, ToMagickColor(bitmap.GetPixel(i, 1)));
        }

        [Fact]
        public void ShouldBeAbleToConvertRgbImage()
        {
            using var image = new MagickImage(MagickColors.Magenta, 5, 1);
            using var bitmap = image.ToBitmapWithDensity();
            for (var i = 0; i < image.Width; i++)
                ColorAssert.Equal(MagickColors.Magenta, ToMagickColor(bitmap.GetPixel(i, 0)));
        }

        [Fact]
        public void ShouldBeAbleToConvertRgbaImage()
        {
            using var image = new MagickImage(MagickColors.Magenta, 5, 1);
            image.Alpha(AlphaOption.On);

            var color = MagickColors.Magenta;
            color.A = Quantum.Max;

            using var bitmap = image.ToBitmapWithDensity();
            for (var i = 0; i < image.Width; i++)
                ColorAssert.Equal(color, ToMagickColor(bitmap.GetPixel(i, 0)));
        }

        [Fact]
        public void ShouldSetTheDensityOfTheBitmap()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Density = new Density(300, 200);

            using var bitmap = image.ToBitmapWithDensity();
            Assert.Equal(300, (int)bitmap.HorizontalResolution);
            Assert.Equal(200, (int)bitmap.VerticalResolution);
        }

        [Fact]
        public void ShouldThrowExceptionWhenImageFormatIsNull()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);

            Assert.Throws<ArgumentNullException>("imageFormat", () => image.ToBitmapWithDensity(null!));
        }

        [Fact]
        public void ShouldSetTheDensityOfTheBitmapWhenFormatIsUsed()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Density = new Density(300, 200);

            using var bitmap = image.ToBitmapWithDensity(ImageFormat.Jpeg);
            Assert.Equal(300, (int)bitmap.HorizontalResolution);
            Assert.Equal(200, (int)bitmap.VerticalResolution);
        }

        [Fact]
        public void ShouldUseTheDensityWhenUnitsAreUndefined()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Density = new Density(1, 1, DensityUnit.Undefined);

            using var bitmap = image.ToBitmapWithDensity(ImageFormat.Jpeg);
            Assert.Equal(1, bitmap.HorizontalResolution);
            Assert.Equal(1, bitmap.VerticalResolution);
        }

        [Fact]
        public void ShouldUseTheDefaultDensityWhenXIsZero()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Density = new Density(0, 1, DensityUnit.PixelsPerCentimeter);

            using var bitmap = image.ToBitmapWithDensity(ImageFormat.Jpeg);
            Assert.Equal(96, bitmap.HorizontalResolution);
            Assert.Equal(96, bitmap.VerticalResolution);
        }

        [Fact]
        public void ShouldUseTheDefaultDensityWhenYIsZero()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Density = new Density(1, 0, DensityUnit.PixelsPerCentimeter);

            using var bitmap = image.ToBitmapWithDensity(ImageFormat.Jpeg);
            Assert.Equal(96, bitmap.HorizontalResolution);
            Assert.Equal(96, bitmap.VerticalResolution);
        }

        private static void AssertUnsupportedImageFormat(ImageFormat imageFormat)
        {
            using var image = new MagickImage(MagickColors.Red, 10, 10);

            Assert.Throws<NotSupportedException>(() => image.ToBitmapWithDensity(imageFormat));
        }

        private static void AssertSupportedImageFormat(ImageFormat imageFormat)
        {
            using var image = new MagickImage(MagickColors.Red, 10, 10);
            using var bitmap = image.ToBitmapWithDensity(imageFormat);
            Assert.Equal(imageFormat, bitmap.RawFormat);

            // Cannot test JPEG due to rounding issues.
            if (imageFormat == ImageFormat.Jpeg)
                return;

            ColorAssert.Equal(MagickColors.Red, ToMagickColor(bitmap.GetPixel(0, 0)));
            ColorAssert.Equal(MagickColors.Red, ToMagickColor(bitmap.GetPixel(5, 5)));
            ColorAssert.Equal(MagickColors.Red, ToMagickColor(bitmap.GetPixel(9, 9)));
        }

        private static MagickColor ToMagickColor(Color color)
        {
            var result = new MagickColor();
            result.SetFromColor(color);
            return result;
        }
    }
}
