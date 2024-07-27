// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSetBitDepthMethod
    {
        [Fact]
        public void ShouldChangeTheBitDepth()
        {
            using var image = new MagickImage(Files.RoseSparkleGIF);
            image.SetBitDepth(1);

            Assert.Equal(1U, image.DetermineBitDepth());
        }

        [Fact]
        public void ShouldChangeTheBitDepthForTheSpecifiedChannel()
        {
            using var image = new MagickImage(Files.RoseSparkleGIF);
            image.SetBitDepth(1, Channels.Red);

            Assert.Equal(1U, image.DetermineBitDepth(Channels.Red));
            Assert.Equal(8U, image.DetermineBitDepth(Channels.Green));
            Assert.Equal(8U, image.DetermineBitDepth(Channels.Blue));
        }
    }
}
