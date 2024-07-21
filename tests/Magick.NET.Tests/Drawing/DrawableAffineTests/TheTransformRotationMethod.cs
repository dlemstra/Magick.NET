// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableAffineTests
{
    public class TheTransformRotationMethod
    {
        [Fact]
        public void ShouldSetThePropertiesToTheCorrectValue()
        {
            var affine = new DrawableAffine();
            affine.TransformRotation(45.0);

            Assert.InRange(affine.ScaleX, 0.7071, 0.7072);
            Assert.InRange(affine.ScaleY, 0.7071, 0.7072);
            Assert.InRange(affine.ShearX, -0.7072, -0.7071);
            Assert.InRange(affine.ShearY, 0.7071, 0.7072);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }
    }
}
