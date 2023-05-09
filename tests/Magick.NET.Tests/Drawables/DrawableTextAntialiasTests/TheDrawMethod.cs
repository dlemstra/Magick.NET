// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableTextAntialiasTests
{
    public class TheDrawMethod
    {
        [Fact]
        public void ShouldDrawOnDrawingWand()
        {
            IDrawingWand drawableTextAntialias = DrawableTextAntialias.Enabled;

            using var image = new MagickImage();
            var wand = new DrawingWand(image);
            drawableTextAntialias.Draw(wand);
        }
    }
}
