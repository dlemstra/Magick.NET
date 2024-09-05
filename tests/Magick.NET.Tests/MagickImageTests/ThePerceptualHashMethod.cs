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
            Assert.Throws<ArgumentNullException>("colorSpaces", () => image.PerceptualHash(null));
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
        public void ShouldRemoveDuplicateColorSpaces()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            Assert.Throws<ArgumentException>("colorSpaces", () => image.PerceptualHash(ColorSpace.CMY, ColorSpace.CMY));
        }

        [Fact]
        public void ShouldReturnThePerceptualHash()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();
            var channel = phash.GetChannel(PixelChannel.Red);

#if Q8
            TestChannel(channel, 0, 0.2609, 0.2609, 0.0975, 0.0993);
            TestChannel(channel, 1, 3.2660, 3.2659, 0.6583, 0.6686);
            TestChannel(channel, 2, 3.5858, 3.5854, 0.9238, 0.9196);
            TestChannel(channel, 3, 3.6612, 3.6610, 2.3627, 2.3355);
            TestChannel(channel, 4, 7.3919, 7.3918, 4.9577, 5.2277);
            TestChannel(channel, 5, 6.1101, 6.1107, 2.8422, 2.8360);
            TestChannel(channel, 6, 7.4893, 7.4885, 4.0087, 3.9636);
#elif Q16
            TestChannel(channel, 0, 0.2600, 0.2600, 0.2576, 0.2570);
            TestChannel(channel, 1, 3.2672, 3.2672, 1.0621, 1.0553);
            TestChannel(channel, 2, 3.5845, 3.5845, 1.3757, 1.3801);
            TestChannel(channel, 3, 3.6533, 3.6533, 2.8341, 2.8361);
            TestChannel(channel, 4, 7.3710, 7.3710, 5.1136, 5.1087);
            TestChannel(channel, 5, 6.0810, 6.0810, 3.4792, 3.4734);
            TestChannel(channel, 6, 7.4909, 7.4909, 5.0677, 5.0815);
#else
            TestChannel(channel, 0, 0.2600, 0.2600, 0.2948, 0.2946);
            TestChannel(channel, 1, 3.2673, 3.2673, 1.1852, 1.1834);
            TestChannel(channel, 2, 3.5845, 3.5845, 1.5017, 1.5010);
            TestChannel(channel, 3, 3.6533, 3.6533, 3.0470, 3.0420);
            TestChannel(channel, 4, 7.3708, 7.3708, 5.3762, 5.3652);
            TestChannel(channel, 5, 6.0808, 6.0808, 3.6754, 3.6675);
            TestChannel(channel, 6, 7.4910, 7.4910, 5.6470, 5.6501);
#endif

            channel = phash.GetChannel(PixelChannel.Green);

#if Q8
            TestChannel(channel, 0, 0.2623, 0.2623, 0.0637, 0.0637);
            TestChannel(channel, 1, 2.9570, 2.9572, 0.6035, 0.6036);
            TestChannel(channel, 2, 3.2478, 3.2475, 0.9510, 0.9508);
            TestChannel(channel, 3, 3.5946, 3.5946, 1.1194, 1.1199);
            TestChannel(channel, 4, 7.3861, 7.3853, 2.5013, 2.5020);
            TestChannel(channel, 5, 5.7001, 5.6994, 1.4568, 1.4574);
            TestChannel(channel, 6, 7.0594, 7.0594, 2.2038, 2.2044);
#elif Q16
            TestChannel(channel, 0, 0.2620, 0.2620, 0.0635, 0.0635);
            TestChannel(channel, 1, 2.9595, 2.9595, 0.6031, 0.6031);
            TestChannel(channel, 2, 3.2481, 3.2481, 0.9501, 0.9501);
            TestChannel(channel, 3, 3.5937, 3.5937, 1.1202, 1.1202);
            TestChannel(channel, 4, 7.3773, 7.3773, 2.5015, 2.5015);
            TestChannel(channel, 5, 5.6952, 5.6952, 1.4575, 1.4575);
            TestChannel(channel, 6, 7.0599, 7.0599, 2.2046, 2.2046);
#else
            TestChannel(channel, 0, 0.2620, 0.2620, 0.0635, 0.0635);
            TestChannel(channel, 1, 2.9595, 2.9595, 0.6031, 0.6031);
            TestChannel(channel, 2, 3.2481, 3.2481, 0.9501, 0.9501);
            TestChannel(channel, 3, 3.5937, 3.5937, 1.1202, 1.1202);
            TestChannel(channel, 4, 7.3771, 7.3771, 2.5015, 2.5015);
            TestChannel(channel, 5, 5.6950, 5.6950, 1.4575, 1.4575);
            TestChannel(channel, 6, 7.0599, 7.0599, 2.2046, 2.2046);
#endif

            channel = phash.GetChannel(PixelChannel.Blue);

#if Q8
            TestChannel(channel, 0, 0.6559, 0.6560, 0.7381, 0.7381);
            TestChannel(channel, 1, 3.1032, 3.1035, 4.0987, 4.0989);
            TestChannel(channel, 2, 3.7201, 3.7202, 4.9907, 4.9905);
            TestChannel(channel, 3, 3.9978, 3.9979, 5.1225, 5.1225);
            TestChannel(channel, 4, 7.8604, 7.8607, 10.1991, 10.1992);
            TestChannel(channel, 5, 5.8126, 5.8133, 7.2674, 7.2678);
            TestChannel(channel, 6, 8.7449, 8.7418, 10.7076, 10.7048);
#elif Q16
            TestChannel(channel, 0, 0.6558, 0.6558, 0.7381, 0.7381);
            TestChannel(channel, 1, 3.1021, 3.1021, 4.0982, 4.0982);
            TestChannel(channel, 2, 3.7194, 3.7194, 4.9910, 4.9910);
            TestChannel(channel, 3, 3.9968, 3.9968, 5.1224, 5.1224);
            TestChannel(channel, 4, 7.8585, 7.8585, 10.1987, 10.1987);
            TestChannel(channel, 5, 5.8103, 5.8103, 7.2667, 7.2667);
            TestChannel(channel, 6, 8.7491, 8.7491, 10.7102, 10.7102);
#else
            TestChannel(channel, 0, 0.6558, 0.6558, 0.7381, 0.7381);
            TestChannel(channel, 1, 3.1021, 3.1021, 4.0982, 4.0982);
            TestChannel(channel, 2, 3.7194, 3.7194, 4.9910, 4.9910);
            TestChannel(channel, 3, 3.9968, 3.9968, 5.1224, 5.1224);
            TestChannel(channel, 4, 7.8585, 7.8585, 10.1987, 10.1987);
            TestChannel(channel, 5, 5.8103, 5.8103, 7.2667, 7.2667);
            TestChannel(channel, 6, 8.7491, 8.7491, 10.7102, 10.7102);
#endif
        }

        private void TestChannel(IChannelPerceptualHash channel, int index, double xyyHuPhashWithOpenCL, double xyyHuPhashWithoutOpenCL, double hsbHuPhashWithOpenCL, double hsbHuPhashWithoutOpenCL)
        {
            OpenCLValue.Assert(xyyHuPhashWithOpenCL, xyyHuPhashWithoutOpenCL, channel.HuPhash(ColorSpace.XyY, index));
            OpenCLValue.Assert(hsbHuPhashWithOpenCL, hsbHuPhashWithoutOpenCL, channel.HuPhash(ColorSpace.HSB, index));
        }
    }
}
