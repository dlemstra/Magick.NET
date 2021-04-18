// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickGeometryTests
    {
        public class TheFromPageSizeMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenPageSizeIsNull()
            {
                Assert.Throws<ArgumentNullException>("pageSize", () => MagickGeometry.FromPageSize(null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPageSizeIsEmpty()
            {
                Assert.Throws<ArgumentException>("pageSize", () => MagickGeometry.FromPageSize(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPageSizeIsInvalid()
            {
                var exception = Assert.Throws<InvalidOperationException>(() => MagickGeometry.FromPageSize("invalid"));

                Assert.Equal("Invalid page size specified.", exception.Message);
            }

            [Fact]
            public void ShouldReturnTheCorrectGeometry()
            {
                var geometry = MagickGeometry.FromPageSize("a4");

                Assert.Equal(595, geometry.Width);
                Assert.Equal(842, geometry.Height);
                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
            }

            [Fact]
            public void ShouldSetTheXAndYPosition()
            {
                var geometry = MagickGeometry.FromPageSize("a4+3+2");

                Assert.Equal(595, geometry.Width);
                Assert.Equal(842, geometry.Height);
                Assert.Equal(3, geometry.X);
                Assert.Equal(2, geometry.Y);
            }
        }
    }
}
