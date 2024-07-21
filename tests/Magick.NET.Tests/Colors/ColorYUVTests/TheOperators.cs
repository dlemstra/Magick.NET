// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorYUVTests
{
    public class TheOperators
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
        {
            var color = new ColorYUV(0, 0, 0);

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
            var first = new ColorYUV(0, 0, 0);
            var second = new ColorYUV(0, 0, 0);

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
            var first = new ColorYUV(1, 0.5, 0.5);
            var second = new ColorYUV(0.5, 0.5, 0.5);

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
            var actual = (ColorYUV)new MagickColor("#BFFFDFFF9FFFFFFF");
            Assert.InRange(actual.Y, 0.80, 0.81);
            Assert.InRange(actual.U, 0.40, 0.41);
            Assert.InRange(actual.V, 0.44, 0.45);
        }
    }
}
