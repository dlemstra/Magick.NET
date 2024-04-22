// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class StatisticsTests
{
    public class TheCompositeMethod
    {
        [Fact]
        public void ShouldReturnTheCompositeStatistics()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            var statistics = image.Statistics().Composite();

            Assert.NotNull(statistics);

            Assert.Equal(8, statistics.Depth);
            Assert.InRange(statistics.Entropy, 0.19, 0.20);
            Assert.InRange(statistics.Kurtosis, 0.044, 0.045);
            Assert.Equal(0, statistics.Minimum);
            Assert.InRange(statistics.Skewness, 1.37, 1.38);
#if Q8
            Assert.Equal(255, statistics.Maximum);
            Assert.InRange(statistics.Mean, 48.58, 48.59);
            Assert.InRange(statistics.StandardDeviation, 88.23, 88.24);
#else
            Assert.Equal(65535, statistics.Maximum);
            Assert.InRange(statistics.Mean, 12486.36, 12486.37);
            Assert.InRange(statistics.StandardDeviation, 22677.39, 22677.40);
#endif
        }
    }
}
