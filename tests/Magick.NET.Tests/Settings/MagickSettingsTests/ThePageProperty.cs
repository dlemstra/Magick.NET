// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class ThePageProperty
    {
        [Fact]
        public void ShouldDefaultToNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.Page);
        }

        [Fact]
        public void ShouldSetTheCorrectDimensionsWhenReadingImage()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.Page);

            image.Settings.Font = "Courier New";
            image.Settings.Page = new MagickGeometry(50, 50, 100, 100);
            image.Read("pango:Test");

            Assert.Equal(140U, image.Width);
            Assert.Contains(image.Height, new[] { 114U, 118U, 119U });
        }
    }
}
