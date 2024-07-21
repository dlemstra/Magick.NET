// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheStrokeLineCapProperty
    {
        [Fact]
        public void ShouldDefaultToButt()
        {
            using var image = new MagickImage();

            Assert.Equal(LineCap.Butt, image.Settings.StrokeLineCap);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.SkyBlue, 100, 60);
            image.Settings.StrokeWidth = 8;
            image.Settings.StrokeColor = MagickColors.Sienna;
            image.Settings.StrokeLineCap = LineCap.Round;
            image.Draw(new DrawablePath(new PathMoveToAbs(40, 20), new PathLineToAbs(40, 70)));

            ColorAssert.Equal(MagickColors.Sienna, image, 40, 17);
        }
    }
}
