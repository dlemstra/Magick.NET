// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheRotationalBlurMethod
    {
        [Fact]
        public void ShouldRotationalBlurTheImage()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.RotationalBlur(20);

#if Q8
            ColorAssert.Equal(new MagickColor("#fbfbfb2b"), image, 10, 10);
            ColorAssert.Equal(new MagickColor("#8b0303"), image, 13, 67);
            ColorAssert.Equal(new MagickColor("#167616"), image, 63, 67);
            ColorAssert.Equal(new MagickColor("#3131fc"), image, 125, 67);
#else
            ColorAssert.Equal(new MagickColor("#fbf7fbf7fbf72aab"), image, 10, 10);
            ColorAssert.Equal(new MagickColor("#8b2102990299"), image, 13, 67);
            ColorAssert.Equal(new MagickColor("#159275F21592"), image, 63, 67);
            ColorAssert.Equal(new MagickColor("#31853185fd47"), image, 125, 67);
#endif
        }

        [Fact]
        public void ShouldRotationalBlurTheSpecifiedChannels()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.RotationalBlur(20, Channels.RGB);

#if Q8
            ColorAssert.Equal(new MagickColor("#fbfbfb80"), image, 10, 10);
            ColorAssert.Equal(new MagickColor("#8b0303"), image, 13, 67);
            ColorAssert.Equal(new MagickColor("#167616"), image, 63, 67);
            ColorAssert.Equal(new MagickColor("#3131fc"), image, 125, 67);
#else
            ColorAssert.Equal(new MagickColor("#fbf7fbf7fbf78000"), image, 10, 10);
            ColorAssert.Equal(new MagickColor("#8b2102990299"), image, 13, 67);
            ColorAssert.Equal(new MagickColor("#159275f21592"), image, 63, 67);
            ColorAssert.Equal(new MagickColor("#31853185fd47"), image, 125, 67);
#endif
        }
    }
}
