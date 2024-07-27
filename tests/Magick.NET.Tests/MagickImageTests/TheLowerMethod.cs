// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheLowerMethod
    {
        [Fact]
        public void ShouldDarkenTheEdges()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            image.Lower(30);

            ColorAssert.Equal(new MagickColor("#2da153c773f1"), image, 29, 30);
            ColorAssert.Equal(new MagickColor("#706195c7bbed"), image, 570, 265);
        }
    }
}
