// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
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
    public class TheFuzzyEqualsMethod
    {
#if !Q16HDRI
        [Fact]
        public void ShouldThrowExceptionWhenFuzzIsNegative()
        {
            var first = MagickColors.White;
            var second = MagickColors.White;

            Assert.Throws<ArgumentException>("fuzz", () => first.FuzzyEquals(second, new Percentage(-1)));
        }
#endif

        [Fact]
        public void ShouldReturnFalseWhenValueIsNull()
        {
            var first = MagickColors.White;

            Assert.False(first.FuzzyEquals(null, (Percentage)0));
        }

        [Fact]
        public void ShouldReturnTrueWhenValuesAreSame()
        {
            var first = MagickColors.White;

            Assert.True(first.FuzzyEquals(first, (Percentage)0));
        }

        [Fact]
        public void ShouldReturnTrueWhenValuesAreEqual()
        {
            var first = MagickColors.White;
            var second = new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max);

            Assert.True(first.FuzzyEquals(second, (Percentage)0));
        }

        [Fact]
        public void ShouldReturnTheCorrectValue()
        {
            var first = new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max);

            var half = (QuantumType)(Quantum.Max / 2.0);
            var second = new MagickColor(Quantum.Max, half, Quantum.Max);

            Assert.False(first.FuzzyEquals(second, (Percentage)0));
            Assert.False(first.FuzzyEquals(second, (Percentage)10));
            Assert.False(first.FuzzyEquals(second, (Percentage)20));
            Assert.True(first.FuzzyEquals(second, (Percentage)30));
        }
    }
}
