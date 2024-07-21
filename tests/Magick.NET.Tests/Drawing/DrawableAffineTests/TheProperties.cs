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
        public void ShouldSetTheCorrectValue()
        {
            var affine = new DrawableAffine();

            Assert.Equal(1.0, affine.ScaleX);
            Assert.Equal(1.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);

            affine.ScaleX = 2.0;
            Assert.Equal(2.0, affine.ScaleX);
            Assert.Equal(1.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);

            affine.ScaleY = 3.0;
            Assert.Equal(2.0, affine.ScaleX);
            Assert.Equal(3.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);

            affine.ShearX = 4.0;
            Assert.Equal(2.0, affine.ScaleX);
            Assert.Equal(3.0, affine.ScaleY);
            Assert.Equal(4.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);

            affine.ShearY = 5.0;
            Assert.Equal(2.0, affine.ScaleX);
            Assert.Equal(3.0, affine.ScaleY);
            Assert.Equal(4.0, affine.ShearX);
            Assert.Equal(5.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);

            affine.TranslateX = 6.0;
            Assert.Equal(2.0, affine.ScaleX);
            Assert.Equal(3.0, affine.ScaleY);
            Assert.Equal(4.0, affine.ShearX);
            Assert.Equal(5.0, affine.ShearY);
            Assert.Equal(6.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);

            affine.TranslateY = 7.0;
            Assert.Equal(2.0, affine.ScaleX);
            Assert.Equal(3.0, affine.ScaleY);
            Assert.Equal(4.0, affine.ShearX);
            Assert.Equal(5.0, affine.ShearY);
            Assert.Equal(6.0, affine.TranslateX);
            Assert.Equal(7.0, affine.TranslateY);
        }
    }
}
