// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class PercentageTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnCorrectValueWhenValuesAreEqual()
            {
                var first = new Percentage(100);
                var second = new Percentage(100);

                Assert.True(first == second);
                Assert.False(first != second);
                Assert.False(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnCorrectValueWhenValuesFirstIsHigher()
            {
                var first = new Percentage(100);
                var second = new Percentage(101);

                Assert.False(first == second);
                Assert.True(first != second);
                Assert.True(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.False(first >= second);
            }

            [Fact]
            public void ShouldReturnCorrectValueWhenValuesFirstIsLower()
            {
                var first = new Percentage(100);
                var second = new Percentage(50);

                Assert.False(first == second);
                Assert.True(first != second);
                Assert.False(first < second);
                Assert.False(first <= second);
                Assert.True(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnCorrectValueWhenMultiplying()
            {
                Percentage percentage = default;
                Assert.Equal(0, 10 * percentage);

                percentage = new Percentage(50);
                Assert.Equal(5, 10 * percentage);

                percentage = new Percentage(200);
                Assert.Equal(20.0, 10.0 * percentage);

                percentage = new Percentage(25);
                Assert.Equal(2.5, 10.0 * percentage);
            }
        }
    }
}
