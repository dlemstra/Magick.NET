// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawablesTests
{
    public class TheStrokeDashArrayMethod
    {
        [Fact]
        public void ShouldRenderCorrectly()
        {
            using var image = new MagickImage(MagickColors.White, 210, 210);
            var drawables = new Drawables()
                .StrokeDashArray(20.0, 10.0)
                .StrokeLineCap(LineCap.Round)
                .EnableStrokeAntialias()
                .StrokeWidth(5.0)
                .FillColor(MagickColors.Transparent)
                .StrokeColor(MagickColors.OrangeRed)
                .Rectangle(5, 5, 200, 200);

            image.Draw(drawables);

            ColorAssert.Equal(MagickColors.OrangeRed, image, 7, 40);
            ColorAssert.Equal(MagickColors.OrangeRed, image, 7, 175);
            ColorAssert.Equal(MagickColors.OrangeRed, image, 200, 30);
            ColorAssert.Equal(MagickColors.OrangeRed, image, 200, 170);
        }
    }
}
