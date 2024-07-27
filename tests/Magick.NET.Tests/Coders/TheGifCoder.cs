// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class TheGifCoder
{
    [Fact]
    public void ShouldReturnTheCorrectNumberOfAnimationIterations()
    {
        using var images = new MagickImageCollection
        {
            new MagickImage(MagickColors.Red, 1, 1),
            new MagickImage(MagickColors.Green, 1, 1),
        };

        images[0].AnimationIterations = 1;

        using var tempFile = new TemporaryFile("output.gif");
        images.Write(tempFile.File);

        images.Read(tempFile.File);
        Assert.Equal(1U, images[0].AnimationIterations);
    }
}
