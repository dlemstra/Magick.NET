// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests;

public partial class MagickImageFactoryTests
{
    public partial class TheCreateMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenBitmapIsNull()
        {
            var factory = new MagickImageFactory();
            Assert.Throws<ArgumentNullException>("bitmap", () => factory.Create((Bitmap)null));
        }

        [Fact]
        public void ShouldCreateImageFromBitmap()
        {
            var factory = new MagickImageFactory();
            using var bitmap = new Bitmap(Files.SnakewarePNG);
            using var image = factory.Create(bitmap);

            Assert.Equal(286, image.Width);
            Assert.Equal(67, image.Height);
            Assert.Equal(MagickFormat.Png, image.Format);
        }
    }
}
