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
        [Fact]
        public void ShouldThrowExceptionWhenSettingsIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("settings", () => image.Deskew(null));
        }

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
        public void ShouldUseAutoCrop()
        {
            var settings = new DeskewSettings
            {
                AutoCrop = true,
                Threshold = new Percentage(10),
            };
            using var image = new MagickImage(Files.LetterJPG);
            var angle = image.Deskew(settings);

            Assert.InRange(angle, 7.01, 7.02);
            Assert.Equal(480, image.Width);
            Assert.Equal(577, image.Height);
        }

        [Fact]
        public void ShouldReturnTheAngle()
        {
            using var image = new MagickImage(Files.LetterJPG);
            var angle = image.Deskew(new Percentage(10));

            Assert.InRange(angle, 7.01, 7.02);
            Assert.Equal(546, image.Width);
            Assert.Equal(579, image.Height);
        }
    }
}
