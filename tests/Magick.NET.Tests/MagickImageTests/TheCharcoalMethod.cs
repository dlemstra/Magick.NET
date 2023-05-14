// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCharcoalMethod
    {
        [Fact]
        public void ShouldApplyCharcoalEffect()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Charcoal();

            ColorAssert.Equal(MagickColors.White, image, 424, 412);
        }

        [Fact]
        public void ShouldUseTheSpecifiedRadiusAndSigma()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Charcoal(4, 2);

            ColorAssert.Equal(MagickColors.Black, image, 370, 240);
        }
    }
}
