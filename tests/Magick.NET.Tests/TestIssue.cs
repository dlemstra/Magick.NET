// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Colors;
using ImageMagick.Configuration;
using ImageMagick.Drawing;
using ImageMagick.Factories;
using ImageMagick.Formats;
using ImageMagick.ImageOptimizers;
using Xunit;

namespace Magick.NET.Tests;

public class TestIssue
{
    [Fact]
    public void RunTest()
    {
        var drawables = new Drawables();

        drawables.FontPointSize(40)
                    .Font(@"I:\issues\im7\1752\font1.ttf")
                    .FillColor(MagickColors.Black)
                    .TextAlignment(TextAlignment.Left)
                    .Text(100, 50, "This is such a nice font!");

        drawables.FontPointSize(40)
                    .Font(@"I:\issues\im7\1752\font2.ttf")
                    .FillColor(MagickColors.Black)
                    .TextAlignment(TextAlignment.Left)
                    .Text(100, 100, "This is even better, or does it look the same?");

        using var image = new MagickImage(MagickColors.White, 1000, 800);
        drawables.Draw(image);
        image.Write(@"I:\issues\im7\1752\z.png");
    }
}
