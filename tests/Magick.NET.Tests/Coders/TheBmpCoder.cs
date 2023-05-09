// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class TheBmpCoder
{
    [Fact]
    public void ShouldBeAbleToReadBmp3Format()
    {
        using var tempFile = new TemporaryFile(Files.MagickNETIconPNG);
        using var input = new MagickImage(tempFile.File);
        input.Write(tempFile.File, MagickFormat.Bmp3);

        var settings = new MagickReadSettings
        {
            Format = MagickFormat.Bmp3,
        };
        using var output = new MagickImage(tempFile.File, settings);

        Assert.Equal(MagickFormat.Bmp3, output.Format);
    }
}
