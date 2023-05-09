// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheWaveletDenoiseMethod
    {
        [Fact]
        public void ShouldRemoveNoiseFromImage()
        {
            using (var image = new MagickImage(Files.NoisePNG))
            {
#if Q8
                var color = new MagickColor("#dd");
#elif Q16
                var color = new MagickColor(OpenCLValue.Get("#dea4dea4dea4", "#deb5deb5deb5"));
#else
                var color = new MagickColor(OpenCLValue.Get("#dea5dea5dea5", "#deb5deb5deb5"));
#endif

                ColorAssert.NotEqual(color, image, 130, 123);

                image.ColorType = ColorType.TrueColor;
                image.WaveletDenoise((Percentage)25);

                ColorAssert.Equal(color, image, 130, 123);
            }
        }
    }
}
