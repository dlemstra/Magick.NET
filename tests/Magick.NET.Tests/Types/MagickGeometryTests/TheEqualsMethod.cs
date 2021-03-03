// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickGeometryTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                var geometry = new MagickGeometry(10, 5);

                Assert.False(geometry.Equals(null));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var geometry = new MagickGeometry(10, 5);

                Assert.True(geometry.Equals(geometry));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var geometry = new MagickGeometry(10, 5);

                Assert.True(geometry.Equals((object)geometry));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(10, 5);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(10, 5);

                Assert.True(first.Equals((object)second));
            }

            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(5, 10);

                Assert.False(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(5, 10);

                Assert.False(first.Equals((object)second));
            }
        }
    }
}
