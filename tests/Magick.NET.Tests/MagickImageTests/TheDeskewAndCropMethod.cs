// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheDeskewAndCropMethod
    {
#if !Q16HDRI

        [Fact]
        public void ShouldThrowExceptionWhenThresholdIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("threshold", () => image.DeskewAndCrop(new Percentage(-1)));
        }
#endif

        [Fact]
        public void ShouldUseAutoCrop()
        {
            using var image = new MagickImage(Files.LetterJPG);
            var angle = image.DeskewAndCrop(new Percentage(10));

            Assert.InRange(angle, 7.01, 7.02);
            Assert.Equal(480, image.Width);
            Assert.Equal(577, image.Height);
        }

        [Fact]
        public void ShouldReturnTheAngle()
        {
            using var image = new MagickImage(Files.LetterJPG);
            var angle = image.DeskewAndCrop(new Percentage(10));

            Assert.InRange(angle, 7.01, 7.02);
            Assert.Equal(480, image.Width);
            Assert.Equal(577, image.Height);
        }
    }
}
