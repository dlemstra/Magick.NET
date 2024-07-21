// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorCMYKTests
{
    public class TheCompareToMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenOtherIsNull()
        {
            var color = new ColorCMYK(1, 2, 3, 4);

            Assert.Equal(1, color.CompareTo(null));
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenOtherIsEqual()
        {
            var color = new ColorCMYK(1, 2, 3, 4);

            Assert.Equal(0, color.CompareTo(color));
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenOtherIsLower()
        {
            var color = new ColorCMYK(Quantum.Max, 2, 3, 4);
            var other = new ColorCMYK(1, 2, 3, 4);

            Assert.Equal(1, color.CompareTo(other));
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenOtherIsHigher()
        {
            var color = new ColorCMYK(1, 2, 3, 4);
            var other = new ColorCMYK(Quantum.Max, 2, 3, 4);

            Assert.Equal(-1, color.CompareTo(other));
        }
    }
}
