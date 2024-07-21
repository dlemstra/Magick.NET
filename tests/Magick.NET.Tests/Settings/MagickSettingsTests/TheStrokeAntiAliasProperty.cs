// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheStrokeAntiAliasProperty
    {
        [Fact]
        public void ShouldDefaultToTrue()
        {
            using var image = new MagickImage();

            Assert.True(image.Settings.StrokeAntiAlias);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.Purple, 300, 300);

            image.Settings.StrokeWidth = 20;
            image.Settings.StrokeAntiAlias = false;
            image.Settings.StrokeColor = MagickColors.Orange;
            image.Draw(new DrawableCircle(150, 150, 100, 100));

            ColorAssert.Equal(MagickColors.Purple, image, 69, 145);
            ColorAssert.Equal(MagickColors.Orange, image, 69, 146);
            ColorAssert.Equal(MagickColors.Orange, image, 69, 154);
            ColorAssert.Equal(MagickColors.Purple, image, 69, 155);
        }
    }
}
