// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheBitDepthMethod
        {
            [Fact]
            public void ShouldCalculateTheBitDepth()
            {
                using (var image = new MagickImage(Files.RoseSparkleGIF))
                {
                    Assert.Equal(8, image.BitDepth());

                    image.Threshold((Percentage)50);
                    Assert.Equal(1, image.BitDepth());
                }
            }

            [Fact]
            public void ShouldCalculateTheBitDepthForTheSpecifiedChannel()
            {
                using (var image = new MagickImage(Files.RoseSparkleGIF))
                {
                    Assert.Equal(1, image.BitDepth(Channels.Alpha));
                }
            }

            [Fact]
            public void ShouldChangeTheBithDepth()
            {
                using (var image = new MagickImage(Files.RoseSparkleGIF))
                {
                    image.BitDepth(1);

                    Assert.Equal(1, image.BitDepth());
                }
            }

            [Fact]
            public void ShouldChangeTheBithDepthForTheSpecifiedChannel()
            {
                using (var image = new MagickImage(Files.RoseSparkleGIF))
                {
                    image.BitDepth(Channels.Red, 1);

                    Assert.Equal(1, image.BitDepth(Channels.Red));
                    Assert.Equal(8, image.BitDepth(Channels.Green));
                    Assert.Equal(8, image.BitDepth(Channels.Blue));
                }
            }
        }
    }
}
