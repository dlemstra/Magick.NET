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

        Assert.Equal(2155, image.Width);
        Assert.Equal(2687, image.Height);

        using var first = new MagickImage(Files.Coders.GrimJP2 + "[0]");

        Assert.Equal(2155, first.Width);
        Assert.Equal(2687, first.Height);

        using var second = new MagickImage(Files.Coders.GrimJP2 + "[1]");

        Assert.Equal(256, second.Width);
        Assert.Equal(256, second.Height);
    }

    [Fact]
    public void ShouldCheckTheAlphaValue()
    {
        using var image = new MagickImage(Files.Coders.TestJP2);

        Assert.Equal(1, image.Width);
        Assert.Equal(1, image.Height);
        Assert.False(image.HasAlpha);
        Assert.Equal(4, image.ChannelCount);
        Assert.Contains(PixelChannel.Meta0, image.Channels);
    }
}
