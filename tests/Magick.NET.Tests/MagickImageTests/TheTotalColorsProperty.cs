// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTotalColorsProperty
    {
        [Fact]
        public void ShouldReturnZeroForEmptyImage()
        {
            using var image = new MagickImage();

            Assert.Equal(0U, image.TotalColors);
        }

        [Fact]
        public void ShouldReturnTheTotalNumberOfColors()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Equal(256U, image.TotalColors);
        }
    }
}
