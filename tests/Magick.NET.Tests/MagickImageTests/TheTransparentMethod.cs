// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTransparentMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("color", () => image.Transparent(null));
        }

        [Fact]
        public void ShouldChangePixelsWithMatchingColorToTransparent()
        {
            var red = new MagickColor("red");
            var transparentRed = new MagickColor("red");
            transparentRed.A = 0;

            using var image = new MagickImage(Files.RedPNG);

            ColorAssert.Equal(red, image, 0, 0);

            image.Transparent(red);

            ColorAssert.Equal(transparentRed, image, 0, 0);
            ColorAssert.NotEqual(transparentRed, image, (int)image.Width - 1, 0);
        }
    }
}
