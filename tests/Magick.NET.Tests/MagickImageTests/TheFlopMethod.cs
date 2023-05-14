// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheFlopMethod
    {
        [Fact]
        public void ShouldFlipTheImageHorizontally()
        {
            using var images = new MagickImageCollection();
            images.Add(new MagickImage(MagickColors.DodgerBlue, 10, 10));
            images.Add(new MagickImage(MagickColors.Firebrick, 10, 10));

            using var image = images.AppendHorizontally();
            ColorAssert.Equal(MagickColors.DodgerBlue, image, 0, 5);
            ColorAssert.Equal(MagickColors.Firebrick, image, 10, 5);

            image.Flop();

            ColorAssert.Equal(MagickColors.Firebrick, image, 0, 5);
            ColorAssert.Equal(MagickColors.DodgerBlue, image, 10, 5);
        }
    }
}
