// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheMagnifyMethod
    {
        [Fact]
        public void ShouldMagnifyTheImage()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.Magnify();

            Assert.Equal(256U, image.Width);
            Assert.Equal(256U, image.Height);
        }
    }
}
