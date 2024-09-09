// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DensityExtensionsTests
{
    public class TheToGeometryMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectValue()
        {
            var density = new Density(50.0);

            var geometry = density.ToGeometry(0.5, 2.0);

            Assert.NotNull(geometry);
            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
            Assert.Equal(25U, geometry.Width);
            Assert.Equal(100U, geometry.Height);
        }
    }
}
