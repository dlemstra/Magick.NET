// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class PercentageTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                Percentage percentage = default;

                Assert.False(percentage.Equals(null));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                Percentage percentage = default;

                Assert.True(percentage.Equals(percentage));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                Percentage percentage = default;

                Assert.True(percentage.Equals((object)percentage));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new Percentage(50.0);
                var second = new Percentage(50.0);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new Percentage(50.0);
                var second = new Percentage(50.0);

                Assert.True(first.Equals((object)second));
            }

            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new Percentage(50.0);
                var second = new Percentage(50.1);

                Assert.False(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new Percentage(50.0);
                var second = new Percentage(50.1);

                Assert.False(first.Equals((object)second));
            }
        }
    }
}
