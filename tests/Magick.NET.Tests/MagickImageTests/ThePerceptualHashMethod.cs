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
            TestChannel(channel, 0, 0.6980, 0.6980, 0.0974, 0.0993);
            TestChannel(channel, 1, 3.4388, 3.4390, 0.6582, 0.6685);
            TestChannel(channel, 2, 3.9123, 3.9123, 0.9238, 0.9195);
            TestChannel(channel, 3, 4.2922, 4.2920, 2.3627, 2.3354);
            TestChannel(channel, 4, 8.7570, 8.7574, 4.9574, 5.2273);
            TestChannel(channel, 5, 8.2422, 8.2149, 2.8422, 2.8360);
            TestChannel(channel, 6, 8.4397, 8.4394, 4.0087, 3.9636);
#elif Q16
            TestChannel(channel, 0, 0.6979, 0.6979, 0.2575, 0.2570);
            TestChannel(channel, 1, 3.4385, 3.4385, 1.0621, 1.0552);
            TestChannel(channel, 2, 3.9123, 3.9123, 1.3756, 1.3800);
            TestChannel(channel, 3, 4.2920, 4.2920, 2.8341, 2.8360);
            TestChannel(channel, 4, 8.7557, 8.7557, 5.1134, 5.1087);
            TestChannel(channel, 5, 8.3019, 8.3018, 3.4791, 3.4733);
            TestChannel(channel, 6, 8.4398, 8.4398, 5.0679, 5.0815);
#else
            TestChannel(channel, 0, 0.6979, 0.6979, 0.2944, 0.2945);
            TestChannel(channel, 1, 3.4385, 3.4385, 1.1850, 1.1834);
            TestChannel(channel, 2, 3.9123, 3.9123, 1.5006, 1.5009);
            TestChannel(channel, 3, 4.2920, 4.2920, 3.0480, 3.0419);
            TestChannel(channel, 4, 8.7557, 8.7557, 5.3844, 5.3651);
            TestChannel(channel, 5, 8.3018, 8.3018, 3.6804, 3.6675);
            TestChannel(channel, 6, 8.4398, 8.4398, 5.6247, 5.6501);
#endif

            channel = phash.GetChannel(PixelChannel.Green);

#if Q8
            TestChannel(channel, 0, 0.6942, 0.6942, -0.0601, -0.0601);
            TestChannel(channel, 1, 3.3993, 3.3995, 0.3090, 0.3093);
            TestChannel(channel, 2, 4.1171, 4.1172, 0.6084, 0.6083);
            TestChannel(channel, 3, 4.4847, 4.4847, 0.7559, 0.7566);
            TestChannel(channel, 4, 8.8180, 8.8183, 1.7224, 1.7237);
            TestChannel(channel, 5, 6.4829, 6.4832, 0.9413, 0.9421);
            TestChannel(channel, 6, 9.2143, 9.2141, 1.5065, 1.5074);
#elif Q16
            TestChannel(channel, 0, 0.6942, 0.6942, -0.0601, -0.0601);
            TestChannel(channel, 1, 3.3989, 3.3989, 0.3092, 0.3092);
            TestChannel(channel, 2, 4.1169, 4.1169, 0.6084, 0.6084);
            TestChannel(channel, 3, 4.4844, 4.4844, 0.7559, 0.7559);
            TestChannel(channel, 4, 8.8174, 8.8174, 1.7230, 1.7230);
            TestChannel(channel, 5, 6.4821, 6.4821, 0.9413, 0.9413);
            TestChannel(channel, 6, 9.2148, 9.2148, 1.5063, 1.5063);
#else
            TestChannel(channel, 0, 0.6942, 0.6942, -0.0601, -0.0601);
            TestChannel(channel, 1, 3.3989, 3.3989, 0.3092, 0.3092);
            TestChannel(channel, 2, 4.1169, 4.1169, 0.6084, 0.6084);
            TestChannel(channel, 3, 4.4844, 4.4844, 0.7559, 0.7559);
            TestChannel(channel, 4, 8.8174, 8.8174, 1.7230, 1.7230);
            TestChannel(channel, 5, 6.4821, 6.4821, 0.9413, 0.9413);
            TestChannel(channel, 6, 9.2148, 9.2148, 1.5063, 1.5063);
#endif

            channel = phash.GetChannel(PixelChannel.Blue);

#if Q8
            TestChannel(channel, 0, 0.7223, 0.7223, 0.6984, 0.6984);
            TestChannel(channel, 1, 3.8298, 3.8298, 3.4611, 3.4612);
            TestChannel(channel, 2, 5.1301, 5.1301, 4.1312, 4.1312);
            TestChannel(channel, 3, 5.0217, 5.0218, 4.4867, 4.4867);
            TestChannel(channel, 4, 10.4769, 10.4761, 8.8669, 8.8670);
            TestChannel(channel, 5, 6.9453, 6.9454, 6.6108, 6.6110);
            TestChannel(channel, 6, 10.1394, 10.1396, 9.0725, 9.0722);
#elif Q16
            TestChannel(channel, 0, 0.7222, 0.7222, 0.6984, 0.6984);
            TestChannel(channel, 1, 3.8295, 3.8295, 3.4609, 3.4609);
            TestChannel(channel, 2, 5.1304, 5.1304, 4.1314, 4.1314);
            TestChannel(channel, 3, 5.0214, 5.0214, 4.4866, 4.4866);
            TestChannel(channel, 4, 10.4771, 10.4772, 8.8663, 8.8663);
            TestChannel(channel, 5, 6.9448, 6.9448, 6.6094, 6.6094);
            TestChannel(channel, 6, 10.1388, 10.1388, 9.0737, 9.0737);
#else
            TestChannel(channel, 0, 0.7222, 0.7222, 0.6984, 0.6984);
            TestChannel(channel, 1, 3.8295, 3.8295, 3.4609, 3.4609);
            TestChannel(channel, 2, 5.1304, 5.1304, 4.1314, 4.1314);
            TestChannel(channel, 3, 5.0214, 5.0214, 4.4866, 4.4866);
            TestChannel(channel, 4, 10.4772, 10.4772, 8.8663, 8.8663);
            TestChannel(channel, 5, 6.9448, 6.9448, 6.6094, 6.6094);
            TestChannel(channel, 6, 10.1388, 10.1388, 9.0737, 9.0737);
#endif
        }

        private void TestChannel(IChannelPerceptualHash channel, int index, double srgbHuPhashWithOpenCL, double srgbHuPhashWithoutOpenCL, double hclpHuPhashWithOpenCL, double hclpHuPhashWithoutOpenCL)
        {
            OpenCLValue.Assert(srgbHuPhashWithOpenCL, srgbHuPhashWithoutOpenCL, channel.HuPhash(ColorSpace.sRGB, index), 0.0001);
            OpenCLValue.Assert(hclpHuPhashWithOpenCL, hclpHuPhashWithoutOpenCL, channel.HuPhash(ColorSpace.HCLp, index), 0.0001);
        }
    }
}
