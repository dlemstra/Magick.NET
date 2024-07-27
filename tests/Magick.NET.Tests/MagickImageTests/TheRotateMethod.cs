// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheRotateMethod
    {
        [Fact]
        public void ShouldRotateTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Equal(640U, image.Width);
            Assert.Equal(480U, image.Height);

            image.Rotate(90);

            Assert.Equal(480U, image.Width);
            Assert.Equal(640U, image.Height);
        }
    }
}
