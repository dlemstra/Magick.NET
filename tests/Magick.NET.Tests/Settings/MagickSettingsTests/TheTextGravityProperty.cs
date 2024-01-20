// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheTextGravityProperty
    {
        [Fact]
        public void ShouldDefaultToUndefined()
        {
            using var image = new MagickImage(MagickColors.Fuchsia, 100, 60);

            Assert.Equal(Gravity.Undefined, image.Settings.TextGravity);
        }

        [Fact]
        public void ShouldDetermineThePositionOfTheText()
        {
            using var image = new MagickImage("xc:red", 300, 300);

            Assert.Equal(Gravity.Undefined, image.Settings.TextGravity);

            image.Settings.BackgroundColor = MagickColors.Yellow;
            image.Settings.StrokeColor = MagickColors.Fuchsia;
            image.Settings.FillColor = MagickColors.Fuchsia;
            image.Settings.TextGravity = Gravity.Center;

            image.Read("label:Test");

            ColorAssert.Equal(MagickColors.Yellow, image, 50, 80);
            ColorAssert.Equal(MagickColors.Fuchsia, image, 50, 160);
        }
    }
}
