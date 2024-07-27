// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTransposeMethod
    {
        [Fact]
        public void ShouldCreateHorizontalMirrorImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Transpose();

            Assert.Equal(480U, image.Width);
            Assert.Equal(640U, image.Height);
            ColorAssert.Equal(MagickColors.Red, image, 61, 292);
            ColorAssert.Equal(new MagickColor("#f5f5eeee3636"), image, 104, 377);
            ColorAssert.Equal(new MagickColor("#eded1f1f2424"), image, 442, 391);
        }
    }
}
