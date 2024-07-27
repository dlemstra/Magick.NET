// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheMinifyMethod
    {
        [Fact]
        public void ShouldReduceImageByIntegralSize()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.Minify();

            Assert.Equal(64U, image.Width);
            Assert.Equal(64U, image.Height);
        }
    }
}
