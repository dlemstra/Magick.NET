// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MomentsTests
{
    public class TheGetChannelMethod
    {
        [Fact]
        public void ShouldReturnNullWhenChannelDoesNotExist()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var moments = image.Moments();

            Assert.NotNull(moments.GetChannel(PixelChannel.Red));
            Assert.NotNull(moments.GetChannel(PixelChannel.Green));
            Assert.NotNull(moments.GetChannel(PixelChannel.Blue));

            Assert.Null(moments.GetChannel(PixelChannel.Black));
        }
    }
}
