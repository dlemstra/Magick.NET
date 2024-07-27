// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheGetPixelMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthOutsideImage()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>("x", () => pixels.GetPixel((int)image.Width + 1, 0));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightOutsideImage()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>("y", () => pixels.GetPixel(0, (int)image.Height + 1));
        }

        [Fact]
        public void ShouldReturnPixelWhenIndexInsideImage()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            using var pixels = image.GetPixels();
            var pixel = pixels.GetPixel(55, 68);

            ColorAssert.Equal(new MagickColor("#a8dff8ff"), pixel.ToColor());
        }
    }
}
