// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheInterpolativeResizeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            using var image = new MagickImage(Files.RedPNG);
            Assert.Throws<ArgumentException>("width", () => image.InterpolativeResize(-1, 32, PixelInterpolateMethod.Mesh));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            using var image = new MagickImage(Files.RedPNG);
            Assert.Throws<ArgumentException>("height", () => image.InterpolativeResize(32, -1, PixelInterpolateMethod.Mesh));
        }

        [Fact]
        public void ShouldResizeTheImage()
        {
            using var image = new MagickImage(Files.RedPNG);
            image.InterpolativeResize(32, 32, PixelInterpolateMethod.Mesh);

            Assert.Equal(32, image.Width);
            Assert.Equal(11, image.Height);
        }

        [Fact]
        public void ShouldUseThePixelInterpolateMethod()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
            image.InterpolativeResize(150, 100, PixelInterpolateMethod.Mesh);

            Assert.Equal(150, image.Width);
            Assert.Equal(100, image.Height);

            ColorAssert.Equal(new MagickColor("#acacbcbcb2b2"), image, 20, 37);
            ColorAssert.Equal(new MagickColor("#08891d1d4242"), image, 117, 39);
        }
    }
}
