// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTransparentChromaMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorLowIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("colorLow", () => image.TransparentChroma(null!, MagickColors.Red));
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorHighIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("colorHigh", () => image.TransparentChroma(MagickColors.Red, null!));
        }

        [Fact]
        public void ShouldChangePixelsWithinRangeOfLowAndHighColor()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.TransparentChroma(MagickColors.Black, MagickColors.WhiteSmoke);

            ColorAssert.Equal(new MagickColor("#3962396239620000"), image, 50, 50);
            ColorAssert.Equal(new MagickColor("#0000"), image, 32, 80);
            ColorAssert.Equal(new MagickColor("#f6def6def6deffff"), image, 132, 42);
            ColorAssert.Equal(new MagickColor("#0000808000000000"), image, 74, 79);
        }
    }
}
