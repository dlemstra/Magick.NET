// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableAffineTests
{
    public class TheTransformSkewYMethod
    {
        [Fact]
        public void ShouldSetThePropertiesToTheCorrectValue()
        {
            var affine = new DrawableAffine();
            affine.TransformSkewY(2.0);

            Assert.Equal(1.0, affine.ScaleX);
            Assert.Equal(1.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.InRange(affine.ShearY, 0.034, 0.035);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }
    }
}
