// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class TheAvifCoder
{
    [Fact]
    public void ShouldEncodeAndDecodeAlphaChannel()
    {
        using var input = new MagickImage(Files.TestPNG);
        input.Resize(new Percentage(15));

        using var stream = new MemoryStream();
        input.Write(stream, MagickFormat.Avif);

        stream.Position = 0;

        using var output = new MagickImage(stream);
        Assert.True(output.HasAlpha);
        Assert.Equal(MagickFormat.Avif, output.Format);
        Assert.Equal(input.Width, output.Width);
        Assert.Equal(input.Height, output.Height);
    }

    [Fact]
    public void ShouldIgnoreEmptyExifProfile()
    {
        using var image = new MagickImage(Files.Coders.EmptyExifAVIF);
    }
}
