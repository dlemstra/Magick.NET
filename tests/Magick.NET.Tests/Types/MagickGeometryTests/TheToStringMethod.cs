// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickGeometryTests
{
    public class TheToStringMethod
    {
        [Fact]
        public void ShouldOnlyReturnWidthAndHeight()
        {
            var geometry = new MagickGeometry(10, 5);

            Assert.Equal("10x5", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnCorrectValueForPositiveValues()
        {
            var geometry = new MagickGeometry(1, 2, 10, 20);

            Assert.Equal("10x20+1+2", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnCorrectValueForNegativeValues()
        {
            var geometry = new MagickGeometry(-1, -2, 20, 10);

            Assert.Equal("20x10-1-2", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnCorrectValueForIgnoreAspectRatio()
        {
            var geometry = new MagickGeometry(5, 10)
            {
                IgnoreAspectRatio = true,
            };

            Assert.Equal("5x10!", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnCorrectValueForLess()
        {
            var geometry = new MagickGeometry(2, 1, 10, 5)
            {
                Less = true,
            };

            Assert.Equal("10x5+2+1<", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnCorrectValueForGreater()
        {
            var geometry = new MagickGeometry(0, 10)
            {
                Greater = true,
            };

            Assert.Equal("x10>", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnCorrectValueForFillArea()
        {
            var geometry = new MagickGeometry(10, 15)
            {
                FillArea = true,
            };

            Assert.Equal("10x15^", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnCorrectValueForLimitPixels()
        {
            var geometry = new MagickGeometry(10, 0)
            {
                LimitPixels = true,
            };

            Assert.Equal("10x@", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnCorrectValueForAspectRation()
        {
            var geometry = new MagickGeometry(3, 2)
            {
                AspectRatio = true,
            };

            Assert.Equal("3:2", geometry.ToString());
        }

        [Fact]
        public void ShouldSetGreaterAndIsPercentage()
        {
            var geometry = new MagickGeometry(new Percentage(50), new Percentage(0))
            {
                Greater = true,
            };

            Assert.Equal("50%>", geometry.ToString());
        }

        [Fact]
        public void ShouldIncludeZeroXYWhenSpecified()
        {
            var geometry = new MagickGeometry(0, 0, 5, 10);

            Assert.Equal("5x10+0+0", geometry.ToString());
        }

        [Fact]
        public void ShouldReturnTheCorrectStringWhenBothWidthAndHeightAreZero()
        {
            var geometry = new MagickGeometry(0, 0);

            Assert.Equal("0x0", geometry.ToString());
        }

        [Fact]
        public void ShouldCreateTheCorrectStringWhenAllValuesAreZero()
        {
            var geometry = new MagickGeometry(0, 0, 0, 0);

            Assert.Equal("0x0+0+0", geometry.ToString());
        }

        [Theory]
        [InlineData("0x0+0+0")]
        [InlineData("0x0-0+0")]
        [InlineData("0x0+0-0")]
        [InlineData("0x0-0-0")]
        public void ShouldIncludeZeroXYWhenSpecifiedInStringConstructor(string value)
        {
            var geometry = new MagickGeometry(value);

            Assert.Equal("0x0+0+0", geometry.ToString());
        }
    }
}
