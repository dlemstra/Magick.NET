// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheFontFamilyProperty
    {
        [Fact]
        public void ShouldDefaultToNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.FontFamily);
        }

        [Fact]
        public void ShouldChangeTheFont()
        {
            using var image = new MagickImage();

            image.Settings.FontFamily = "Courier New";
            image.Settings.FontPointsize = 40;
            image.Settings.FontStyle = FontStyleType.Oblique;
            image.Settings.FontWeight = FontWeight.ExtraBold;
            image.Read("label:Test");

            Assert.Contains(image.Width, new[] { 97U, 98U });
            Assert.Equal(48U, image.Height);

            // Different result on MacOS
            if (image.Width != 97)
                ColorAssert.Equal(MagickColors.Black, image, 13, 13);
        }
    }
}
