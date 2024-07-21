// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
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

public partial class ColorRGBTests
{
    public class TheFromMagickColorMethod
    {
        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            var result = ColorRGB.FromMagickColor(null);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldInitializeTheProperties()
        {
            var color = new MagickColor(Quantum.Max, (QuantumType)(Quantum.Max * 0.75), (QuantumType)(Quantum.Max * 0.5));
            var rgbColor = ColorRGB.FromMagickColor(color);

            Assert.Equal(Quantum.Max, rgbColor.R);
            Assert.Equal((QuantumType)(Quantum.Max * 0.75), rgbColor.G);
            Assert.Equal((QuantumType)(Quantum.Max * 0.5), rgbColor.B);
        }
    }
}
