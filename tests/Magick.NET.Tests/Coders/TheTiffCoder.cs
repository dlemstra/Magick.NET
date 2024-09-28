// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TheTiffCoder
{
    private static readonly string _tag = "32934";

    [Fact]
    public void ShouldThrowExceptionWhenImageContainsInvalidTag()
    {
        using var image = new MagickImage();

        var exception = Assert.Throws<MagickCoderErrorException>(() => image.Read(Files.Coders.IgnoreTagTIF));

        Assert.Contains(@$"Null count for ""Tag {_tag}""", exception.Message);
    }

    [Fact]
    public void ShouldIgnoreTheSpecifiedTags()
    {
        using var image = new MagickImage();
        image.Settings.SetDefine(MagickFormat.Tiff, "ignore-tags", _tag);
        image.Read(Files.Coders.IgnoreTagTIF);

        var settings = new MagickReadSettings(new TiffReadDefines
        {
            IgnoreTags = new string[] { _tag },
        });

        image.Settings.RemoveDefine(MagickFormat.Tiff, "ignore-tags");
        image.Read(Files.Coders.IgnoreTagTIF, settings);
    }

    [Fact]
    public void ShouldBeAbleToReadAndWriteIptcValues()
    {
        using var input = new MagickImage(Files.MagickNETIconPNG);
        var profile = input.GetIptcProfile();

        Assert.Null(profile);

        profile = new IptcProfile();
        profile.SetValue(IptcTag.Headline, "Magick.NET");
        profile.SetValue(IptcTag.CopyrightNotice, "Copyright.NET");

        input.SetProfile(profile);

        using var memStream = new MemoryStream();
        input.Format = MagickFormat.Tiff;
        input.Write(memStream);
        memStream.Position = 0;

        using var output = new MagickImage(memStream);
        profile = output.GetIptcProfile();

        Assert.NotNull(profile);
        TestValue(profile, IptcTag.Headline, "Magick.NET");
        TestValue(profile, IptcTag.CopyrightNotice, "Copyright.NET");
    }

    [Fact]
    public void ShouldBeAbleToWriteLzwPTiffToStream()
    {
        using var image = new MagickImage(Files.InvitationTIF);
        image.Settings.Compression = CompressionMethod.LZW;

        using var stream = new MemoryStream();
        image.Write(stream, MagickFormat.Ptif);
    }

    [Theory]
    [InlineData(CompressionMethod.Fax)]
    [InlineData(CompressionMethod.Group4)]
    [InlineData(CompressionMethod.JPEG)]
    public void ShouldBeAbleToUseTheSpecifiedCompression(CompressionMethod compression)
    {
        using var input = new MagickImage(Files.Builtin.Logo);
        input.Settings.Compression = compression;

        var bytes = input.ToByteArray(MagickFormat.Tiff);

        using var output = new MagickImage(bytes);

        Assert.Equal(compression, output.Compression);
    }

    [Fact]
    public void ShouldBeAbleToReadImageWithInfiniteRowsPerStrip()
    {
        using var image = new MagickImage(Files.Coders.RowsPerStripTIF);

        Assert.Equal(MagickFormat.Tiff, image.Format);
    }

    [Fact]
    public void ShouldBeAbleToReadImageWithLargeScanLine()
    {
        using var image = new MagickImage(MagickColors.Green, 1000, 1);

        image.Settings.Compression = CompressionMethod.Zip;

        var data = image.ToByteArray(MagickFormat.Tiff);
        Assert.NotNull(data);

        image.Read(data);
    }

    [Fact]
    public void ShouldReadImageWithAlphaCorrectly()
    {
        using var image = new MagickImage(Files.MagickNETIconPNG);
        using var stream = new MemoryStream();
        image.Write(stream, MagickFormat.Tiff);
        stream.Position = 0;

        image.Read(stream);

        ColorAssert.Equal(new MagickColor("#a8dff8ff"), image, 55, 70);
    }

    [Fact]
    public void ShouldWriteTiffImageInCorrectColor()
    {
        using var input = new MagickImage(Files.Coders.PixelTIF);
        using var memorystream = new MemoryStream();
        input.Write(memorystream, MagickFormat.Tiff);
        memorystream.Position = 0;

        using var output = new MagickImage(memorystream);

        ColorAssert.Equal(MagickColors.White, output, 0, 0);
    }

    private static void TestValue(IIptcProfile profile, IptcTag tag, string expectedValue)
    {
        var value = profile.GetValue(tag);
        Assert.NotNull(value);
        Assert.Equal(expectedValue, value.Value);
    }
}
