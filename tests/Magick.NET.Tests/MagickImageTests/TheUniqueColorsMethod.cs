// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheUniqueColorsMethod
    {
        [Fact]
        public void ShouldReturnTheUniqueColorsAsAnImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var uniqueColors = image.UniqueColors();

            Assert.Equal(1U, uniqueColors.Height);
            Assert.Equal(256U, uniqueColors.Width);
        }
    }
}
