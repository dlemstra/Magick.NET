// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheClaheMethod
    {
        [Fact]
        public void ShouldChangeTheImage()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
            using var result = image.Clone();
            result.Clahe(10, 20, 30, 1.5);

            Assert.InRange(image.Compare(result, ErrorMetric.RootMeanSquared), 0.08, 0.09);
        }

        [Fact]
        public void ShouldUsePercentageOfTheWidthAndHeight()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
            using var result = image.Clone();
            result.Clahe(new Percentage(1.6666), new Percentage(5), 30, 1.5);

            Assert.InRange(image.Compare(result, ErrorMetric.RootMeanSquared), 0.07, 0.08);
        }
    }
}
