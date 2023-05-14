// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheDespeckleMethod
    {
        [Fact]
        public void ShouldReduceSpeckleNoise()
        {
            using var image = new MagickImage(Files.NoisePNG);
            var color = new MagickColor("#d1d1d1d1d1d1");

            ColorAssert.NotEqual(color, image, 130, 123);

            image.Despeckle();
            image.Despeckle();
            image.Despeckle();

            ColorAssert.Equal(color, image, 130, 123);
        }
    }
}
