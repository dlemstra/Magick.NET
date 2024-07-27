// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TheVideoCoder
{
    [Fact]
    public void ShouldUseWebPAsTheDefaultIntermediateFormat()
    {
        using var images = new MagickImageCollection(Files.Coders.TestMP4);

        Assert.Equal(2, images.Count);

        Assert.Equal(20U, images[0].AnimationDelay);
        Assert.Equal(2U, images[0].Width);
        Assert.Equal(2U, images[0].Height);
        ColorAssert.Equal(MagickColors.Black, images[0], 0, 0);

        Assert.Equal(12U, images[1].AnimationDelay);
        Assert.Equal(2U, images[1].Width);
        Assert.Equal(2U, images[1].Height);
        ColorAssert.Equal(MagickColors.White, images[1], 0, 0);
    }

    [Fact]
    public void ShouldUsePamAsTheIntermediateFormatWhenReadModeIsByFrame()
    {
        var videoDefines = new VideoReadDefines(MagickFormat.Mp4)
        {
            ReadMode = VideoReadMode.ByFrame,
        };

        var settings = new MagickReadSettings();
        settings.SetDefines(videoDefines);
        using var images = new MagickImageCollection(Files.Coders.TestMP4, settings);

        Assert.Equal(8, images.Count);
    }
}
