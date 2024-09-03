// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests;

public partial class IMagickImageExtensionsTests
{
    public partial class TheReadMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenBitmapIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("bitmap", () => image.Read((Bitmap)null!));
        }

        [Fact]
        public void ShouldUsePngFormatWhenBitmapIsPng()
        {
            using var bitmap = new Bitmap(Files.SnakewarePNG);
            using var image = new MagickImage();
            image.Read(bitmap);

            Assert.Equal(286U, image.Width);
            Assert.Equal(67U, image.Height);
            Assert.Equal(MagickFormat.Png, image.Format);
        }

        [Fact]
        public void ShouldUseBmpFormatWhenBitmapIsMemoryBmp()
        {
            using var bitmap = new Bitmap(100, 50, PixelFormat.Format24bppRgb);
            Assert.Equal(bitmap.RawFormat, ImageFormat.MemoryBmp);

            using var image = new MagickImage();
            image.Read(bitmap);

            Assert.Equal(100U, image.Width);
            Assert.Equal(50U, image.Height);
            Assert.Equal(MagickFormat.Bmp3, image.Format);
        }

        [Fact]
        public void ShouldCreateCorrectImageWithByteArrayFromSystemDrawing()
        {
            using var img = Image.FromFile(Files.Coders.PageTIF);
            using var memStream = new MemoryStream();
            img.Save(memStream, ImageFormat.Tiff);
            var bytes = memStream.GetBuffer();

            using var image = new MagickImage();
            image.Read(bytes);

            image.Settings.Compression = CompressionMethod.Group4;
            bytes = image.ToByteArray();

            using var before = new MagickImage(Files.Coders.PageTIF);
            using var after = new MagickImage(bytes);
            Assert.Equal(0.0, before.Compare(after, ErrorMetric.RootMeanSquared));
        }
    }
}
