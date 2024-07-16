// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheVignetteMethod
    {
        [Fact]
        public void ShouldSoftenTheEdges()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.BackgroundColor = MagickColors.Aqua;
            image.Vignette();

            ColorAssert.Equal(new MagickColor("#641affffffff"), image, 292, 0);
            ColorAssert.Equal(new MagickColor("#91bdffffffff"), image, 358, 479);
        }

        [Fact]
        public void ShouldUseTheCorrectDefaultValues()
        {
            using var image = new MagickImage(Files.NoisePNG);
            using var other = image.Clone();
            image.Vignette();
            other.Vignette(0.0, 1.0, 0, 0);

            Assert.Equal(0, other.Compare(image, ErrorMetric.RootMeanSquared));
        }
    }
}
