// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class TheEpsCoder
{
    [Fact]
    public void ShouldReadTwoImages()
    {
        if (!Ghostscript.IsAvailable)
            return;

        using var images = new MagickImageCollection(Files.Coders.SwedenHeartEPS);

        Assert.Equal(2, images.Count);

        Assert.Equal(447U, images[0].Width);
        Assert.Equal(420U, images[0].Height);
        Assert.Equal(MagickFormat.Ept, images[0].Format);

        Assert.Equal(447U, images[1].Width);
        Assert.Equal(420U, images[1].Height);
        Assert.Equal(MagickFormat.Tiff, images[1].Format);
    }

    [Fact]
    public void ShouldReadMonoChromeImageWhenUseMonochromeIsTrue()
    {
        if (!Ghostscript.IsAvailable)
            return;

        var settings = new MagickReadSettings
        {
            UseMonochrome = true,
        };

        using var image = new MagickImage(Files.Coders.SwedenHeartEPS, settings);

        Assert.Equal(447U, image.Width);
        Assert.Equal(420U, image.Height);
        ColorAssert.Equal(MagickColors.Black, image, 223, 61);
        ColorAssert.Equal(MagickColors.Black, image, 263, 255);
        ColorAssert.Equal(MagickColors.White, image, 223, 62);
        ColorAssert.Equal(MagickColors.White, image, 193, 254);
    }

    [Fact]
    public void ShouldReadClipPathsInTiffPreview()
    {
        if (!Ghostscript.IsAvailable)
            return;

        using var images = new MagickImageCollection(Files.Coders.SwedenHeartEPS);

        var profile = images[1].Get8BimProfile();

        Assert.NotNull(profile);

        var clipPaths = profile.ClipPaths;

        Assert.Single(clipPaths);

        var clipPath = clipPaths[0].Path.CreateNavigator()?.OuterXml;

        var expected = @"<svg width=""447"" height=""420"">
  <g>
    <path fill=""#00000000"" stroke=""#00000000"" stroke-width=""0"" stroke-antialiasing=""false"" d=""M 130 24&#xA;C 177.197 22.997 198.698 39.3 224 59&#xA;C 226.333 56.667 228.667 54.333 231 52&#xA;C 252.311 41.657 267.075 28.759 297 24&#xA;C 376.672 11.329 446.714 100.194 419 180&#xA;C 405.782 218.063 373.582 241.858 348 268&#xA;C 306.671 310.996 265.329 354.004 224 397&#xA;C 223 396.333 222 395.667 221 395&#xA;C 208.335 382.001 195.665 368.999 183 356&#xA;C 156.003 327.336 128.997 298.664 102 270&#xA;C 81.254 247.261 53.218 227.821 38 200&#xA;C 12.841 154.003 26.357 90.226 56 60&#xA;C 80.776 34.737 95.296 37.972 130 24 Z&#xA;"" />
  </g>
</svg>";
        Assert.Equal(expected, clipPath);
    }
}
