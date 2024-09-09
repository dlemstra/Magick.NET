// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ChannelPerceptualHashTests
{
    public class TheHclpHuPhashMethod
    {
        [Fact]
        public void ShouldThrowExceptionForInvalidIndex()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();

            Assert.NotNull(phash);

            var channel = phash.GetChannel(PixelChannel.Red);

            Assert.NotNull(channel);
            Assert.Throws<ArgumentOutOfRangeException>(() => channel.HuPhash(ColorSpace.HCLp, 7));
        }
    }
}
