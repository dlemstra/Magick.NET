// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConvolveMatrixTests
{
    public class TheSetRowMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenYTooLow()
            => TestThrowsException(-1);

        [Fact]
        public void ShouldThrowExceptionWhenYTooHigh()
            => TestThrowsException(2);

        [Fact]
        public void ShouldThrowExceptionWhenValuesIsNull()
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentNullException>("values", () => { matrix.SetRow(0, null); });
        }

        [Fact]
        public void ShouldSetRowForCorrectNumberOfValues()
        {
            var matrix = new ConvolveMatrix(3);

            matrix.SetRow(1, 6, 8, 10);
            Assert.Equal(0, matrix.GetValue(0, 0));
            Assert.Equal(6, matrix.GetValue(0, 1));
            Assert.Equal(0, matrix.GetValue(0, 2));
            Assert.Equal(0, matrix.GetValue(1, 0));
            Assert.Equal(8, matrix.GetValue(1, 1));
            Assert.Equal(0, matrix.GetValue(1, 2));
            Assert.Equal(0, matrix.GetValue(2, 0));
            Assert.Equal(10, matrix.GetValue(2, 1));
            Assert.Equal(0, matrix.GetValue(2, 2));
        }

        [Fact]
        public void ShouldThrowExceptionForInvalidNumberOfValues()
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentException>("values", () => { matrix.SetRow(0, 1, 2, 3); });
        }

        private static void TestThrowsException(int y)
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentOutOfRangeException>("y", () => { matrix.SetRow(y, 1.0, 2.0); });
        }
    }
}
