// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorCMYKTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenOtherIsNull()
        {
            var color = new ColorCMYK(1, 2, 3, 4);

            Assert.False(color.Equals(null));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherAsObjectIsNull()
        {
            var color = new ColorCMYK(1, 2, 3, 4);

            Assert.False(color.Equals((object)null!));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsEqual()
        {
            var color = new ColorCMYK(1, 2, 3, 4);
            var other = new ColorCMYK(1, 2, 3, 4);

            Assert.True(color.Equals(other));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherAsObjectIsEqual()
        {
            var color = new ColorCMYK(1, 2, 3, 4);
            var other = new ColorCMYK(1, 2, 3, 4);

            Assert.True(color.Equals((object)other));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotEqual()
        {
            var color = new ColorCMYK(4, 3, 2, 1);
            var other = new ColorCMYK(1, 2, 3, 4);

            Assert.False(color.Equals(other));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherAsObjectIsNotEqual()
        {
            var color = new ColorCMYK(4, 3, 2, 1);
            var other = new ColorCMYK(1, 2, 3, 4);

            Assert.False(color.Equals((object)other));
        }
    }
}
