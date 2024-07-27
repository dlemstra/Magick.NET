// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public class TheDngCoder
{
    [Fact]
    public void ShouldNotReadTheThumbnailByDefault()
    {
        using var image = new MagickImage();
        image.Ping(Files.Coders.RawKodakDC50KDC);

        var profile = image.GetProfile("dng:thumbnail");
        Assert.Null(profile);
    }

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

        var data = profile.ToByteArray();
        Assert.NotNull(data);
        Assert.Equal(18432, data.Length);

        var type = image.GetAttribute("dng:thumbnail.type");
        var size = new MagickGeometry(image.GetAttribute("dng:thumbnail.geometry"));
        var bits = int.Parse(image.GetAttribute("dng:thumbnail.bits"));
        var colors = int.Parse(image.GetAttribute("dng:thumbnail.colors"));

        Assert.Equal("bitmap", type);
        Assert.Equal(768U, image.Width);
        Assert.Equal(512U, image.Height);
        Assert.Equal(8, bits);
        Assert.Equal(3, colors);

        using var thumbnail = new MagickImage();
        thumbnail.ReadPixels(data, new PixelReadSettings(size.Width, size.Height, StorageType.Char, "RGB"));

        Assert.Equal("b68a4fcd77c02b98b22707db20bdbdd55a310fc0b96e91d174c0d7f1139033bf", thumbnail.Signature);
    }

    [Fact]
    public void ShouldReadMetadatawhenPingingImage()
    {
        using var image = new MagickImage();
        image.Ping(Files.Coders.RawKodakDC50KDC);

        Assert.Equal("DC50", image.GetAttribute("dng:camera.model.name"));
    }
}
