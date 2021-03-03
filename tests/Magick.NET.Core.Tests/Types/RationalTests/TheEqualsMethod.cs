// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class RationalTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                var density = new Rational(3, 2);

                Assert.False(density.Equals(null));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var density = new Rational(3, 2);

                Assert.True(density.Equals(density));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var density = new Rational(3, 2);

                Assert.True(density.Equals((object)density));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new Rational(3, 2);
                var second = new Rational(3, 2);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new Rational(3, 2);
                var second = new Rational(3, 2);

                Assert.True(first.Equals((object)second));
            }

            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new Rational(3, 2);
                var second = new Rational(2, 3);

                Assert.False(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new Rational(3, 2);
                var second = new Rational(2, 3);

                Assert.False(first.Equals((object)second));
            }

            [Fact]
            public void ShouldHandleFractionCorrectly()
            {
                var first = new Rational(1.0 / 1600);
                var second = new Rational(1.0 / 1600, true);

                Assert.False(first.Equals(second));
            }
        }
    }
}
