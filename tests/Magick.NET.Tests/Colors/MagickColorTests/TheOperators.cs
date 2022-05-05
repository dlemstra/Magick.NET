// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickColorTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var color = MagickColors.Red;

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
            public void ShouldReturnTheCorrectValueWhenInstanceIsSpecified()
            {
                var first = MagickColors.Red;
                var second = MagickColors.Green;

                Assert.False(first == second);
                Assert.True(first != second);
                Assert.False(first < second);
                Assert.False(first <= second);
                Assert.True(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceAreEqual()
            {
                var first = MagickColors.Red;
                var second = new MagickColor("red");

                Assert.True(first == second);
                Assert.False(first != second);
                Assert.False(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.True(first >= second);
            }
        }
    }
}
