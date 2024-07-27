// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheDeskewMethod
    {
#if !Q16HDRI

        [Fact]
        public void ShouldThrowExceptionWhenThresholdIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("threshold", () => image.Deskew(new Percentage(-1)));
        }
#endif

        [Fact]
        public void ShouldDeskewTheImage()
        {
            using var image = new MagickImage(Files.LetterJPG);
            image.ColorType = ColorType.Bilevel;

            ColorAssert.Equal(MagickColors.White, image, 471, 92);

            image.Deskew(new Percentage(10));

            ColorAssert.Equal(new MagickColor("#007400740074ffff"), image, 471, 92);
        }

        [Fact]
        public void ShouldReturnTheAngle()
        {
            using var image = new MagickImage(Files.LetterJPG);
            var angle = image.Deskew(new Percentage(10));

            Assert.InRange(angle, 7.01, 7.02);
            Assert.Equal(546U, image.Width);
            Assert.Equal(579U, image.Height);
        }
    }
}
