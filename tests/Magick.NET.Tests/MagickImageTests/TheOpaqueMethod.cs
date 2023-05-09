// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheOpaqueMethod
    {
        [Fact]
        public void ShouldChangePixelsThatMatchesTarget()
        {
            using (var image = new MagickImage(MagickColors.Red, 1, 1))
            {
                image.Opaque(MagickColors.Red, MagickColors.Yellow);
                ColorAssert.Equal(MagickColors.Yellow, image, 0, 0);
            }
        }

        [Fact]
        public void ShouldNotChangePixelsThatDoNotMatchesTarget()
        {
            using (var image = new MagickImage(MagickColors.Red, 1, 1))
            {
                image.Opaque(MagickColors.Yellow, MagickColors.Red);
                ColorAssert.Equal(MagickColors.Red, image, 0, 0);
            }
        }
    }
}
