// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class TheCaptionCoder
{
    [Fact]
    public void ShouldAddCorrectLineBreaks1()
    {
        var caption = "caption:Text 2 Verylongtext";
        var settings = new MagickReadSettings
        {
            FontPointsize = 23,
            FillColor = MagickColors.Blue,
            Width = 180,
            Height = 85,
        };

        using var image = new MagickImage(caption, settings);

        Assert.Equal(180U, image.Width);
        Assert.Equal(85U, image.Height);
        ColorAssert.Equal(MagickColors.Blue, image, 55, 20);
    }

    [Fact]
    public void ShouldAddCorrectLineBreaks2()
    {
        var caption = "caption:tex1_124x40_3a277be1b9da51b7_2d0d8f84dc3ccc36_8";
        var settings = new MagickReadSettings
        {
            BackgroundColor = MagickColors.Transparent,
            FontPointsize = 39,
            FillColor = MagickColors.Red,
            TextUnderColor = MagickColors.Green,
            TextGravity = Gravity.Center,
            Width = 450,
        };

        using var image = new MagickImage(caption, settings);

        Assert.Equal(450U, image.Width);
        Assert.Equal(138U, image.Height);
        ColorAssert.Equal(MagickColors.Green, image, 155, 57);
        ColorAssert.Equal(MagickColors.Green, image, 178, 80);
        ColorAssert.Equal(MagickColors.Red, image, 441, 26);
        ColorAssert.Equal(MagickColors.Red, image, 395, 55);
        ColorAssert.Equal(MagickColors.Red, image, 231, 116);
        ColorAssert.Equal(new MagickColor("#0000"), image, 170, 93);
    }

    [Fact]
    public void ShouldAddCorrectLineBreaks3()
    {
        var caption = "caption:Dans votre vie, vous mangerez environ 30 000 kilos de nourriture, l’équivalent du poids de 6 éléphants.";
        var settings = new MagickReadSettings
        {
            TextGravity = Gravity.Center,
            Width = 465,
            Height = 101,
        };

        using var image = new MagickImage(caption, settings);

        Assert.Equal(465U, image.Width);
        Assert.Equal(101U, image.Height);
        ColorAssert.Equal(MagickColors.Black, image, 14, 73);
        ColorAssert.Equal(MagickColors.Black, image, 220, 49);
        ColorAssert.Equal(MagickColors.White, image, 307, 49);
        ColorAssert.Equal(MagickColors.Black, image, 423, 27);
        ColorAssert.Equal(MagickColors.Black, image, 453, 90);
    }

    [Fact]
    public void ShouldAddCorrectLineBreaks4()
    {
        var caption = "caption:This does not wrap";
        var settings = new MagickReadSettings
        {
            FontPointsize = 50,
            Width = 400,
        };

        using var image = new MagickImage(caption, settings);

        Assert.Equal(400U, image.Width);
        Assert.Equal(116U, image.Height);
        ColorAssert.Equal(MagickColors.White, image, 321, 30);
        ColorAssert.Equal(MagickColors.Black, image, 86, 86);
    }

    [Fact]
    public void ShouldAddCorrectLineBreaks5()
    {
        var caption = "caption:A";
        var settings = new MagickReadSettings
        {
            BackgroundColor = MagickColors.Transparent,
            FontPointsize = 72,
            TextGravity = Gravity.West,
            FillColor = MagickColors.Black,
            Width = 40,
        };

        using var image = new MagickImage(caption, settings);

        Assert.Equal(40U, image.Width);
        Assert.Equal(83U, image.Height);
        ColorAssert.Equal(MagickColors.Black, image, 39, 46);
        ColorAssert.Equal(new MagickColor("#0000"), image, 39, 65);
    }

    [Fact]
    public void ShouldAddCorrectLineBreaks6()
    {
        var caption = "caption:AAA";
        var settings = new MagickReadSettings
        {
            BackgroundColor = MagickColors.Transparent,
            FontPointsize = 72,
            TextGravity = Gravity.West,
            FillColor = MagickColors.Black,
            Width = 40,
        };

        using var image = new MagickImage(caption, settings);

        Assert.Equal(40U, image.Width);
        Assert.Equal(249U, image.Height);
        ColorAssert.Equal(MagickColors.Black, image, 39, 47);
        ColorAssert.Equal(new MagickColor("#0000"), image, 39, 66);
        ColorAssert.Equal(MagickColors.Black, image, 39, 129);
        ColorAssert.Equal(new MagickColor("#0000"), image, 39, 148);
        ColorAssert.Equal(MagickColors.Black, image, 39, 211);
        ColorAssert.Equal(new MagickColor("#0000"), image, 39, 230);
    }
}

#endif
