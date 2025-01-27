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
            OpenCLValue.Assert(13.33, 14.48, red.SumSquaredDistance(green));
#elif Q16
            OpenCLValue.Assert(23.02, 23.06, red.SumSquaredDistance(green));
#else
            OpenCLValue.Assert(29.99, 29.89, red.SumSquaredDistance(green));
#endif
        }
    }
}
