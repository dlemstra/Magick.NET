// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheDetermineBitDepthMethod
    {
        [Fact]
        public void ShouldCalculateTheBitDepth()
        {
            using (var image = new MagickImage(Files.RoseSparkleGIF))
            {
                Assert.Equal(8, image.DetermineBitDepth());

                image.Threshold((Percentage)50);
                Assert.Equal(1, image.DetermineBitDepth());
            }
        }

        [Fact]
        public void ShouldCalculateTheBitDepthForTheSpecifiedChannel()
        {
            using (var image = new MagickImage(Files.RoseSparkleGIF))
            {
                Assert.Equal(1, image.DetermineBitDepth(Channels.Alpha));
            }
        }
    }
}
