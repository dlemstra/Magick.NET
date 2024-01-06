// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
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

public partial class ColorYUVTests
{
    public class TheFromMagickColorMethod
    {
        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            var result = ColorYUV.FromMagickColor(null);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldInitializeTheProperties()
        {
            var color = new MagickColor(Quantum.Max, Quantum.Max, (QuantumType)(Quantum.Max * 0.02));
            var hslColor = ColorYUV.FromMagickColor(color);

            Assert.InRange(hslColor.Y, 0.88, 0.89);
            Assert.InRange(hslColor.U, 0.07, 0.08);
            Assert.InRange(hslColor.V, 0.59, 0.60);
        }
    }
}
