// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class ThePolaroidMethod
    {
        [Fact]
        public void ShouldSimulatesPolaroidPicture()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.BorderColor = MagickColors.Red;
                image.BackgroundColor = MagickColors.Fuchsia;
                image.Settings.FontPointsize = 20;
                image.Polaroid("Magick.NET", 10, PixelInterpolateMethod.Bilinear);
                image.Clamp();

                ColorAssert.Equal(MagickColors.Black, image, 104, 163);
                ColorAssert.Equal(MagickColors.Red, image, 72, 156);
#if Q8
                ColorAssert.Equal(new MagickColor("#ff00ffbc"), image, 146, 196);
#else
                ColorAssert.Equal(new MagickColor("#ffff0000ffffbb9a"), image, 146, 196);
#endif
            }
        }
    }
}
