// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class TheMapCoder
{
    [Fact]
    public void CanBeReadFromFileWithMapExtensions()
    {
        using var image = new MagickImage(Files.Builtin.Logo);
        using var tempFile = new TemporaryFile("test.map");
        image.Write(tempFile.File, MagickFormat.Map);

        var settings = new MagickReadSettings
        {
            Width = image.Width,
            Height = image.Height,
            Depth = 8,
        };
        image.Read(tempFile.File, settings);

        Assert.Equal(MagickFormat.Map, image.Format);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, 1U)]
    [InlineData(1U, null)]
    public void ShouldThrowExceptionWhenDimensionsNotSpecified(uint? width, uint? height)
    {
        var settings = new MagickReadSettings
        {
            Width = width,
            Height = height,
        };

        using var tempFile = new TemporaryFile("test.map");
        using var image = new MagickImage();

        var exception = Assert.Throws<MagickOptionErrorException>(() => image.Read(tempFile.File, settings));
        Assert.Contains("must specify image size", exception.Message);
    }

    [Fact]
    public void ShouldThrowExceptionWhenDepthNotSpecified()
    {
        var settings = new MagickReadSettings
        {
            Width = 1,
            Height = 1,
        };

        using var tempFile = new TemporaryFile("test.map");
        using var image = new MagickImage();

        var exception = Assert.Throws<MagickOptionErrorException>(() => image.Read(tempFile.File, settings));
        Assert.Contains("must specify image depth", exception.Message);
    }

    [Fact]
    public void CannotBeReadFromFileWithoutMapExtensions()
    {
        using var image = new MagickImage(Files.Builtin.Logo);
        using var tempFile = new TemporaryFile("test");
        image.Write(tempFile.File, MagickFormat.Map);

        Assert.Throws<MagickMissingDelegateErrorException>(() => image.Read(tempFile.File, image.Width, image.Height));
    }
}
