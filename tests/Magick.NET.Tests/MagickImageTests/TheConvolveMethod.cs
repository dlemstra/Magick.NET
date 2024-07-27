// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheConvolveMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenMatrixIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("matrix", () => image.Convolve(null));
        }

        [Fact]
        public void ShouldApplyTheSpecifiedMatrix()
        {
            using var image = new MagickImage("xc:", 1, 1);
            image.BorderColor = MagickColors.Black;
            image.Border(5);

            Assert.Equal(11U, image.Width);
            Assert.Equal(11U, image.Height);

            var matrix = new ConvolveMatrix(3, 0, 0.5, 0, 0.5, 1, 0.5, 0, 0.5, 0);
            image.Convolve(matrix);

            var gray = new MagickColor("#800080008000");
            ColorAssert.Equal(MagickColors.Black, image, 4, 4);
            ColorAssert.Equal(gray, image, 5, 4);
            ColorAssert.Equal(MagickColors.Black, image, 6, 4);
            ColorAssert.Equal(gray, image, 4, 5);
            ColorAssert.Equal(MagickColors.White, image, 5, 5);
            ColorAssert.Equal(gray, image, 6, 5);
            ColorAssert.Equal(MagickColors.Black, image, 4, 6);
            ColorAssert.Equal(gray, image, 5, 6);
            ColorAssert.Equal(MagickColors.Black, image, 6, 6);
        }
    }
}
