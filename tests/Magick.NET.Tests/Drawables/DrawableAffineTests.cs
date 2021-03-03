// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DrawableAffineTests
    {
        [Fact]
        public void Test_Reset()
        {
            DrawableAffine affine = new DrawableAffine();

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

            affine.Reset();
            Assert.Equal(1.0, affine.ScaleX);
            Assert.Equal(1.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }

        [Fact]
        public void Test_TransformOrigin()
        {
            DrawableAffine affine = new DrawableAffine();
            affine.TransformOrigin(4.0, 2.0);

            Assert.Equal(1.0, affine.ScaleX);
            Assert.Equal(1.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(4.0, affine.TranslateX);
            Assert.Equal(2.0, affine.TranslateY);
        }

        [Fact]
        public void Test_TransformRatation()
        {
            DrawableAffine affine = new DrawableAffine();
            affine.TransformRotation(45.0);

            Assert.InRange(affine.ScaleX, 0.7071, 0.7072);
            Assert.InRange(affine.ScaleY, 0.7071, 0.7072);
            Assert.InRange(affine.ShearX, -0.7072, -0.7071);
            Assert.InRange(affine.ShearY, 0.7071, 0.7072);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }

        [Fact]
        public void Test_TransformScale()
        {
            DrawableAffine affine = new DrawableAffine();
            affine.TransformScale(4.0, 2.0);

            Assert.Equal(4.0, affine.ScaleX);
            Assert.Equal(2.0, affine.ScaleY);
            Assert.Equal(0.0, affine.ShearX);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }

        [Fact]
        public void Test_TransformSkew()
        {
            DrawableAffine affine = new DrawableAffine();
            affine.TransformSkewX(4.0);

            Assert.Equal(1.0, affine.ScaleX);
            Assert.Equal(1.0, affine.ScaleY);
            Assert.InRange(affine.ShearX, 0.0699, 0.0700);
            Assert.Equal(0.0, affine.ShearY);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);

            affine.TransformSkewY(2.0);

            Assert.Equal(1.0, affine.ScaleX);
            Assert.InRange(affine.ScaleY, 1.0024, 1.0025);
            Assert.InRange(affine.ShearX, 0.0699, 0.0700);
            Assert.InRange(affine.ShearY, 0.0349, 0.0350);
            Assert.Equal(0.0, affine.TranslateX);
            Assert.Equal(0.0, affine.TranslateY);
        }
    }
}
