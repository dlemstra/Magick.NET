// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class ThePosterizeMethod
    {
        [Fact]
        public void ShouldPosterizeTheImage()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            image.Posterize(5);

#if Q8
            ColorAssert.Equal(new MagickColor("#4080bf"), image, 300, 150);
            ColorAssert.Equal(new MagickColor("#404080"), image, 495, 270);
            ColorAssert.Equal(new MagickColor("#404040"), image, 445, 255);
#else
            ColorAssert.Equal(new MagickColor("#40008000bfff"), image, 300, 150);
            ColorAssert.Equal(new MagickColor("#400040008000"), image, 495, 270);
            ColorAssert.Equal(new MagickColor("#400040004000"), image, 445, 255);
#endif
        }

        [Fact]
        public void ShouldPosterizeTheSpecifiedChannel()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            image.Posterize(5, Channels.Red);

#if Q8
            ColorAssert.Equal(new MagickColor("#4073a4"), image, 300, 150);
            ColorAssert.Equal(new MagickColor("#405b64"), image, 495, 270);
            ColorAssert.Equal(new MagickColor("#404b54"), image, 445, 255);
#else
            ColorAssert.Equal(new MagickColor("#40007342a43f"), image, 300, 150);
            ColorAssert.Equal(new MagickColor("#40005389648a"), image, 495, 270);
            ColorAssert.Equal(new MagickColor("#40004b045492"), image, 445, 255);
#endif
        }
    }
}
