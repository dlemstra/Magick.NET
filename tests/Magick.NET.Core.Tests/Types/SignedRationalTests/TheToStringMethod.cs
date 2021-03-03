// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class SignedRationalTests
    {
        public class TheToStringMethod
        {
            [Fact]
            public void ShouldReturnPositiveInfinityWhenValueIsNan()
            {
                var rational = new SignedRational(double.NaN);
                Assert.Equal("Indeterminate", rational.ToString());
            }

            [Fact]
            public void ShouldReturnPositiveInfinityWhenValueIsPositiveInfinity()
            {
                var rational = new SignedRational(double.PositiveInfinity);
                Assert.Equal("PositiveInfinity", rational.ToString());
            }

            [Fact]
            public void ShouldReturnNegativeInfinityWhenValueIsNegativeInfinity()
            {
                var rational = new SignedRational(double.NegativeInfinity);
                Assert.Equal("NegativeInfinity", rational.ToString());
            }

            [Fact]
            public void ShouldReturnTheCorrectValue()
            {
                var rational = new SignedRational(0, 1);
                Assert.Equal("0", rational.ToString());

                rational = new SignedRational(-2, 1);
                Assert.Equal("-2", rational.ToString());

                rational = new SignedRational(-1, 2);
                Assert.Equal("-1/2", rational.ToString());
            }
        }
    }
}
