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

public partial class ColorHSVTests
{
    public class TheFromMagickColorMethod
    {
        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            var result = ColorHSV.FromMagickColor(null);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldInitializeTheProperties()
        {
            var color = new MagickColor(Quantum.Max, Quantum.Max, (QuantumType)(Quantum.Max * 0.02));
            var hslColor = ColorHSV.FromMagickColor(color);

            Assert.InRange(hslColor.Hue, 0.16, 0.17);
            Assert.InRange(hslColor.Saturation, 0.98, 0.99);
            Assert.InRange(hslColor.Value, 1.0, 1.01);
        }
    }
}
