// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class TheJpegXlCoder
{
    [Fact]
    public void ShouldWriteCorrectOutputImage()
    {
        using var image = new MagickImage(Files.Builtin.Logo);
        using var memoryStream = new MemoryStream();
        image.Write(memoryStream, MagickFormat.Jxl);

        Assert.InRange(memoryStream.Length, 20000, 40000);
    }

    [Fact]
    public void ShouldSupportReadingAndWritingImageProfiles()
    {
        using var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
        input.Scale(1, 1);

        var inputXmpProfile = input.GetXmpProfile();
        Assert.NotNull(inputXmpProfile);

        var inputExifProfile = input.GetExifProfile();
        Assert.NotNull(inputExifProfile);

        using var memStream = new MemoryStream();
        input.Write(memStream, MagickFormat.Jxl);
        memStream.Position = 0;

        using var output = new MagickImage(memStream);
        var outputXmpProfile = input.GetXmpProfile();

        Assert.NotNull(outputXmpProfile);
        Assert.Equal(inputXmpProfile.ToByteArray(), outputXmpProfile.ToByteArray());

        var outputExifProfile = input.GetExifProfile();

        Assert.NotNull(outputExifProfile);
        Assert.Equal(inputExifProfile.ToByteArray(), outputExifProfile.ToByteArray());
    }
}
