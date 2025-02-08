// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class ThePerceptualHashMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorSpacesIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            Assert.Throws<ArgumentNullException>("colorSpaces", () => image.PerceptualHash(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenNotEnoughColorSpacesAreProvided()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            Assert.Throws<ArgumentOutOfRangeException>("colorSpaces", () => image.PerceptualHash([]));
        }

        [Fact]
        public void ShouldThrowExceptionWhenTooMuchColorSpacesAreProvided()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            Assert.Throws<ArgumentOutOfRangeException>("colorSpaces", () => image.PerceptualHash(ColorSpace.CMY, ColorSpace.CMYK, ColorSpace.Gray, ColorSpace.HCL, ColorSpace.HCLp, ColorSpace.HSB, ColorSpace.HSI));
        }

        [Fact]
        public void ShouldThrowExceptionWhenDuplicateColorSpacesAreProvided()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            Assert.Throws<ArgumentException>("colorSpaces", () => image.PerceptualHash(ColorSpace.CMY, ColorSpace.CMY));
        }

        [Fact]
        public void ShouldReturnThePerceptualHash()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();
            Assert.NotNull(phash);

            var channel = phash.GetChannel(PixelChannel.Red);

#if Q8
            AssertChannel(channel, 0, 0.2609, 0.0993);
            AssertChannel(channel, 1, 3.2659, 0.6686);
            AssertChannel(channel, 2, 3.5854, 0.9196);
            AssertChannel(channel, 3, 3.6610, 2.3355);
            AssertChannel(channel, 4, 7.3918, 5.2277);
            AssertChannel(channel, 5, 6.1107, 2.8360);
            AssertChannel(channel, 6, 7.4885, 3.9636);
#elif Q16
            AssertChannel(channel, 0, 0.2600, 0.2570);
            AssertChannel(channel, 1, 3.2672, 1.0553);
            AssertChannel(channel, 2, 3.5845, 1.3801);
            AssertChannel(channel, 3, 3.6533, 2.8361);
            AssertChannel(channel, 4, 7.3710, 5.1087);
            AssertChannel(channel, 5, 6.0810, 3.4734);
            AssertChannel(channel, 6, 7.4909, 5.0815);
#else
            AssertChannel(channel, 0, 0.2600, 0.2946);
            AssertChannel(channel, 1, 3.2673, 1.1834);
            AssertChannel(channel, 2, 3.5845, 1.5010);
            AssertChannel(channel, 3, 3.6533, 3.0420);
            AssertChannel(channel, 4, 7.3708, 5.3652);
            AssertChannel(channel, 5, 6.0808, 3.6675);
            AssertChannel(channel, 6, 7.4910, 5.6501);
#endif

            channel = phash.GetChannel(PixelChannel.Green);

#if Q8
            AssertChannel(channel, 0, 0.2623, 0.0637);
            AssertChannel(channel, 1, 2.9572, 0.6036);
            AssertChannel(channel, 2, 3.2475, 0.9508);
            AssertChannel(channel, 3, 3.5946, 1.1199);
            AssertChannel(channel, 4, 7.3853, 2.5020);
            AssertChannel(channel, 5, 5.6994, 1.4574);
            AssertChannel(channel, 6, 7.0594, 2.2044);
#elif Q16
            AssertChannel(channel, 0, 0.2620, 0.0635);
            AssertChannel(channel, 1, 2.9595, 0.6031);
            AssertChannel(channel, 2, 3.2481, 0.9501);
            AssertChannel(channel, 3, 3.5937, 1.1202);
            AssertChannel(channel, 4, 7.3773, 2.5015);
            AssertChannel(channel, 5, 5.6952, 1.4575);
            AssertChannel(channel, 6, 7.0599, 2.2046);
#else
            AssertChannel(channel, 0, 0.2620, 0.0635);
            AssertChannel(channel, 1, 2.9595, 0.6031);
            AssertChannel(channel, 2, 3.2481, 0.9501);
            AssertChannel(channel, 3, 3.5937, 1.1202);
            AssertChannel(channel, 4, 7.3771, 2.5015);
            AssertChannel(channel, 5, 5.6950, 1.4575);
            AssertChannel(channel, 6, 7.0599, 2.2046);
#endif

            channel = phash.GetChannel(PixelChannel.Blue);

#if Q8
            AssertChannel(channel, 0, 0.6560, 0.7381);
            AssertChannel(channel, 1, 3.1035, 4.0989);
            AssertChannel(channel, 2, 3.7202, 4.9905);
            AssertChannel(channel, 3, 3.9979, 5.1225);
            AssertChannel(channel, 4, 7.8607, 10.1992);
            AssertChannel(channel, 5, 5.8133, 7.2678);
            AssertChannel(channel, 6, 8.7418, 10.7048);
#elif Q16
            AssertChannel(channel, 0, 0.6558, 0.7381);
            AssertChannel(channel, 1, 3.1021, 4.0982);
            AssertChannel(channel, 2, 3.7194, 4.9910);
            AssertChannel(channel, 3, 3.9968, 5.1224);
            AssertChannel(channel, 4, 7.8585, 10.1987);
            AssertChannel(channel, 5, 5.8103, 7.2667);
            AssertChannel(channel, 6, 8.7491, 10.7102);
#else
            AssertChannel(channel, 0, 0.6558, 0.7381);
            AssertChannel(channel, 1, 3.1021, 4.0982);
            AssertChannel(channel, 2, 3.7194, 4.9910);
            AssertChannel(channel, 3, 3.9968, 5.1224);
            AssertChannel(channel, 4, 7.8585, 10.1987);
            AssertChannel(channel, 5, 5.8103, 7.2667);
            AssertChannel(channel, 6, 8.7491, 10.7102);
#endif
        }

        private static void AssertChannel(IChannelPerceptualHash? channel, int channelIndex, double xyyHuPhash, double hsbHuPhash)
        {
            Assert.NotNull(channel);

            Assert.Equal(xyyHuPhash, channel.HuPhash(ColorSpace.XyY, channelIndex), 4);
            Assert.Equal(hsbHuPhash, channel.HuPhash(ColorSpace.HSB, channelIndex), 4);
        }
    }
}
