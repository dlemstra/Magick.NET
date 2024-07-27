// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheResampleMethod
    {
        [Fact]
        public void ShouldResizeTheImageWithTheSpecifiedDensity()
        {
            using var image = new MagickImage("xc:red", 100, 100);
            image.Resample(new PointD(300));

            Assert.Equal(300, image.Density.X);
            Assert.Equal(300, image.Density.Y);
            Assert.NotEqual(100U, image.Width);
            Assert.NotEqual(100U, image.Height);
        }

        [Fact]
        public void ShouldResizeTheImageWithTheSpecifiedResolutions()
        {
            using var image = new MagickImage("xc:red", 100, 100);
            image.Resample(300, 150);

            Assert.Equal(300, image.Density.X);
            Assert.Equal(150, image.Density.Y);
            Assert.NotEqual(100U, image.Width);
            Assert.NotEqual(100U, image.Height);
        }
    }
}
