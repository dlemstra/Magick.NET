// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class ThePangoCoder
{
    [Fact]
    public void ShouldUseInterlineSpacingSetting()
    {
        var settings = new MagickReadSettings()
        {
            TextInterlineSpacing = 20,
        };

        using var imageA = new MagickImage("pango:Test\nTest");
        using var imageB = new MagickImage("pango:Test\nTest", settings);

        Assert.NotEqual(imageA.Height, imageB.Height);
    }

    [Fact]
    public void ShouldUseTextAntiAliasSetting()
    {
        if (TestRuntime.HasFlakyMacOSArm64Result)
            return;

        var settings = new MagickReadSettings()
        {
            AntiAlias = false,
        };

        var pango = @"pango:<span font_family=""Arial"">1</span>";

        using var imageA = new MagickImage(pango);
        using var imageB = new MagickImage(pango, settings);

        Assert.NotEqual(imageA.Signature, imageB.Signature);
    }

    [Fact]
    public void IsThreadSafe()
    {
        string LoadImage()
        {
            using var image = new MagickImage("pango:1");
            return image.Signature;
        }

        var signature = LoadImage();
        Parallel.For(1, 10, (int i) =>
        {
            Assert.Equal(signature, LoadImage());
        });
    }

    [Fact]
    public void CanReadFromLargePangoFile()
    {
        var fileName = "pango:<span font=\"Arial\">" + new string('*', 4500) + "</span>";
        using var image = new MagickImage(fileName);
    }
}
