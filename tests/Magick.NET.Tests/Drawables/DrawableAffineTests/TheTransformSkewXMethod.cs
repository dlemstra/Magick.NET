// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableAffineTests
{
    public class TheTransformSkewXMethod
    {
        [Fact]
        public void ShouldSetThePropertiesToTheCorrectValue()
        {
            var affine = new DrawableAffine();
            affine.TransformSkewX(4.0);

            Assert.Equal(1.0, affine.ScaleX);
            Assert.Equal(1.0, affine.ScaleY);
            Assert.InRange(affine.ShearX, 0.0699, 0.0700);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }
    }
}
