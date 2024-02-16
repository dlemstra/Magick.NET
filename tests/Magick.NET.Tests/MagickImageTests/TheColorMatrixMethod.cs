// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheColorMatrixMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenMatrixIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("matrix", () => image.ColorMatrix(null));
        }

        [Fact]
        public void ShouldApplyTheSpecifiedColorMatrix()
        {
            using var image = new MagickImage(Files.Builtin.Rose);
            var matrix = new MagickColorMatrix(3, 0, 0, 1, 0, 1, 0, 1, 0, 0);

            image.ColorMatrix(matrix);

            ColorAssert.Equal(MagickColor.FromRgb(58, 31, 255), image, 39, 25);
        }
    }
}
