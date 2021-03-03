// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StatisticsTests
    {
        public class TheChannelsProperty
        {
            [Fact]
            public void ShouldReturnTheCorrectChannels()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    var statistics = image.Statistics();

                    var channels = statistics.Channels.ToList();

                    Assert.Equal(5, channels.Count);
                    Assert.Contains(PixelChannel.Red, channels);
                    Assert.Contains(PixelChannel.Green, channels);
                    Assert.Contains(PixelChannel.Blue, channels);
                    Assert.Contains(PixelChannel.Alpha, channels);
                    Assert.Contains(PixelChannel.Composite, channels);
                }
            }
        }
    }
}
