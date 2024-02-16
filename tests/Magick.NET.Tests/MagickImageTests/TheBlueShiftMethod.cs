// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheBlueShiftMethod
    {
        [Fact]
        public void ShouldSimulateNighttimeScene()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            ColorAssert.NotEqual(MagickColors.White, image, 180, 80);

            image.BlueShift(2);

#if Q16HDRI
            ColorAssert.NotEqual(MagickColors.White, image, 180, 80);
            image.Clamp();
#endif

            ColorAssert.Equal(MagickColors.White, image, 180, 80);
            ColorAssert.Equal(new MagickColor("#ac2cb333c848"), image, 350, 265);
        }
    }
}
