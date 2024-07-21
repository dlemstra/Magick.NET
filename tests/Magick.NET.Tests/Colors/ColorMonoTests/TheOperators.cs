// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorMonoTests
{
    public class TheOperators
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
        {
            var color = ColorMono.White;

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
            var first = ColorMono.Black;
            var second = ColorMono.Black;

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
            var first = ColorMono.White;
            var second = ColorMono.Black;

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
            var actual = (ColorMono)MagickColors.White;
            Assert.False(actual.IsBlack);
        }
    }
}
