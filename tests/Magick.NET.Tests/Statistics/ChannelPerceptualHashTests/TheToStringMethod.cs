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
            //using var image = new MagickImage(Files.ImageMagickJPG);
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var phash = image.PerceptualHash();
            Assert.NotNull(phash);

            var red = phash.GetChannel(PixelChannel.Red);
            Assert.NotNull(red);

#if Q8
            Assert.Equal("a65e687f9388c0e88f0261ce08eeb361d40a26ce81a1e823ec85b3b8cc3586ec889ad4", red.ToString());
#elif Q16
            Assert.Equal("a658c87fa088c0588eb561ccb8ed8a61d43a646682939835e986ec98c78f887ae8c67f", red.ToString());
#else
            Assert.Equal("a658c87fa188c0588eb561ccb8ed8861d43a731182e3a83aa2876d48d19488f438dcb5", red.ToString());
#endif
        }
    }
}
