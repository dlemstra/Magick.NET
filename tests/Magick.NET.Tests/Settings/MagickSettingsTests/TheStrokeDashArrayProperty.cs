// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheStrokeDashArrayProperty
    {
        [Fact]
        public void ShouldDefaultToNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.StrokeDashArray);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.SkyBlue, 100, 60);
            image.Settings.StrokeColor = MagickColors.Moccasin;
            image.Settings.StrokeDashArray = new double[] { 5.0, 8.0, 10.0 };
            image.Settings.StrokeDashOffset = 1;
            image.Draw(new DrawablePath(new PathMoveToAbs(10, 20), new PathLineToAbs(90, 20)));

            ColorAssert.Equal(MagickColors.Moccasin, image, 13, 20);
            ColorAssert.Equal(MagickColors.Moccasin, image, 37, 20);
            ColorAssert.Equal(MagickColors.Moccasin, image, 60, 20);
            ColorAssert.Equal(MagickColors.Moccasin, image, 84, 20);
        }
    }
}
