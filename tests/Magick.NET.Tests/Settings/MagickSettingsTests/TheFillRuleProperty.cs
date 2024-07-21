// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheFillRuleProperty
    {
        [Fact]
        public void ShouldDefaultToEvenOdd()
        {
            using var image = new MagickImage();

            Assert.Equal(FillRule.EvenOdd, image.Settings.FillRule);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.SkyBlue, 100, 60);

            image.Settings.FillRule = FillRule.Nonzero;
            image.Settings.FillColor = MagickColors.White;
            image.Settings.StrokeColor = MagickColors.Black;
            image.Draw(new DrawablePath(
              new PathMoveToAbs(40, 10),
              new PathLineToAbs(20, 20),
              new PathLineToAbs(70, 50),
              new PathClose(),
              new PathMoveToAbs(20, 40),
              new PathLineToAbs(70, 40),
              new PathLineToAbs(90, 10),
              new PathClose()));

            ColorAssert.Equal(MagickColors.White, image, 50, 30);
        }
    }
}
