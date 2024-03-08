// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheStatisticMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            using var image = new MagickImage(Files.NoisePNG);

            Assert.Throws<ArgumentException>("width", () => image.Statistic(StatisticType.Minimum, -1, 1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            using var image = new MagickImage(Files.NoisePNG);

            Assert.Throws<ArgumentException>("height", () => image.Statistic(StatisticType.Minimum, 10, -1));
        }

        [Fact]
        public void ShouldChangePixels()
        {
            using var image = new MagickImage(Files.NoisePNG);
            image.Statistic(StatisticType.Minimum, 10, 1);

            ColorAssert.Equal(MagickColors.Black, image, 42, 119);
            ColorAssert.Equal(new MagickColor("#eeeeeeeeeeee"), image, 90, 120);
            ColorAssert.Equal(new MagickColor("#999999999999"), image, 90, 168);
        }
    }
}
