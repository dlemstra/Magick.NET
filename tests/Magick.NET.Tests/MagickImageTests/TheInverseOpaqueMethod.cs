// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheInverseOpaqueMethod
    {
        [Fact]
        public void ShouldNotChangePixelsThatMatchesTarget()
        {
            using (var image = new MagickImage(MagickColors.Red, 1, 1))
            {
                image.InverseOpaque(MagickColors.Red, MagickColors.Yellow);
                ColorAssert.Equal(MagickColors.Red, image, 0, 0);
            }
        }

        [Fact]
        public void ShouldChangePixelsThatDoNotMatchesTarget()
        {
            using (var image = new MagickImage(MagickColors.Red, 1, 1))
            {
                image.InverseOpaque(MagickColors.Yellow, MagickColors.Purple);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }
        }
    }
}
