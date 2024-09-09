// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickGeometryFactoryTests
{
    public partial class TheCreateFromPageSizeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenValueIsNull()
        {
            var factory = new MagickGeometryFactory();

            Assert.Throws<ArgumentNullException>("pageSize", () => factory.CreateFromPageSize(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsEmpty()
        {
            var factory = new MagickGeometryFactory();

            Assert.Throws<ArgumentException>("pageSize", () => factory.CreateFromPageSize(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPageSizeIsInvalid()
        {
            var factory = new MagickGeometryFactory();
            var exception = Assert.Throws<InvalidOperationException>(() => factory.CreateFromPageSize("invalid"));

            Assert.Equal("Invalid page size specified.", exception.Message);
        }

        [Fact]
        public void ShouldReturnTheCorrectGeometry()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.CreateFromPageSize("a4");

            Assert.Equal(595U, geometry.Width);
            Assert.Equal(842U, geometry.Height);
            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
        }

        [Fact]
        public void ShouldSetTheXAndYPosition()
        {
            var factory = new MagickGeometryFactory();
            var geometry = factory.CreateFromPageSize("a4+3+2");

            Assert.Equal(595U, geometry.Width);
            Assert.Equal(842U, geometry.Height);
            Assert.Equal(3, geometry.X);
            Assert.Equal(2, geometry.Y);
        }
    }
}
