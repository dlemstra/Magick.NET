// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSetBitDepthMethod
        {
            [Fact]
            public void ShouldChangeTheBithDepth()
            {
                using (var image = new MagickImage(Files.RoseSparkleGIF))
                {
                    image.SetBitDepth(1);

                    Assert.Equal(1, image.BitDepth());
                }
            }

            [Fact]
            public void ShouldChangeTheBithDepthForTheSpecifiedChannel()
            {
                using (var image = new MagickImage(Files.RoseSparkleGIF))
                {
                    image.SetBitDepth(1, Channels.Red);

                    Assert.Equal(1, image.BitDepth(Channels.Red));
                    Assert.Equal(8, image.BitDepth(Channels.Green));
                    Assert.Equal(8, image.BitDepth(Channels.Blue));
                }
            }
        }
    }
}
