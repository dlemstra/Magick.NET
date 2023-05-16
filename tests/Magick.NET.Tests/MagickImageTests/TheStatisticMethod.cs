// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheStatisticMethod
    {
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
