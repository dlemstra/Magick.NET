// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing.Imaging;
using ImageMagick;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheToBitmapMethod
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
            public void ShouldReturnBitmap()
            {
                using var images = new MagickImageCollection(Files.RoseSparkleGIF);
                Assert.Equal(3, images.Count);

                using var bitmap = images.ToBitmap();
                Assert.NotNull(bitmap);
                Assert.Equal(3, bitmap.GetFrameCount(FrameDimension.Page));
            }

            [Fact]
            public void ShouldUseOptimizationForSingleImage()
            {
                using var images = new MagickImageCollection(Files.RoseSparkleGIF);
                images.RemoveAt(0);
                images.RemoveAt(0);

                Assert.Single(images);

                using var bitmap = images.ToBitmap();
                Assert.NotNull(bitmap);
            }

            private void AssertUnsupportedImageFormat(ImageFormat imageFormat)
            {
                using var images = new MagickImageCollection();

                Assert.Throws<NotSupportedException>(() => images.ToBitmap(imageFormat));
            }

            private void AssertSupportedImageFormat(ImageFormat imageFormat)
            {
                using var images = new MagickImageCollection(Files.RoseSparkleGIF);
                using var bitmap = images.ToBitmap(imageFormat);

                Assert.NotNull(bitmap);
                Assert.Equal(imageFormat, bitmap.RawFormat);
            }
        }
    }
}
