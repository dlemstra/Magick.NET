// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorRGBTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenOtherIsNull()
        {
            var color = new ColorRGB(1, 2, 3);

            Assert.False(color.Equals(null));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherAsObjectIsNull()
        {
            var color = new ColorRGB(1, 2, 3);

            Assert.False(color.Equals((object)null!));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsEqual()
        {
            var color = new ColorRGB(1, 2, 3);
            var other = new ColorRGB(1, 2, 3);

            Assert.True(color.Equals(other));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherAsObjectIsEqual()
        {
            var color = new ColorRGB(1, 2, 3);
            var other = new ColorRGB(1, 2, 3);

            Assert.True(color.Equals((object)other));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotEqual()
        {
            var color = new ColorRGB(3, 2, 1);
            var other = new ColorRGB(1, 2, 3);

            Assert.False(color.Equals(other));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherAsObjectIsNotEqual()
        {
            var color = new ColorRGB(3, 2, 1);
            var other = new ColorRGB(1, 2, 3);

            Assert.False(color.Equals((object)other));
        }
    }
}
