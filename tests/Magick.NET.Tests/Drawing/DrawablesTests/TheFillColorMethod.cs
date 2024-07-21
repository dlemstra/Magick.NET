// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests;

public partial class DrawablesTests
{
    public class TheFillColorMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorIsNull()
        {
            Assert.Throws<ArgumentNullException>("color", () =>
            {
                new Drawables().FillColor(null);
            });
        }

        [Fact]
        public void ShouldSupportCmykColors()
        {
            using var image = new MagickImage("xc:purple", 90, 40);
            image.ColorSpace = ColorSpace.CMYK;

            var trueBlack = new MagickColor(
                (QuantumType)(Quantum.Max * 0.75),
                (QuantumType)(Quantum.Max * 0.68),
                (QuantumType)(Quantum.Max * 0.67),
                (QuantumType)(Quantum.Max * 0.90),
                Quantum.Max);

            var drawables = new Drawables()
                .FillColor(trueBlack)
                .DisableTextAntialias()
                .FontPointSize(25)
                .Text(10, 30, "CMYK")
                .Draw(image);

            var colors = image.Histogram();

            Assert.Equal(2, colors.Count);

            var color = colors.Keys.FirstOrDefault(color => color.R != 0);
            Assert.NotNull(color);
            Assert.True(color.IsCmyk);
            Assert.Equal((int)trueBlack.R, (int)color.R);
            Assert.Equal((int)trueBlack.G, (int)color.G);
            Assert.Equal((int)trueBlack.B, (int)color.B);
            Assert.Equal((int)trueBlack.K, (int)color.K);
            Assert.Equal((int)trueBlack.A, (int)color.A);

            Assert.Equal(ColorSpace.CMYK, image.ColorSpace);
        }
    }
}
