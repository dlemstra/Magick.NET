// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests;

public partial class MagickColorTests
{
    public class TheToColorMethod
    {
        [Fact]
        public void ShouldConvertRgbColorToRgba()
        {
            var rgbColor = new ColorRGB(Quantum.Max, 0, 0);
            var magickColor = rgbColor.ToMagickColor();

            var color = magickColor.ToColor();
            Assert.Equal(255, color.R);
            Assert.Equal(0, color.G);
            Assert.Equal(0, color.B);
            Assert.Equal(255, color.A);
        }

        [Fact]
        public void ShouldConvertCmykColorToRgba()
        {
            var cmkyColor = new ColorCMYK(Quantum.Max, 0, 0, 0, Quantum.Max);
            var magickColor = cmkyColor.ToMagickColor();

            var color = magickColor.ToColor();
            Assert.Equal(0, color.R);
            Assert.Equal(255, color.G);
            Assert.Equal(255, color.B);
            Assert.Equal(255, color.A);
        }
    }
}
