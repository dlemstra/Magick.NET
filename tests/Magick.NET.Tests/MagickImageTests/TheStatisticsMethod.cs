// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheStatisticsMethod
    {
        [Fact]
        public void ShouldReturnTheImageStatistics()
        {
            using var image = new MagickImage(Files.SnakewarePNG);
            var statistics = image.Statistics();

            Assert.NotNull(statistics);

            var red = statistics.GetChannel(PixelChannel.Red);

            Assert.NotNull(red);

            Assert.Equal(8U, red.Depth);
            Assert.InRange(red.Entropy, 0.98, 0.99);
            Assert.InRange(red.Kurtosis, -1.90, -1.89);
            Assert.Equal(0, red.Minimum);
            Assert.InRange(red.Skewness, 0.32, 0.33);
#if Q8
            Assert.Equal(2, red.Maximum);
            Assert.InRange(red.Mean, 0.83, 0.84);
            Assert.InRange(red.StandardDeviation, 0.98, 0.99);
#else
            Assert.Equal(514, red.Maximum);
            Assert.InRange(red.Mean, 215.79, 215.80);
            Assert.InRange(red.StandardDeviation, 253.68, 253.69);
#endif
        }

        [Fact]
        public void ShouldOnlyUseTheSpecifiedChannels()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            var statistics = image.Statistics(Channels.Red);

            Assert.NotNull(statistics);
            Assert.Equal(new[] { PixelChannel.Red, PixelChannel.Composite }, statistics.Channels);

            var red = statistics.GetChannel(PixelChannel.Red);

            Assert.NotNull(red);

            var green = statistics.GetChannel(PixelChannel.Green);

            Assert.Null(green);
        }

        [Fact]
        public void ShouldAlwaysReturnTheCompositeChannel()
        {
            using var image = new MagickImage(Files.SnakewarePNG);
            var statistics = image.Statistics(Channels.Undefined);

            Assert.NotNull(statistics);

            Assert.Single(statistics.Channels);
            Assert.Equal(PixelChannel.Composite, statistics.Channels.First());

            var composite = statistics.Composite();

            Assert.NotNull(composite);
        }
    }
}
