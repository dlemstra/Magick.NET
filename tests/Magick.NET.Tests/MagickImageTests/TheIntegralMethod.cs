// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheIntegralMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectImage()
        {
            using var input = new MagickImage(Files.TestPNG);
            using var integral = input.Integral();
            Assert.NotNull(integral);

            var distortion = input.Compare(integral, ErrorMetric.MeanAbsolute);

#if Q16HDRI
            Assert.InRange(distortion, 724.0, 724.1);
#else
            Assert.InRange(distortion, 0.30, 0.31);
#endif
        }
    }
}
