// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheShaveMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenSizeIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("leftRight", () => image.Shave(-1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenLeftRightIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("leftRight", () => image.Shave(-1, 40));
        }

        [Fact]
        public void ShouldThrowExceptionWhenTopBottomIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("topBottom", () => image.Shave(20, -1));
        }

        [Fact]
        public void ShouldShaveSizeFromEdges()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Shave(10);

            Assert.Equal(620, image.Width);
            Assert.Equal(460, image.Height);
        }

        [Fact]
        public void ShouldShavePixelsFromEdges()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Shave(20, 40);

            Assert.Equal(600, image.Width);
            Assert.Equal(400, image.Height);
        }
    }
}
