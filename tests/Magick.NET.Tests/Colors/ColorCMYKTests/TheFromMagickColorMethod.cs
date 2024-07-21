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

public partial class ColorCMYKTests
{
    public class TheFromMagickColorMethod
    {
        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            var result = ColorCMYK.FromMagickColor(null);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldInitializeTheProperties()
        {
            var color = new MagickColor(Quantum.Max, (QuantumType)(Quantum.Max * 0.75), (QuantumType)(Quantum.Max * 0.5), (QuantumType)(Quantum.Max * 0.25));
            var cmykColor = ColorCMYK.FromMagickColor(color);

            Assert.InRange(Quantum.ScaleToDouble(cmykColor.C), 0.99, 1.0);
            Assert.InRange(Quantum.ScaleToDouble(cmykColor.M), 0.74, 0.75);
            Assert.InRange(Quantum.ScaleToDouble(cmykColor.Y), 0.49, 0.5);
            Assert.InRange(Quantum.ScaleToDouble(cmykColor.K), 0.0, 0.01);
            Assert.InRange(Quantum.ScaleToDouble(cmykColor.A), 0.24, 0.25);
        }
    }
}
