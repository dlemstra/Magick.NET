// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class TheCaptionCoder
{
    [Fact]
    public void ShouldNotReplaceNonBreakingSpaceWithNewline()
    {
        var settings = new MagickReadSettings
        {
            Font = Files.Fonts.Arial,
            FontPointsize = 19,
            Width = 160,
        };

        using var image = new MagickImage("caption:Нуорунен в Карелии", settings);
        image.Trim();

        Assert.Equal(88U, image.Width);
        Assert.Equal(41U, image.Height);
    }

    [Fact]
    public void ShouldReplaceOghamSpaceMarkWithNewline()
    {
        var settings = new MagickReadSettings
        {
            Font = Files.Fonts.Arial,
            FontPointsize = 19,
            Width = 160,
        };

        using var image = new MagickImage("caption:Нуорунен в Карелии", settings);
        image.Trim();

        Assert.Equal(99U, image.Width);
        Assert.Equal(41U, image.Height);
    }

    [Fact]
    public void ShouldAddNewlineWhenTextDoesNotContainNewline()
    {
        var settings = new MagickReadSettings
        {
            Font = Files.Fonts.Arial,
            FontPointsize = 19,
            Width = 160,
        };

        using var image = new MagickImage("caption:НуоруненвКарелии", settings);
        image.Trim();

        Assert.Equal(157U, image.Width);
        Assert.Equal(37U, image.Height);
    }

    [Fact]
    public void ShouldJoinFontLigatures()
    {
        var settings = new MagickReadSettings
        {
            Font = Files.Fonts.GloockRegular,
            FontPointsize = 200,
        };

        using var image = new MagickImage("label:find fly", settings);
        image.Trim();

        Assert.Equal(635U, image.Width);
        Assert.Equal(204U, image.Height);

        ColorAssert.Equal(MagickColors.Black, image, 50, 58);
        ColorAssert.Equal(MagickColors.Black, image, 475, 3);
    }
}
