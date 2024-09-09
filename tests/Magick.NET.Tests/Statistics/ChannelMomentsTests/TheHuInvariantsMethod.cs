// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ChannelMomentsTests
{
    public class TheHuInvariantsMethod
    {
        [Fact]
        public void ShouldThrowExceptionForInvalidIndex()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var moments = image.Moments();
            var redMoment = moments.GetChannel(PixelChannel.Red);

            Assert.NotNull(redMoment);
            Assert.Throws<ArgumentOutOfRangeException>(() => redMoment.HuInvariants(9));
        }
    }
}
