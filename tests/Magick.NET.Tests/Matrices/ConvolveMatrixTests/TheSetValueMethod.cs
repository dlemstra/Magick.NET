// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConvolveMatrixTests
{
    public class TheSetValueMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenXTooLow()
        {
            TestThrowsException("x", -1, 0);
        }

        [Fact]
        public void ShouldThrowExceptionWhenXTooHigh()
        {
            TestThrowsException("x", 1, 0);
        }

        [Fact]
        public void ShouldThrowExceptionWhenYTooLow()
        {
            TestThrowsException("y", 0, -1);
        }

        [Fact]
        public void ShouldThrowExceptionWhenYTooHigh()
        {
            TestThrowsException("y", 0, 1);
        }

        [Fact]
        public void ShouldSetTheValues()
        {
            var matrix = new ConvolveMatrix(3);

            matrix.SetValue(1, 2, 1.5);

            Assert.Equal(0.0, matrix.GetValue(0, 0));
            Assert.Equal(0.0, matrix.GetValue(0, 1));
            Assert.Equal(0.0, matrix.GetValue(0, 2));
            Assert.Equal(0.0, matrix.GetValue(1, 0));
            Assert.Equal(0.0, matrix.GetValue(1, 1));
            Assert.Equal(1.5, matrix.GetValue(1, 2));
            Assert.Equal(0.0, matrix.GetValue(2, 0));
            Assert.Equal(0.0, matrix.GetValue(2, 1));
            Assert.Equal(0.0, matrix.GetValue(2, 2));
        }

        private static void TestThrowsException(string paramName, int x, int y)
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentOutOfRangeException>(paramName, () =>
            {
                matrix.SetValue(x, y, 1);
            });
        }
    }
}
