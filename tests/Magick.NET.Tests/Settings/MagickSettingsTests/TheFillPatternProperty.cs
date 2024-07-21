// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheFillPatternProperty
    {
        [Fact]
        public void ShouldDefaultToNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.FillPattern);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.Transparent, 500, 500);
            image.Settings.FillPattern = new MagickImage(Files.SnakewarePNG);

            var coordinates = new PointD[4];
            coordinates[0] = new PointD(50, 50);
            coordinates[1] = new PointD(150, 50);
            coordinates[2] = new PointD(150, 150);
            coordinates[3] = new PointD(50, 150);
            image.Draw(new DrawablePolygon(coordinates));

            ColorAssert.Equal(new MagickColor("#02"), image, 84, 80);
        }
    }
}
