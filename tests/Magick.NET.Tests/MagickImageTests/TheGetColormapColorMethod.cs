// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheGetColormapColorMethod
        {
            [Fact]
            public void ShouldReturnNullWhenImageHasNoColormap()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    Assert.Null(image.GetColormapColor(0));
                    Assert.Null(image.GetColormapColor(1));
                }
            }

            [Fact]
            public void ShouldReturnTheColorOfTheSpecifiedIndex()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProGIF))
                {
                    ColorAssert.Equal(new MagickColor("#040d14"), image.GetColormapColor(0));
                }
            }
        }
    }
}
