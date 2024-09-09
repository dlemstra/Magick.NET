// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorHSVTests
{
    public class TheOperators
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
        {
            var color = new ColorHSV(0, 0, 0);

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
            var first = new ColorHSV(0, 0, 0);
            var second = new ColorHSV(0, 0, 0);

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
            var first = new ColorHSV(1, 0.5, 0.5);
            var second = new ColorHSV(0.5, 0.5, 0.5);

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
            var actual = (ColorHSV?)new MagickColor("#BFFFDFFF9FFFFFFF");

            Assert.NotNull(actual);
            Assert.InRange(actual.Hue, 0.24, 0.26);
            Assert.InRange(actual.Saturation, 0.28, 0.29);
            Assert.InRange(actual.Value, 0.87, 0.88);
        }
    }
}
