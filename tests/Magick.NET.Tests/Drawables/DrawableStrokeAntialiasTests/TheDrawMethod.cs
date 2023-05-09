// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableStrokeAntialiasTests
{
    public class TheDrawMethod
    {
        [Fact]
        public void ShouldDrawOnDrawingWand()
        {
            IDrawingWand drawableStrokeAntialias = DrawableStrokeAntialias.Enabled;

            using var image = new MagickImage();
            var wand = new DrawingWand(image);
            drawableStrokeAntialias.Draw(wand);
        }
    }
}
