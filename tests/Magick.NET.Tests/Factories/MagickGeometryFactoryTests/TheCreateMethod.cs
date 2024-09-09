// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickGeometryFactoryTests
{
    public partial class TheCreateMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenValueIsNull()
        {
            var factory = new MagickGeometryFactory();

            Assert.Throws<ArgumentNullException>("value", () => factory.Create(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsEmpty()
        {
            var factory = new MagickGeometryFactory();
            Assert.Throws<ArgumentException>("value", () => factory.Create(string.Empty));
        }

        [Fact]
        public void ShouldSetIgnoreAspectRatio()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("5x10!");

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(5U, geometry.Width);
            Assert.Equal(10U, geometry.Height);
            Assert.True(geometry.IgnoreAspectRatio);
        }

        [Fact]
        public void ShouldSetLess()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("10x5+2+1<");

            Assert.Equal(2, geometry.X);
            Assert.Equal(1, geometry.Y);
            Assert.Equal(10U, geometry.Width);
            Assert.Equal(5U, geometry.Height);
            Assert.True(geometry.Less);
        }

        [Fact]
        public void ShouldSetGreater()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("5x10>");

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(5U, geometry.Width);
            Assert.Equal(10U, geometry.Height);
            Assert.True(geometry.Greater);
        }

        [Fact]
        public void ShouldSetFillArea()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("10x15^");

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(10U, geometry.Width);
            Assert.Equal(15U, geometry.Height);
            Assert.True(geometry.FillArea);
        }

        [Fact]
        public void ShouldSetLimitPixels()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("10@");

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(10U, geometry.Width);
            Assert.Equal(0U, geometry.Height);
            Assert.True(geometry.LimitPixels);
        }

        [Fact]
        public void ShouldSetGreaterAndIsPercentage()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("50%x0>");

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(50U, geometry.Width);
            Assert.Equal(0U, geometry.Height);
            Assert.True(geometry.IsPercentage);
            Assert.True(geometry.Greater);
        }

        [Fact]
        public void ShouldSetAspectRatio()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("3:2");

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(3U, geometry.Width);
            Assert.Equal(2U, geometry.Height);
            Assert.True(geometry.AspectRatio);
        }

        [Fact]
        public void ShouldSetAspectRatioWithOnlyXOffset()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("4:3+2");

            Assert.Equal(2, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(4U, geometry.Width);
            Assert.Equal(3U, geometry.Height);
            Assert.True(geometry.AspectRatio);
        }

        [Fact]
        public void ShouldSetAspectRatioWithOffset()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("4:3+2+1");

            Assert.Equal(2, geometry.X);
            Assert.Equal(1, geometry.Y);
            Assert.Equal(4U, geometry.Width);
            Assert.Equal(3U, geometry.Height);
            Assert.True(geometry.AspectRatio);
        }

        [Fact]
        public void ShouldSetAspectRatioWithNegativeOffset()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create("4:3-2+1");

            Assert.Equal(-2, geometry.X);
            Assert.Equal(1, geometry.Y);
            Assert.Equal(4U, geometry.Width);
            Assert.Equal(3U, geometry.Height);
            Assert.True(geometry.AspectRatio);
        }

        [Fact]
        public void ShouldSetWidthAndHeightWhenSizeIsSupplied()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create(5);

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(5U, geometry.Width);
            Assert.Equal(5U, geometry.Height);
        }

        [Fact]
        public void ShouldSetWidthAndHeight()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create(5, 10);

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(5U, geometry.Width);
            Assert.Equal(10U, geometry.Height);
        }

        [Fact]
        public void ShouldSetXAndY()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create(5, 10, 15, 20);

            Assert.Equal(5, geometry.X);
            Assert.Equal(10, geometry.Y);
            Assert.Equal(15U, geometry.Width);
            Assert.Equal(20U, geometry.Height);
        }

        [Fact]
        public void ShouldSetWidthAndHeightAndIsPercentage()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create(new Percentage(50.0), new Percentage(10.0));

            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(50U, geometry.Width);
            Assert.Equal(10U, geometry.Height);
            Assert.True(geometry.IsPercentage);
        }

        [Fact]
        public void ShouldSetXAndYAndIsPercentage()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.Create(5, 10, (Percentage)15.0, (Percentage)20.0);

            Assert.Equal(5, geometry.X);
            Assert.Equal(10, geometry.Y);
            Assert.Equal(15U, geometry.Width);
            Assert.Equal(20U, geometry.Height);
            Assert.True(geometry.IsPercentage);
        }
    }
}
