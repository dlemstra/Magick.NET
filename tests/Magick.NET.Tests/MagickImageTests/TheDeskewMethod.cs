// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheDeskewMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("settings", () => image.Deskew(null));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsThresholdIsNegative()
            {
                using (var image = new MagickImage())
                {
                    var settings = new DeskewSettings
                    {
                        Threshold = new Percentage(-1),
                    };

                    Assert.Throws<ArgumentException>("settings", () => image.Deskew(settings));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenThresholdIsNegative()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentException>("settings", () => image.Deskew(new Percentage(-1)));
                }
            }

            [Fact]
            public void ShouldDeskewTheImage()
            {
                using (var image = new MagickImage(Files.LetterJPG))
                {
                    image.ColorType = ColorType.Bilevel;

                    ColorAssert.Equal(MagickColors.White, image, 471, 92);

                    image.Deskew(new Percentage(10));

                    ColorAssert.Equal(new MagickColor("#007400740074"), image, 471, 92);
                }
            }

            [Fact]
            public void ShouldUseAutoCrop()
            {
                using (var image = new MagickImage(Files.LetterJPG))
                {
                    var settings = new DeskewSettings
                    {
                        AutoCrop = true,
                        Threshold = new Percentage(10),
                    };

                    image.Deskew(settings);

                    Assert.Equal(480, image.Width);
                    Assert.Equal(577, image.Height);
                }
            }

            [Fact]
            public void ShouldReturnTheAngle()
            {
                using (var image = new MagickImage(Files.LetterJPG))
                {
                    var angle = image.Deskew(new Percentage(10));

                    Assert.InRange(angle, 7.01, 7.02);
                }
            }
        }
    }
}
