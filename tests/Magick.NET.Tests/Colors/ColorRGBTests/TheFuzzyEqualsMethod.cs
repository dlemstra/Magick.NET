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
    public class TheFuzzyEqualsMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenOtherIsNull()
        {
            var color = new ColorRGB(Quantum.Max, Quantum.Max, Quantum.Max);

            Assert.False(color.FuzzyEquals(null, (Percentage)0));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsSame()
        {
            var color = new ColorRGB(Quantum.Max, Quantum.Max, Quantum.Max);

            Assert.True(color.FuzzyEquals(color, (Percentage)0));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsEqual()
        {
            var first = new ColorRGB(Quantum.Max, Quantum.Max, Quantum.Max);
            var second = (ColorRGB)new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max);

            Assert.True(first.FuzzyEquals(second, (Percentage)0));
        }

        [Fact]
        public void ShouldUseThePercentage()
        {
            var first = new ColorRGB(Quantum.Max, Quantum.Max, Quantum.Max);
            var second = new ColorRGB(Quantum.Max, (QuantumType)(Quantum.Max / 2.0), Quantum.Max);

            Assert.False(first.FuzzyEquals(second, (Percentage)0));
            Assert.False(first.FuzzyEquals(second, (Percentage)10));
            Assert.False(first.FuzzyEquals(second, (Percentage)20));
            Assert.True(first.FuzzyEquals(second, (Percentage)30));
        }
    }
}
