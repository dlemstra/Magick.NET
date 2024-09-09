// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorHSLTests
{
    public class TheOperators
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
        {
            var color = new ColorHSL(0, 0, 0);

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
            var first = new ColorHSL(0, 0, 0);
            var second = new ColorHSL(0, 0, 0);

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
            var first = new ColorHSL(1, 0.5, 0.5);
            var second = new ColorHSL(0.5, 0.5, 0.5);

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
            var actual = (ColorHSL?)new MagickColor("#BFFFDFFF9FFFFFFF");

            Assert.NotNull(actual);
            Assert.InRange(actual.Hue, 0.24, 0.26);
            Assert.InRange(actual.Saturation, 0.49, 0.51);
            Assert.InRange(actual.Lightness, 0.74, 0.76);
        }
    }
}
