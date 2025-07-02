// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ChannelPerceptualHashTests
{
    public class TheSumSquaredDistanceMethod
    {
        [Fact]
        public void ShouldReturnTheDifference()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();
            Assert.NotNull(phash);

            var red = phash.GetChannel(PixelChannel.Red);
            Assert.NotNull(red);

#if Q8
            Assert.Equal("a65e687f9488c1088f0262ee062ee062ee0a26ce81a1e823ec85b3b62ee062ee089ad4", red.ToString());
#elif Q16
            Assert.Equal("a658b87fa188c0688eb562ee062ee062ee0a646682939835e986ec962ee062ee062ee0", red.ToString());
#else
            Assert.Equal("a658a87fa188c0688eb562ee062ee062ee0a731182e3a83aa2876d462ee062ee062ee0", red.ToString());
#endif
        }
    }
}
