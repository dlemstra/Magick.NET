// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class PercentageTests
    {
        public class TheCompareToMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenValuesAreSame()
            {
                var first = new Percentage(100);

                Assert.Equal(0, first.CompareTo(first));
            }

            [Fact]
            public void ShouldReturnZeroWhenValuesAreEqual()
            {
                var first = new Percentage(100);
                var second = new Percentage(100);

                Assert.Equal(0, first.CompareTo(second));
            }

            [Fact]
            public void ShouldReturnMinusOneWhenValueIsHigher()
            {
                var first = new Percentage(100);
                var second = new Percentage(101);

                Assert.Equal(-1, first.CompareTo(second));
            }

            [Fact]
            public void ShouldReturnOneWhenValueIsLower()
            {
                var first = new Percentage(100);
                var second = new Percentage(50);

                Assert.Equal(1, first.CompareTo(second));
            }
        }
    }
}
