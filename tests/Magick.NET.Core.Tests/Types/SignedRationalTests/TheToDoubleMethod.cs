// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class RationalTests
    {
        public class SignedRationalTests
        {
            [Fact]
            public void ShouldReturnNanWhenNumeratorIsZero()
            {
                var rational = new SignedRational(0, 0);
                Assert.Equal(double.NaN, rational.ToDouble());
            }

            [Fact]
            public void ShouldReturnPositiveInfinityWhenDenominatorIsZero()
            {
                var rational = new SignedRational(2, 0);
                Assert.Equal(double.PositiveInfinity, rational.ToDouble());
            }

            [Fact]
            public void ShouldReturnNegativeInfinityWhenDenominatorIsZeroAndValueIsNegative()
            {
                var rational = new SignedRational(-2, 0);
                Assert.Equal(double.NegativeInfinity, rational.ToDouble());
            }
        }
    }
}
