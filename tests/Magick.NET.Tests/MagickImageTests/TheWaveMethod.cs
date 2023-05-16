// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheWaveMethod
    {
        [Fact]
        public void ShouldAddWaveEffectToImage()
        {
            using var image = new MagickImage(Files.TestPNG);
            using var original = image.Clone();
            image.Wave();

            Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.62619, 0.62623);
        }
    }
}
