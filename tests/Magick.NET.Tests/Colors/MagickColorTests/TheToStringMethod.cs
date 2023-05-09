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

public partial class MagickColorTests
{
    public class TheToStringMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectString()
        {
            var color = new MagickColor(MagickColors.Red);
#if Q8
            Assert.Equal("#FF0000FF", color.ToString());
#else
            Assert.Equal("#FFFF00000000FFFF", color.ToString());
#endif
        }

        [Fact]
        public void ShouldReturnTheCorrectStringForCmykColor()
        {
            var color = new MagickColor(0, Quantum.Max, 0, 0, (QuantumType)(Quantum.Max / 3));
            Assert.Equal("cmyka(0,255,0,0,0.3333)", color.ToString());

            color = new MagickColor(0, Quantum.Max, 0, 0, Quantum.Max);
            Assert.Equal("cmyka(0,255,0,0,1.0)", color.ToString());
        }
    }
}
