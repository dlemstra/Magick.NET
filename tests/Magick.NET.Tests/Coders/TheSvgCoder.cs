// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class TheSvgCoder
{
    [Fact]
    public void ShouldDetectFormatFromXmlDeclaration()
    {
        var data = Encoding.ASCII.GetBytes(@"<?xml version=""1.0"" encoding=""UTF-8""?><circle />");

        var info = new MagickImageInfo(data);

        Assert.Equal(MagickFormat.Svg, info.Format);
        Assert.Equal(0, info.Width);
        Assert.Equal(0, info.Height);
    }

    [Fact]
    public void ShouldDetectFormatFromSvgTag()
    {
        var data = Encoding.ASCII.GetBytes(@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""1000"" height=""716"" />");

        var info = new MagickImageInfo(data);

        Assert.Equal(MagickFormat.Svg, info.Format);
        Assert.Equal(1000, info.Width);
        Assert.Equal(716, info.Height);
    }

    [Fact]
    public void ShouldUseWidthFromReadSettings()
    {
        var settings = new MagickReadSettings
        {
            Width = 100,
        };

        using var image = new MagickImage();
        image.Read(Files.Logos.MagickNETSVG, settings);

        Assert.Equal(100, image.Width);
        Assert.Equal(48, image.Height);
    }

    [Fact]
    public void ShouldUseHeightFromReadSettings()
    {
        var settings = new MagickReadSettings
        {
            Height = 200,
        };

        using var image = new MagickImage();
        image.Read(Files.Logos.MagickNETSVG, settings);

        Assert.Equal(416, image.Width);
        Assert.Equal(200, image.Height);
    }

    [Fact]
    public void ShouldUseWidthAndHeightFromReadSettings()
    {
        var settings = new MagickReadSettings
        {
            Width = 300,
            Height = 300,
        };

        using var image = new MagickImage();
        image.Read(Files.Logos.MagickNETSVG, settings);

        Assert.Equal(300, image.Width);
        Assert.Equal(144, image.Height);

        image.Ping(Files.Logos.MagickNETSVG, settings);

        Assert.Equal(300, image.Width);
        Assert.Equal(144, image.Height);
    }

    [Fact]
    public void ShouldReadFontsWithQuotes()
    {
        if (TestRuntime.HasFlakyMacOSResult)
            return;

        var svg = @"<?xml version=""1.0"" encoding=""utf-8""?>
<svg version=""1.1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" viewBox=""0 0 220 80"">
<style type=""text/css"">
  .st0{font-family:'Arial';font-size:40}
  .st1{font-family:""Times New Roman"";font-size:40}
</style>
<g id=""changable-text"">
  <text transform=""matrix(1 0 0 1 1 35)"" class=""st0"">FONT TEST</text>
  <text transform=""matrix(1 0 0 1 1 70)"" class=""st1"">FONT TEST</text>
</g>
</svg>";
        var bytes = Encoding.ASCII.GetBytes(svg);
        using var image = new MagickImage(bytes);

        Assert.Equal(220, image.Width);
        Assert.Equal(80, image.Height);

        ColorAssert.Equal(MagickColors.White, image, 118, 6);
        ColorAssert.Equal(MagickColors.Black, image, 120, 6);
        ColorAssert.Equal(MagickColors.Black, image, 141, 6);
        ColorAssert.Equal(MagickColors.White, image, 145, 6);
        ColorAssert.Equal(MagickColors.White, image, 114, 43);
        ColorAssert.Equal(MagickColors.Black, image, 116, 43);
        ColorAssert.Equal(MagickColors.Black, image, 135, 43);
    }

    [Fact]
    public void IsThreadSafe()
    {
        var svg = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<svg width=""50"" height=""15"" xmlns=""http://www.w3.org/2000/svg"">
  <text x=""25"" y=""11"" font-size=""9px"" font-family=""Verdana"">1</text>
</svg>";
        var bytes = Encoding.UTF8.GetBytes(svg);

        var signature = LoadImage(bytes);
        Parallel.For(1, 10, (int i) =>
        {
            Assert.Equal(signature, LoadImage(bytes));
        });
    }

    private static string LoadImage(byte[] bytes)
    {
        using var image = new MagickImage(bytes);
        return image.Signature;
    }
}
