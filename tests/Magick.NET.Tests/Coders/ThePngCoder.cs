// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Linq;
using System.Text;
using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public class ThePngCoder
{
    [Fact]
    public void ShouldThrowExceptionAndNotChangeTheOriginalImageWhenTheImageIsCorrupt()
    {
        using var image = new MagickImage(MagickColors.Purple, 4, 2);

        Assert.Throws<MagickCoderErrorException>(() =>
        {
            image.Read(Files.CorruptPNG);
        });

        Assert.Equal(4U, image.Width);
        Assert.Equal(2U, image.Height);
    }

    [Fact]
    public void ShouldBeAbleToReadPngWithLargeIDAT()
    {
        using var image = new MagickImage(Files.VicelandPNG);

        Assert.Equal(200U, image.Width);
        Assert.Equal(28U, image.Height);
    }

    [Fact]
    public void ShouldNotRaiseWarningForValidModificationDateThatBecomes24Hours()
    {
        using var image = new MagickImage("logo:");
        image.Warning += HandleWarning;
        image.SetAttribute("date:modify", "2017-09-10T20:35:00+03:30");

        image.ToByteArray(MagickFormat.Png);
    }

    [Fact]
    public void ShouldNotRaiseWarningForValidModificationDateThatBecomes60Minutes()
    {
        using var image = new MagickImage("logo:");
        image.Warning += HandleWarning;
        image.SetAttribute("date:modify", "2017-09-10T15:30:00+03:30");

        image.ToByteArray(MagickFormat.Png);
    }

    [Fact]
    public void ShouldReadTheExifChunk()
    {
        using var input = new MagickImage(MagickColors.YellowGreen, 1, 1);

        IExifProfile exifProfile = new ExifProfile();
        exifProfile.SetValue(ExifTag.ImageUniqueID, "Have a nice day");

        input.SetProfile(exifProfile);

        using var memoryStream = new MemoryStream();

        input.Write(memoryStream, MagickFormat.Png);
        memoryStream.Position = 0;

        using var output = new MagickImage(memoryStream);
        exifProfile = output.GetExifProfile();

        Assert.NotNull(exifProfile);

        var value = exifProfile.GetValue(ExifTag.ImageUniqueID);
        Assert.Equal("Have a nice day", value.ToString());
    }

    [Fact]
    public void ShouldSetTheAnimationProperties()
    {
        using var images = new MagickImageCollection(Files.Coders.TestMNG);
        Assert.Equal(8, images.Count);

        foreach (var image in images)
        {
            Assert.Equal(20U, image.AnimationDelay);
            Assert.Equal(100, image.AnimationTicksPerSecond);
        }
    }

    [Fact]
    public void ShouldWritePng00Correctly()
    {
        using var image = new MagickImage(Files.Builtin.Logo);
        using var stream = new MemoryStream();
        image.Write(stream, MagickFormat.Png);

        stream.Position = 0;

        image.Read(stream);

        var setting = new QuantizeSettings
        {
            ColorSpace = ColorSpace.Gray,
            DitherMethod = DitherMethod.Riemersma,
            Colors = 2,
        };

        image.Quantize(setting);

        image.Warning += HandleWarning;

        image.Write(stream, MagickFormat.Png00);

        stream.Position = 0;

        image.Read(stream);

        Assert.Equal(ColorType.Palette, image.ColorType);
        ColorAssert.Equal(MagickColors.White, image, 0, 0);
        ColorAssert.Equal(MagickColors.Black, image, 305, 248);
    }

    [Fact]
    public void ShouldNotWriteJpegAndPngProperties()
    {
        using var input = new MagickImage(Files.Builtin.Logo);
        input.SetAttribute("foo", "bar");
        input.SetAttribute("jpeg:foo", "bar");
        input.SetAttribute("png:foo", "bar");

        using var memoryStream = new MemoryStream();
        input.Write(memoryStream, MagickFormat.Png);
        memoryStream.Position = 0;

        using var output = new MagickImage(memoryStream);
        Assert.Equal("bar", output.GetAttribute("foo"));
        Assert.Null(output.GetAttribute("jpeg:foo"));
        Assert.Null(output.GetAttribute("png:foo"));
    }

    [Fact]
    public void ShouldWriteDateProperties()
    {
        using var tempFile = new TemporaryFile("test.png");

        using var image = new MagickImage(MagickColors.Pink, 1, 1);
        image.Write(tempFile.File);

        var content = GetReadableContent(tempFile);

        var dateCreate = GetTimestamp(content, "date:create");
        Assert.InRange((DateTime.Now - dateCreate).TotalSeconds, 0, 5);

        var dateModify = GetTimestamp(content, "date:modify");
        Assert.InRange((DateTime.Now - dateModify).TotalSeconds, 0, 5);

        var dateTimestamp = GetTimestamp(content, "date:timestamp");
        Assert.InRange((DateTime.Now - dateTimestamp).TotalSeconds, 0, 5);
    }

    [Fact]
    public void ShouldNotWriteDatePropertiesWhenDateShouldBeExcluded()
    {
        using var tempFile = new TemporaryFile("test.png");

        using var input = new MagickImage(MagickColors.Pink, 1, 1);
        input.Settings.SetDefine("png:exclude-chunks", "date");
        input.Write(tempFile.File);

        var content = GetReadableContent(tempFile);

        Assert.DoesNotContain($"date:create", content);
        Assert.DoesNotContain($"date:modify", content);
        Assert.DoesNotContain($"date:timestamp", content);
    }

    [Fact]
    public void ShouldIgnoreTiffAndExifPropertiesWhenWritingPHYsChunk()
    {
        using var input = new MagickImage(MagickColors.Purple, 1, 1);

        // The png coder always writes in PixelsPerCentimeter.
        input.Density = new Density(40, 50, DensityUnit.PixelsPerCentimeter);
        input.SetAttribute("exif:ResolutionUnit", "3");
        input.SetAttribute("exif:XResolution", "60/1");
        input.SetAttribute("exif:YResolution", "70/1");
        input.SetAttribute("tiff:ResolutionUnit", "1");
        input.SetAttribute("tiff:XResolution", "80/1");
        input.SetAttribute("tiff:YResolution", "90/1");

        using var memoryStream = new MemoryStream();
        input.Write(memoryStream, MagickFormat.Png);
        memoryStream.Position = 0;

        using var output = new MagickImage(memoryStream);
        Assert.Equal(DensityUnit.PixelsPerCentimeter, output.Density.Units);
        Assert.Equal(40, output.Density.X);
        Assert.Equal(50, output.Density.Y);
    }

    [Fact]
    public void ShouldUseExifPropertiesWhenIgnoringPHYsChunk()
    {
        using var input = new MagickImage(MagickColors.Purple, 1, 1);
        input.Density = new Density(40, 50, DensityUnit.PixelsPerCentimeter);
        input.SetAttribute("exif:ResolutionUnit", "2");
        input.SetAttribute("exif:XResolution", "60/1");
        input.SetAttribute("exif:YResolution", "70/1");

        using var memoryStream = new MemoryStream();
        input.Settings.SetDefine("png:exclude-chunks", "pHYs");
        input.Write(memoryStream, MagickFormat.Png);
        memoryStream.Position = 0;

        using var output = new MagickImage(memoryStream);
        Assert.Equal(DensityUnit.PixelsPerInch, output.Density.Units);
        Assert.Equal(60, output.Density.X);
        Assert.Equal(70, output.Density.Y);
    }

    [Fact]
    public void ShouldUseTiffPropertiesWhenIgnoringPHYsChunk()
    {
        using var input = new MagickImage(MagickColors.Purple, 1, 1);
        input.Density = new Density(40, 50, DensityUnit.PixelsPerCentimeter);
        input.SetAttribute("tiff:ResolutionUnit", "1");
        input.SetAttribute("tiff:XResolution", "80/1");
        input.SetAttribute("tiff:YResolution", "90/1");

        using var memoryStream = new MemoryStream();
        input.Settings.SetDefine("png:exclude-chunks", "pHYs");
        input.Write(memoryStream, MagickFormat.Png);
        memoryStream.Position = 0;

        using var output = new MagickImage(memoryStream);
        Assert.Equal(DensityUnit.Undefined, output.Density.Units);
        Assert.Equal(80, output.Density.X);
        Assert.Equal(90, output.Density.Y);
    }

    private static string GetReadableContent(TemporaryFile tempfile)
        => Encoding.ASCII.GetString(File.ReadAllBytes(tempfile.File.FullName).Where(b => !char.IsControl((char)b)).ToArray());

    private DateTime GetTimestamp(string content, string name)
    {
        var offset = content.IndexOf(name);

        Assert.NotEqual(-1, offset);

        return DateTime.Parse(content.Substring(offset + name.Length, 25));
    }

    private void HandleWarning(object sender, WarningEventArgs e)
        => throw new XunitException("Warning was raised: " + e.Message);
}
