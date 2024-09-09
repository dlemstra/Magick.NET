// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheColorThresholdMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenStartColorIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("startColor", () => image.ColorThreshold(null!, new MagickColor()));
        }

        [Fact]
        public void ShouldThrowExceptionWhenStopColorIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("stopColor", () => image.ColorThreshold(new MagickColor(), null!));
        }

        [Fact]
        public void ShouldChangeTheImageToBlackAndWhite()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
            var startColor = MagickColor.FromRgb(60, 110, 150);
            var stopColor = MagickColor.FromRgb(70, 120, 170);

            image.ColorThreshold(startColor, stopColor);

            ColorAssert.Equal(MagickColors.White, image, 300, 160);
            ColorAssert.Equal(MagickColors.Black, image, 300, 260);
        }
    }
}
