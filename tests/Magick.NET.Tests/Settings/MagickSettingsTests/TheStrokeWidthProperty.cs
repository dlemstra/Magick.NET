// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheStrokeWidthProperty
    {
        [Fact]
        public void ShouldDefaultToOne()
        {
            using var image = new MagickImage();

            Assert.Equal(1, image.Settings.StrokeWidth);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.Purple, 300, 300);
            image.Settings.StrokeWidth = 40;
            image.Settings.StrokeColor = MagickColors.Orange;
            image.Draw(new DrawableCircle(150, 150, 100, 100));

            ColorAssert.Equal(MagickColors.Black, image, 150, 150);
            ColorAssert.Equal(MagickColors.Orange, image, 201, 150);
            ColorAssert.Equal(MagickColors.Purple, image, 244, 150);
        }
    }
}
