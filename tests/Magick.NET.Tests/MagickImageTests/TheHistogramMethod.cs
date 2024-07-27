// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheHistogramMethod
    {
        [Fact]
        public void ShouldReturnEmptyHistogramForEmptyImage()
        {
            using var image = new MagickImage();
            var histogram = image.Histogram();

            Assert.NotNull(histogram);
            Assert.Empty(histogram);
        }

        [Fact]
        public void ShouldReturnHistogramOfTheImage()
        {
            using var image = new MagickImage(Files.RedPNG);
            var histogram = image.Histogram();

            Assert.NotNull(histogram);
            Assert.Equal(3, histogram.Count);

            var red = new MagickColor(Quantum.Max, 0, 0);
            var alphaRed = new MagickColor(Quantum.Max, 0, 0, 0);
            var halfAlphaRed = new MagickColor("#FF000080");

            Assert.Equal(3, histogram.Count);
            Assert.Equal(50000U, histogram[red]);
            Assert.Equal(30000U, histogram[alphaRed]);
            Assert.Equal(40000U, histogram[halfAlphaRed]);
        }
    }
}
