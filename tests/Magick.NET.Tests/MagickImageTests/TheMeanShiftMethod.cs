// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheMeanShiftMethod
    {
#if !Q16HDRI
        [Fact]
        public void ShouldThrowExceptionWhenColorDistanceIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("colorDistance", () => image.MeanShift(1, 1, new Percentage(-1)));
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorDistanceIsNegativeAndSizeIsSpecified()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("colorDistance", () => image.MeanShift(1, new Percentage(-1)));
        }
#endif

        [Fact]
        public void ShouldNotChangeImageWhenSizeIsOne()
        {
            using var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
            using var output = input.Clone();
            output.MeanShift(1);

            Assert.Equal(0.0, output.Compare(input, ErrorMetric.RootMeanSquared));
        }

        [Fact]
        public void ShouldChangeImage()
        {
            using var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
            using var output = input.Clone();
            output.MeanShift(2, new Percentage(80));

            Assert.InRange(output.Compare(input, ErrorMetric.RootMeanSquared), 0.019, 0.020);
        }
    }
}
