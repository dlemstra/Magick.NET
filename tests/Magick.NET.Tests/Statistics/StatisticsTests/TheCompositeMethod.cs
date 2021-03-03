// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StatisticsTests
    {
        public class TheCompositeMethod
        {
            [Fact]
            public void ShouldReturnTheCompositeStatistics()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    var statistics = image.Statistics().Composite();

                    Assert.NotNull(statistics);

                    Assert.Equal(8, statistics.Depth);
                    Assert.InRange(statistics.Entropy, 0.19, 0.20);
                    Assert.InRange(statistics.Kurtosis, 0.34, 0.35);
                    Assert.Equal(0, statistics.Minimum);
                    Assert.InRange(statistics.Skewness, 1.46, 1.47);
#if Q8
                    Assert.Equal(255, statistics.Maximum);
                    Assert.InRange(statistics.Mean, 48.58, 48.59);
                    Assert.InRange(statistics.StandardDeviation, 88.23, 88.24);
                    Assert.InRange(statistics.Sum, 48.58, 48.59);
                    Assert.InRange(statistics.SumCubed, 2381458.27, 2381458.28);
                    Assert.InRange(statistics.SumFourthPower, 553538281.28, 553538281.29);
                    Assert.InRange(statistics.SumSquared, 10519.51, 10519.52);
#else
                    Assert.Equal(65535, statistics.Maximum);
                    Assert.InRange(statistics.Mean, 12486.36, 12486.37);
                    Assert.InRange(statistics.StandardDeviation, 22677.39, 22677.40);
                    Assert.InRange(statistics.Sum, 12486.36, 12486.37);
                    Assert.InRange(statistics.SumCubed, 40424285002062, 40424285002063);
                    Assert.InRange(statistics.SumFourthPower, 2.41479436791132E+18, 2.41479436791133E+18);
                    Assert.InRange(statistics.SumSquared, 694803484.49, 694803484.50);
#endif
                }
            }
        }
    }
}
