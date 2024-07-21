// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableAffineTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldHaveTheCorrectDefaultValues()
        {
            var affine = new DrawableAffine();
            Assert.Equal(1.0, affine.ScaleX);
            Assert.Equal(1.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }

        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            var affine = new DrawableAffine(1.0, 2.0, 3.0, 4.0, 5.0, 6.0);

            Assert.Equal(1.0, affine.ScaleX);
            Assert.Equal(2.0, affine.ScaleY);
            Assert.Equal(3.0, affine.ShearX);
            Assert.Equal(4.0, affine.ShearY);
            Assert.Equal(5.0, affine.TranslateX);
            Assert.Equal(6.0, affine.TranslateY);
        }
    }
}
