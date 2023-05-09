// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public class TheDngCoder
{
    [Fact]
    public void ShouldReadTheThumbnail()
    {
        var settings = new MagickReadSettings
        {
            Defines = new DngReadDefines
            {
                ReadThumbnail = true,
            },
        };

        using var image = new MagickImage();
        image.Ping(Files.Coders.RawKodakDC50KDC, settings);

        var profile = image.GetProfile("dng:thumbnail");
        Assert.NotNull(profile);

        var data = profile.GetData();
        Assert.NotNull(data);
        Assert.Equal(18432, data.Length);
    }

    [Fact]
    public void ShouldNotReadTheThumbnailByDefault()
    {
        using var image = new MagickImage();
        image.Ping(Files.Coders.RawKodakDC50KDC);

        var profile = image.GetProfile("dng:thumbnail");
        Assert.Null(profile);
    }
}
