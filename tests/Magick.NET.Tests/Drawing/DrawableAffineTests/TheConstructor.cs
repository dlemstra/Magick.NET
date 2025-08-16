// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableAffineTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldSetTheCorrectProperties()
        {
            var affine = new DrawableAffine(2.0, 3.0, 4.0, 5.0, 6.0, 7.0);
            Assert.Equal(2.0, affine.ScaleX);
            Assert.Equal(3.0, affine.ScaleY);
            Assert.Equal(4.0, affine.ShearX);
            Assert.Equal(5.0, affine.ShearY);
            Assert.Equal(6.0, affine.TranslateX);
            Assert.Equal(7.0, affine.TranslateY);
        }
    }
}
