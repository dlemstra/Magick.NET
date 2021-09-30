// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ColorMonoTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var color = new ColorMono(false);

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
                var first = new ColorMono(true);
                var second = new ColorMono(true);

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
                var first = new ColorMono(false);
                var second = new ColorMono(true);

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
                ColorMono actual = MagickColors.White;
                Assert.False(actual.IsBlack);
            }
        }
    }
}
