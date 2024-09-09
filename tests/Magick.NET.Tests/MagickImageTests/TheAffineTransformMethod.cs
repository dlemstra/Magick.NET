// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheAffineTransformMethod
    {
        [Fact]
        public void ShouldChangeTheSizeOfTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            var affineMatrix = new DrawableAffine(1, 0.5, 0, 0, 0, 0);
            image.AffineTransform(affineMatrix);

            Assert.Equal(482U, image.Width);
            Assert.Equal(322U, image.Height);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAffineMatrixIsNull()
        {
            using var image = new MagickImage(MagickColors.Purple, 1, 1);

            Assert.Throws<ArgumentNullException>("affineMatrix", () => image.AffineTransform(null!));
        }
    }
}
