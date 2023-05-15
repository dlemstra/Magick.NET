// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheRaiseMethod
    {
        [Fact]
        public void ShouldLightenTheEdges()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            image.Raise(30);

            ColorAssert.Equal(new MagickColor("#6ee29508b532"), image, 29, 30);
            ColorAssert.Equal(new MagickColor("#2f2054867aac"), image, 570, 265);
        }
    }
}
