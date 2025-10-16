// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
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

        ExceptionAssert.Contains($"Defined set_get_field_type of custom tag {_tag} (Tag {_tag}) is TIFF_SETGET_UNDEFINED", exception);
    }

    [Fact]
    public void ShouldIgnoreTheSpecifiedTags()
    {
        using var image = new MagickImage();
        image.Settings.SetDefine(MagickFormat.Tiff, "ignore-tags", _tag);
        image.Read(Files.Coders.IgnoreTagTIF);

        var settings = new MagickReadSettings(new TiffReadDefines
        {
            IgnoreTags = [_tag],
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

    [Fact]
    public void ShouldReadAndWriteMetaChannelsCorrectlyForRgbaImage()
    {
        using var images = new MagickImageCollection
        {
            new MagickImage(MagickColors.Red, 1, 1),
            new MagickImage(MagickColors.Green, 1, 1),
            new MagickImage(MagickColors.Blue, 1, 1),
            new MagickImage(MagickColors.Aqua, 1, 1),
            new MagickImage(MagickColors.Magenta, 1, 1),
        };

        using var input = images.Combine();
        using var memorystream = new MemoryStream();
        input.Write(memorystream, MagickFormat.Tiff);
        memorystream.Position = 0;

        using var output = new MagickImage(memorystream);

        var channels = output.Channels.ToList();
        Assert.Equal(5, channels.Count);
        Assert.Equal(PixelChannel.Red, channels[0]);
        Assert.Equal(PixelChannel.Green, channels[1]);
        Assert.Equal(PixelChannel.Blue, channels[2]);
        Assert.Equal(PixelChannel.Alpha, channels[3]);
        Assert.Equal(PixelChannel.Meta0, channels[4]);
    }

    [Fact]
    public void ShouldReadAndWriteMetaChannelsCorrectlyForChymkaImage()
    {
        using var images = new MagickImageCollection
        {
            new MagickImage(MagickColors.Cyan, 1, 1),
            new MagickImage(MagickColors.Magenta, 1, 1),
            new MagickImage(MagickColors.Yellow, 1, 1),
            new MagickImage(MagickColors.Khaki, 1, 1),
            new MagickImage(MagickColors.Aqua, 1, 1),
            new MagickImage(MagickColors.Magenta, 1, 1),
        };

        using var input = images.Combine(ColorSpace.CMYK);
        using var memorystream = new MemoryStream();
        input.Write(memorystream, MagickFormat.Tiff);
        memorystream.Position = 0;

        using var output = new MagickImage(memorystream);

        var channels = output.Channels.ToList();
        Assert.Equal(6, channels.Count);
        Assert.Equal(PixelChannel.Cyan, channels[0]);
        Assert.Equal(PixelChannel.Magenta, channels[1]);
        Assert.Equal(PixelChannel.Yellow, channels[2]);
        Assert.Equal(PixelChannel.Black, channels[3]);
        Assert.Equal(PixelChannel.Alpha, channels[4]);
        Assert.Equal(PixelChannel.Meta0, channels[5]);
    }

    private static void TestValue(IIptcProfile profile, IptcTag tag, string expectedValue)
    {
        var value = profile.GetValue(tag);
        Assert.NotNull(value);
        Assert.Equal(expectedValue, value.Value);
    }
}
