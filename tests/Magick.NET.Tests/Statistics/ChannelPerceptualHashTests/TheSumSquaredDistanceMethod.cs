// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ChannelPerceptualHashTests
{
    public class TheToStringMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectValue()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();
            Assert.NotNull(phash);

            var red = phash.GetChannel(PixelChannel.Red);
            var green = phash.GetChannel(PixelChannel.Green);
            Assert.NotNull(red);
            Assert.NotNull(green);

#if Q8
            Assert.Equal(14.48, red.SumSquaredDistance(green), 2);
#elif Q16
            Assert.Equal(23.06, red.SumSquaredDistance(green), 2);
#else
            Assert.Equal(29.89, red.SumSquaredDistance(green), 2);
#endif
        }
    }
}
