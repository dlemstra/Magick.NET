// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheFontProperty
    {
        [Fact]
        public void ShouldDefaultToNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.Font);
        }

        [Fact]
        public void ShouldSetTheFontWhenReadingImage()
        {
            if (TestRuntime.HasFlakyMacOSResult)
                return;

            using var image = new MagickImage();

            Assert.Null(image.Settings.Font);

            image.Settings.Font = "Courier New";
            image.Settings.FontPointsize = 40;
            image.Read("pango:Test");

            Assert.Equal(128, image.Width);
            Assert.Contains(image.Height, new[] { 58, 62 });
            ColorAssert.Equal(MagickColors.Black, image, 26, 22);
        }
    }
}
