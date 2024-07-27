// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class TheJp2Coder
{
    [Fact]
    public void ShouldReadTheImageWithCorrectDimensions()
    {
        using var image = new MagickImage(Files.Coders.GrimJP2);

        Assert.Equal(2155U, image.Width);
        Assert.Equal(2687U, image.Height);

        using var first = new MagickImage(Files.Coders.GrimJP2 + "[0]");

        Assert.Equal(2155U, first.Width);
        Assert.Equal(2687U, first.Height);

        using var second = new MagickImage(Files.Coders.GrimJP2 + "[1]");

        Assert.Equal(256U, second.Width);
        Assert.Equal(256U, second.Height);
    }

    [Fact]
    public void ShouldCheckTheAlphaValue()
    {
        using var image = new MagickImage(Files.Coders.TestJP2);

        Assert.Equal(1U, image.Width);
        Assert.Equal(1U, image.Height);
        Assert.False(image.HasAlpha);
        Assert.Equal(4U, image.ChannelCount);
        Assert.Contains(PixelChannel.Meta0, image.Channels);
    }
}
