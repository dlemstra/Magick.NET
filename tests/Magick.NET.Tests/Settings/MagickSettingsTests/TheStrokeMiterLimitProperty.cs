// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheStrokeMiterLimitProperty
    {
        [Fact]
        public void ShouldDefaultToTen()
        {
            using var image = new MagickImage();

            Assert.Equal(10U, image.Settings.StrokeMiterLimit);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.SkyBlue, 100, 60);
            image.Settings.StrokeWidth = 5;
            image.Settings.StrokeColor = MagickColors.MediumSpringGreen;
            image.Settings.StrokeMiterLimit = 6;
            image.Draw(new DrawablePath(new PathMoveToAbs(65, 70), new PathLineToAbs(80, 20), new PathLineToAbs(95, 70)));

            ColorAssert.Equal(MagickColors.SkyBlue, image, 80, 18);
        }
    }
}
