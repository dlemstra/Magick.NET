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
            using var images = new MagickImageCollection();
            images.Add(new MagickImage(MagickColors.Red, 1, 1));
            images.Add(new MagickImage(MagickColors.Blue, 1, 1));
            images.Add(new MagickImage(MagickColors.Green, 1, 1));

            using var pallete = images.AppendHorizontally();
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Clut(pallete, PixelInterpolateMethod.Catrom);

            ColorAssert.Equal(MagickColors.Green, image, 400, 300);
        }
    }
}
