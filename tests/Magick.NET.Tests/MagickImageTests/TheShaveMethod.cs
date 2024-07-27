// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheShaveMethod
    {
        [Fact]
        public void ShouldShaveSizeFromEdges()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Shave(10);

            Assert.Equal(620U, image.Width);
            Assert.Equal(460U, image.Height);
        }

        [Fact]
        public void ShouldShavePixelsFromEdges()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Shave(20, 40);

            Assert.Equal(600U, image.Width);
            Assert.Equal(400U, image.Height);
        }
    }
}
