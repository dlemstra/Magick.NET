// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheBorderColorProperty
    {
        [Fact]
        public void ShouldDefaultToGrey()
        {
            using var image = new MagickImage();

            ColorAssert.Equal(new MagickColor("#df"), image.Settings.BorderColor);
        }

        [Fact]
        public void ShouldBeUsedWhenExtendingTheImage()
        {
            using var image = new MagickImage(MagickColors.MediumTurquoise, 10, 10);

            image.Settings.FillColor = MagickColors.Beige;
            image.Settings.BorderColor = MagickColors.MediumTurquoise;
            image.Extent(20, 20, Gravity.Center, MagickColors.Aqua);
            image.Draw(new DrawableColor(0, 0, PaintMethod.FillToBorder));

            ColorAssert.Equal(MagickColors.Beige, image, 0, 0);
            ColorAssert.Equal(MagickColors.MediumTurquoise, image, 10, 10);
        }
    }
}
