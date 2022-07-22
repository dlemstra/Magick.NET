// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSetColormapColorMethod
        {
            [Fact]
            public void ShouldChangeTheColorAtTheSpecifiedIndex()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProGIF))
                {
                    image.SetColormapColor(0, MagickColors.Fuchsia);
                    ColorAssert.Equal(MagickColors.Fuchsia, image.GetColormapColor(0));
                }
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenImageHasNoColormap()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    image.SetColormapColor(0, MagickColors.Fuchsia);
                }
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenIndexIsOutOfRange()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProGIF))
                {
                    image.SetColormapColor(65536, MagickColors.Fuchsia);
                    Assert.Null(image.GetColormapColor(65536));
                }
            }
        }
    }
}
