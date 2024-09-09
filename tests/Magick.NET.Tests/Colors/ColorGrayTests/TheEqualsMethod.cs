// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorGrayTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenOtherIsNull()
        {
            var color = new ColorGray(1);

            Assert.False(color.Equals(null));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherAsObjectIsNull()
        {
            var color = new ColorGray(1);

            Assert.False(color.Equals((object)null!));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsEqual()
        {
            var color = new ColorGray(1);
            var other = new ColorGray(1);

            Assert.True(color.Equals(other));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherAsObjectIsEqual()
        {
            var color = new ColorGray(1);
            var other = new ColorGray(1);

            Assert.True(color.Equals((object)other));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotEqual()
        {
            var color = new ColorGray(1);
            var other = new ColorGray(0.5);

            Assert.False(color.Equals(other));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherAsObjectIsNotEqual()
        {
            var color = new ColorGray(1);
            var other = new ColorGray(0.5);

            Assert.False(color.Equals((object)other));
        }
    }
}
