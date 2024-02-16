// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConvolveMatrixTests
{
    public class TheIndexer
    {
        [Fact]
        public void ShouldThrowExceptionWhenXTooLow()
            => TestThrowsException("x", -1, 0);

        [Fact]
        public void ShouldThrowExceptionWhenXTooHigh()
            => TestThrowsException("x", 1, 0);

        [Fact]
        public void ShouldThrowExceptionWhenYTooLow()
            => TestThrowsException("y", 0, -1);

        [Fact]
        public void ShouldThrowExceptionWhenYTooHigh()
            => TestThrowsException("y", 0, 1);

        [Fact]
        public void ShouldReturnValueForValidIndexes()
        {
            var matrix = new ConvolveMatrix(1, 8);

            Assert.Equal(8, matrix[0, 0]);
        }

        private static void TestThrowsException(string paramName, int x, int y)
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentOutOfRangeException>(paramName, () =>
            {
                var foo = matrix[x, y];
            });
        }
    }
}
