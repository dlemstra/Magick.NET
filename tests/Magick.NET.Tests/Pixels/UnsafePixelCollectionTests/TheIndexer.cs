// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class UnsafePixelCollectionTests
{
    public class TheIndexer
    {
        [Fact]
        public void ShouldNotThrowExceptionWhenWidthOutOfRange()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels[(int)image.Width + 1, 0];
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenHeightOutOfRange()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels[0, (int)image.Height + 1];
        }

        [Fact]
        public void ShouldReturnPixelWhenIndexIsCorrect()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels[300, 100];

            ColorAssert.Equal(MagickColors.Red, pixel?.ToColor());
        }
    }
}
