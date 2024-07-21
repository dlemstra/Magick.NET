// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableDensityTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldSetTheProperties()
        {
            var density = new DrawableDensity(new PointD(4, 2));
            Assert.Equal(4, density.Density.X);
            Assert.Equal(2, density.Density.Y);
        }
    }
}
