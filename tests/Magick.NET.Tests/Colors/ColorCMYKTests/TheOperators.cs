// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorCMYKTests
{
    public class TheOperators
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
        {
            var color = new ColorCMYK(0, 0, 0, 0);

            Assert.False(color == null);
            Assert.True(color != null);
            Assert.False(color < null);
            Assert.False(color <= null);
            Assert.True(color > null);
            Assert.True(color >= null);
            Assert.False(null == color);
            Assert.True(null != color);
            Assert.True(null < color);
            Assert.True(null <= color);
            Assert.False(null > color);
            Assert.False(null >= color);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstancesAreEqual()
        {
            var first = new ColorCMYK(0, 0, Quantum.Max, 0);
            var second = new ColorCMYK("#0000ff00");

            Assert.True(first == second);
            Assert.False(first != second);
            Assert.False(first < second);
            Assert.True(first <= second);
            Assert.False(first > second);
            Assert.True(first >= second);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstancesAreNotEqual()
        {
            var first = new ColorCMYK(Quantum.Max, 0, 0, 0);
            var second = new ColorCMYK(0, Quantum.Max, 0, 0);

            Assert.False(first == second);
            Assert.True(first != second);
            Assert.False(first < second);
            Assert.False(first <= second);
            Assert.True(first > second);
            Assert.True(first >= second);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenCastedFromMagickColor()
        {
            var expected = new ColorCMYK(Quantum.Max, 0, 0, 0);
            var actual = (ColorCMYK)new MagickColor(Quantum.Max, 0, 0, 0, Quantum.Max);
            Assert.Equal(expected, actual);
        }
    }
}
