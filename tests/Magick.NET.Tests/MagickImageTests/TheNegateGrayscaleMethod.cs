// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheNegateGrayscaleMethod
    {
        [Fact]
        public void ShouldOnlyNegateGrayscale()
        {
            using var image = new MagickImage("xc:white", 2, 1);
            using var pixels = image.GetPixels();
            var pixel = pixels.GetPixel(1, 0);
            pixel.SetChannel(1, 0);
            pixel.SetChannel(2, 0);

            image.NegateGrayscale();

            ColorAssert.Equal(MagickColors.Black, image, 0, 0);
            ColorAssert.Equal(MagickColors.Red, image, 1, 0);
        }

        [Fact]
        public void ShouldNegateTheSpecifiedChannels()
        {
            using var image = new MagickImage("xc:white", 2, 1);
            using var pixels = image.GetPixels();
            var pixel = pixels.GetPixel(1, 0);
            pixel.SetChannel(1, 0);
            pixel.SetChannel(2, 0);

            image.NegateGrayscale(Channels.Green);

            ColorAssert.Equal(MagickColors.Fuchsia, image, 0, 0);
            ColorAssert.Equal(MagickColors.Red, image, 1, 0);
        }
    }
}
