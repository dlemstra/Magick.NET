// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheImplodeMethod
    {
        [Fact]
        public void ShouldImplodeTheImage()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);

            ColorAssert.Equal(new MagickColor("#00000000"), image, 69, 45);

            image.Implode(0.5, PixelInterpolateMethod.Blend);

            ColorAssert.Equal(new MagickColor("#a8dff8"), image, 69, 45);

            image.Implode(-0.5, PixelInterpolateMethod.Background);

            ColorAssert.Equal(new MagickColor("#00000000"), image, 69, 45);
        }
    }
}
