// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheClutMethod
    {
        [Fact]
        public void ShouldApplyTheSpecifiedColorTable()
        {
            using var images = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 1, 1),
                new MagickImage(MagickColors.Blue, 1, 1),
                new MagickImage(MagickColors.Green, 1, 1),
            };

            using var pallete = images.AppendHorizontally();
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Clut(pallete, PixelInterpolateMethod.Catrom);

            ColorAssert.Equal(MagickColors.Green, image, 400, 300);
        }

        [Fact]
        public void ShouldUseTheSpecifiedChannels()
        {
            using var image = new MagickImage(Files.RedPNG);
            using var black = new MagickImage(MagickColors.Black, 1, 1);
            image.Clut(black, Channels.RGB);

            ColorAssert.Equal(MagickColors.Black, image, 100, 100);
            ColorAssert.Equal(MagickColors.Transparent, image, 230, 100);
            ColorAssert.Equal(MagickColors.Black, image, 300, 100);
            ColorAssert.Equal(new MagickColor("#00000080"), image, 500, 100);
        }
    }
}
