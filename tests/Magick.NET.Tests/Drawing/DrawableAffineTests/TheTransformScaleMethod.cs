// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableAffineTests
{
    public class TheTransformScaleMethod
    {
        [Fact]
        public void ShouldSetThePropertiesToTheCorrectValue()
        {
            var affine = new DrawableAffine();
            affine.TransformScale(4.0, 2.0);

            Assert.Equal(4.0, affine.ScaleX);
            Assert.Equal(2.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }
    }
}
