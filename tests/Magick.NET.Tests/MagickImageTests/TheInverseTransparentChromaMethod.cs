// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheInverseTransparentChromaMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorLowIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("colorLow", () => image.InverseTransparentChroma(null!, MagickColors.Red));
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorHighIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("colorHigh", () => image.InverseTransparentChroma(MagickColors.Red, null!));
        }

        [Fact]
        public void ShouldChangePixelsOutsideRangeOfLowAndHighColor()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.InverseTransparentChroma(MagickColors.Black, MagickColors.WhiteSmoke);

            ColorAssert.Equal(new MagickColor("#396239623962ffff"), image, 50, 50);
            ColorAssert.Equal(new MagickColor("#000f"), image, 32, 80);
            ColorAssert.Equal(new MagickColor("#f6def6def6de0000"), image, 132, 42);
            ColorAssert.Equal(new MagickColor("#000080800000ffff"), image, 74, 79);
        }
    }
}
