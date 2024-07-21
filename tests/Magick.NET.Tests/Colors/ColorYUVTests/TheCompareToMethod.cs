// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorYUVTests
{
    public class TheCompareToMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenOtherIsNull()
        {
            var color = new ColorYUV(1, 2, 3);

            Assert.Equal(1, color.CompareTo(null));
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenOtherIsEqual()
        {
            var color = new ColorYUV(1, 2, 3);

            Assert.Equal(0, color.CompareTo(color));
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenOtherIsLower()
        {
            var color = new ColorYUV(1.0, 1.0, 1.0);
            var other = new ColorYUV(0.5, 0.5, 0.5);

            Assert.Equal(1, color.CompareTo(other));
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenOtherIsHigher()
        {
            var color = new ColorYUV(0.5, 0.5, 0.5);
            var other = new ColorYUV(1.0, 1.0, 1.0);

            Assert.Equal(-1, color.CompareTo(other));
        }
    }
}
