// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheBrightnessContrastMethod
    {
        [Fact]
        public void ShouldChangeBrightnessAndContrastOfTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);

            ColorAssert.NotEqual(MagickColors.White, image, 340, 295);

            image.BrightnessContrast(new Percentage(50), new Percentage(50));
            image.Clamp();

            ColorAssert.Equal(MagickColors.White, image, 340, 295);
        }

        [Fact]
        public void ShouldChangeTheSpecifiedChannel()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);

            ColorAssert.NotEqual(MagickColors.White, image, 340, 295);

            image.BrightnessContrast(new Percentage(50), new Percentage(50), Channels.Red);
            image.Clamp();

            ColorAssert.Equal(new MagickColor("#FFE9F2FF"), image, 340, 295);
        }
    }
}
