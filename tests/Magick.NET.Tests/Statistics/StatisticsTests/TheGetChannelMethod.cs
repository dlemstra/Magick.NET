// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StatisticsTests
    {
        public class TheGetChannelMethod
        {
            [Fact]
            public void ShouldReturnNullWhenImageDoesNotContainThatChannel()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var statistics = image.Statistics();

                    Assert.Null(statistics.GetChannel(PixelChannel.Green));
                    Assert.Null(statistics.GetChannel(PixelChannel.Blue));
                    Assert.Null(statistics.GetChannel(PixelChannel.Black));
                }
            }
        }
    }
}
