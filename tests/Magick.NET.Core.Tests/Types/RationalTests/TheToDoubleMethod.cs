// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class RationalTests
    {
        public class TheToDoubleMethod
        {
            [Fact]
            public void ShouldReturnNanWhenNumeratorIsZero()
            {
                var rational = new Rational(0, 0);
                Assert.Equal(double.NaN, rational.ToDouble());
            }

            [Fact]
            public void ShouldReturnPositiveInfinityWhenDenominatorIsZero()
            {
                var rational = new Rational(2, 0);
                Assert.Equal(double.PositiveInfinity, rational.ToDouble());
            }

            [Fact]
            public void ShouldReturnPositiveInfinityWhenValueIsNegativeInfinity()
            {
                var rational = new Rational(double.NegativeInfinity);
                Assert.Equal(double.PositiveInfinity, rational.ToDouble());
            }
        }
    }
}
