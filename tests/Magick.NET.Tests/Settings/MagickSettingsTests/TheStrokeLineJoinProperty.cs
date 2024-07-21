// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheStrokeLineJoinProperty
    {
        [Fact]
        public void ShouldDefaultToMiter()
        {
            using var image = new MagickImage();

            Assert.Equal(LineJoin.Miter, image.Settings.StrokeLineJoin);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.SkyBlue, 100, 60);
            image.Settings.StrokeWidth = 5;
            image.Settings.StrokeColor = MagickColors.LemonChiffon;
            image.Settings.StrokeLineJoin = LineJoin.Round;
            image.Draw(new DrawablePath(new PathMoveToAbs(75, 70), new PathLineToAbs(90, 20), new PathLineToAbs(105, 70)));

            ColorAssert.Equal(MagickColors.SkyBlue, image, 90, 12);
        }
    }
}
