// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheChromaProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectDefaultValues()
        {
            using var image = new MagickImage(Files.SnakewarePNG);

            Assert.InRange(image.Chromaticity.Red.X, 0.64, 0.641);
            Assert.InRange(image.Chromaticity.Red.Y, 0.33, 0.331);
            Assert.InRange(image.Chromaticity.Red.Z, 0.00, 0.001);

            Assert.InRange(image.Chromaticity.Blue.X, 0.15, 0.151);
            Assert.InRange(image.Chromaticity.Blue.Y, 0.06, 0.061);
            Assert.InRange(image.Chromaticity.Blue.Z, 0.00, .001);

            Assert.InRange(image.Chromaticity.Green.X, 0.30, 0.301);
            Assert.InRange(image.Chromaticity.Green.Y, 0.60, 0.601);
            Assert.InRange(image.Chromaticity.Green.Z, 0.00, 0.001);

            Assert.InRange(image.Chromaticity.White.X, 0.3127, 0.31271);
            Assert.InRange(image.Chromaticity.White.Y, 0.329, 0.3291);
            Assert.InRange(image.Chromaticity.White.Z, 0.00, 0.001);
        }

        [Fact]
        public void ShouldHaveTheCorrectValuesWhenChanged()
        {
            using var image = new MagickImage(Files.SnakewarePNG);

            var chromaticity = new ChromaticityInfo(
                new PrimaryInfo(0.5, 1.0, 1.5),
                new PrimaryInfo(0.6, 2.0, 2.5),
                new PrimaryInfo(0.7, 3.0, 3.5),
                new PrimaryInfo(0.8, 4.0, 4.5));

            image.Chromaticity = chromaticity;

            Assert.InRange(image.Chromaticity.Red.X, 0.50, 0.501);
            Assert.InRange(image.Chromaticity.Red.Y, 1.00, 1.001);
            Assert.InRange(image.Chromaticity.Red.Z, 1.50, 1.501);

            Assert.InRange(image.Chromaticity.Green.X, 0.60, 0.601);
            Assert.InRange(image.Chromaticity.Green.Y, 2.00, 2.001);
            Assert.InRange(image.Chromaticity.Green.Z, 2.50, 2.501);

            Assert.InRange(image.Chromaticity.Blue.X, 0.70, 0.701);
            Assert.InRange(image.Chromaticity.Blue.Y, 3.00, 3.001);
            Assert.InRange(image.Chromaticity.Blue.Z, 3.50, 3.501);

            Assert.InRange(image.Chromaticity.White.X, 0.80, 0.801);
            Assert.InRange(image.Chromaticity.White.Y, 4.00, 4.001);
            Assert.InRange(image.Chromaticity.White.Z, 4.50, 4.501);
        }
    }
}
