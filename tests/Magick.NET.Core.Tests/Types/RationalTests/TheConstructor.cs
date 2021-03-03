// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class RationalTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetTheProperties()
            {
                var rational = new Rational(7, 55);
                Assert.Equal(7U, rational.Numerator);
                Assert.Equal(55U, rational.Denominator);
            }

            [Fact]
            public void ShouldSetThePropertiesWhenOnlyValueIsSpecified()
            {
                var rational = new Rational(7);
                Assert.Equal(7U, rational.Numerator);
                Assert.Equal(1U, rational.Denominator);
            }

            [Fact]
            public void ShouldSimplifyByDefault()
            {
                var rational = new Rational(755, 100);
                Assert.Equal(151U, rational.Numerator);
                Assert.Equal(20U, rational.Denominator);
            }

            [Fact]
            public void ShouldNotSimplifyWhenSpecified()
            {
                var rational = new Rational(755, 100, false);
                Assert.Equal(755U, rational.Numerator);
                Assert.Equal(100U, rational.Denominator);
            }

            [Fact]
            public void ShouldHandleNegativeValue()
            {
                var rational = new Rational(-7.55);
                Assert.Equal(151U, rational.Numerator);
                Assert.Equal(20U, rational.Denominator);
            }
        }
    }
}
