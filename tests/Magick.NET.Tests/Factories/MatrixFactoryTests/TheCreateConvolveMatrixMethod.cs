// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MatrixFactoryTests
{
    public class TheCreateConvolveMatrixMethod
    {
        [Fact]
        public void ShouldCreateInstance()
        {
            var factory = new MatrixFactory();

            var matrix = factory.CreateConvolveMatrix(1);

            Assert.NotNull(matrix);
            Assert.IsType<ConvolveMatrix>(matrix);
        }

        [Fact]
        public void ShouldCreateInstanceWithValues()
        {
            var factory = new MatrixFactory();

            var matrix = factory.CreateConvolveMatrix(1, 2);

            Assert.NotNull(matrix);
            Assert.IsType<ConvolveMatrix>(matrix);
            Assert.Equal(2, matrix.GetValue(0, 0));
        }
    }
}
