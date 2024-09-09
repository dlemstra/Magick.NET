// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheIndexer
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthOutOfRange()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>("x", () => pixels[(int)image.Width + 1, 0]);
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightOutOfRange()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>("y", () => pixels[0, (int)image.Height + 1]);
        }

        [Fact]
        public void ShouldReturnPixelWhenIndexIsCorrect()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var pixels = image.GetPixels();
            var pixel = pixels[300, 100];

            ColorAssert.Equal(MagickColors.Red, pixel?.ToColor());
        }
    }
}
