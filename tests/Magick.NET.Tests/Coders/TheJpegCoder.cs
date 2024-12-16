// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class TheJpegCoder
{
    [Fact]
    public void ShouldDecodeCorrectly()
    {
        using var image = new MagickImage(Files.WhiteJPG);
        using var pixels = image.GetPixels();
        var color = pixels.GetPixel(0, 0).ToColor();

        Assert.NotNull(color);
        Assert.Equal(Quantum.Max, color.R);
        Assert.Equal(Quantum.Max, color.G);
        Assert.Equal(Quantum.Max, color.B);
        Assert.Equal(Quantum.Max, color.A);
    }

    [Fact]
    public void ShouldReadImageProfile()
    {
        using var image = new MagickImage(Files.CMYKJPG);
        image.SetProfile(ColorProfile.USWebCoatedSWOP);

        using var memoryStream = new MemoryStream();
        image.Write(memoryStream);
        memoryStream.Position = 0;

        image.Read(memoryStream);
        var profile = image.GetColorProfile();

        Assert.NotNull(profile);
    }

    [Fact]
    public void ShouldWriteTheXmpProfileToTheImage()
    {
        using var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
        var profile = input.GetXmpProfile();

        Assert.NotNull(profile);

        using var memoryStream = new MemoryStream();
        input.Write(memoryStream, MagickFormat.Jpeg);
        memoryStream.Position = 0;

        using var output = new MagickImage(memoryStream);
        var result = output.GetXmpProfile();

        Assert.NotNull(result);
        Assert.True(result.Equals(profile));
    }

    [Fact]
    public void ShouldSupportWritingExifProfileWithMaxLength()
    {
        using var input = new MagickImage(Files.FujiFilmFinePixS1ProJPG);

        var profile = input.GetExifProfile();
        Assert.NotNull(profile);

        var data = profile.ToByteArray();
        Assert.NotNull(data);

        Array.Resize(ref data, 65533);
        input.SetProfile(new ImageProfile("exif", data));

        data = input.ToByteArray(MagickFormat.Jpeg);

        using var output = new MagickImage(data);

        profile = output.GetExifProfile();
        Assert.NotNull(profile);
    }
}
