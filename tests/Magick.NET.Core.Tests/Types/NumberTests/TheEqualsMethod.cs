// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class NumberTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var number = new Number(10);

                Assert.True(number.Equals(number));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var number = new Number(10);

                Assert.True(number.Equals((object)number));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new Number(10);
                var second = new Number(10);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new Number(10);
                var second = new Number(10);

                Assert.True(first.Equals((object)second));
            }

            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new Number(10);
                var second = new Number(20);

                Assert.False(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new Number(10);
                var second = new Number(20);

                Assert.False(first.Equals((object)second));
            }
        }
    }
}
