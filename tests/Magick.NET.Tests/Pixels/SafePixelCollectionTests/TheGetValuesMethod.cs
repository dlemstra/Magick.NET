// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheGetValuesMethod
    {
        [Fact]
        public void ShouldReturnAllPixels()
        {
            using var image = new MagickImage(MagickColors.Purple, 4, 2);
            using var pixels = image.GetPixels();
            var values = pixels.GetValues();
            var length = 4 * 2 * 3;

            Assert.NotNull(values);
            Assert.Equal(length, values.Length);
        }
    }
}
