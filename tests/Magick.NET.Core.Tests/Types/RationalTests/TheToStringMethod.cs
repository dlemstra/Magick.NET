// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class RationalTests
    {
        public class TheToStringMethod
        {
            [Fact]
            public void ShouldReturnPositiveInfinityWhenValueIsNan()
            {
                var rational = new Rational(double.NaN);
                Assert.Equal("Indeterminate", rational.ToString());
            }

            [Fact]
            public void ShouldReturnPositiveInfinityWhenValueIsPositiveInfinity()
            {
                var rational = new Rational(double.PositiveInfinity);
                Assert.Equal("PositiveInfinity", rational.ToString());
            }

            [Fact]
            public void ShouldReturnPositiveInfinityWhenValueIsNegativeInfinity()
            {
                var rational = new Rational(double.NegativeInfinity);
                Assert.Equal("PositiveInfinity", rational.ToString());
            }

            [Fact]
            public void ShouldReturnTheCorrectValue()
            {
                var rational = new Rational(0, 1);
                Assert.Equal("0", rational.ToString());

                rational = new Rational(2, 1);
                Assert.Equal("2", rational.ToString());

                rational = new Rational(1, 2);
                Assert.Equal("1/2", rational.ToString());
            }
        }
    }
}
