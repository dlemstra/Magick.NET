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
            Assert.SkipWhen(TestRuntime.HasFlakyMacOSArm64Result, "Flaky result on MacOS arm64.");

            using var image = new MagickImage();

            Assert.Null(image.Settings.Font);

            image.Settings.Font = "Courier New";
            image.Settings.FontPointsize = 40;
            image.Read("pango:Test");

            Assert.Equal(128U, image.Width);
            Assert.Contains(image.Height, new[] { 58U, 62U });
            ColorAssert.Equal(MagickColors.Black, image, 27, 22);
        }
    }
}
