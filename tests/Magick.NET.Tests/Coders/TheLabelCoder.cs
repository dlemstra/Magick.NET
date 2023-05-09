// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class TheLabelCoder
{
    [Fact]
    public void ShouldUseTheCorrectFontSize()
    {
        var settings = new MagickReadSettings
        {
            Width = 300,
            Font = Files.Fonts.KaushanScript,
        };

        using var image = new MagickImage("label:asf", settings);

        ColorAssert.Equal(MagickColors.Black, image, 293, 68);
        ColorAssert.Equal(MagickColors.Black, image, 17, 200);
    }

    [Fact]
    public void ShouldUseTheCorrectOffset()
    {
        var settings = new MagickReadSettings
        {
            Width = 650,
            Font = Files.Fonts.PhillySans,
        };

        using var image = new MagickImage("label:snow", settings);

        ColorAssert.Equal(MagickColors.White, image, 0, 232);
        ColorAssert.Equal(MagickColors.Black, image, 2, 234);
        ColorAssert.Equal(MagickColors.White, image, 636, 140);
        ColorAssert.Equal(MagickColors.Black, image, 633, 140);
    }
}
