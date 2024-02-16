// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickColorMatrixTests
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
            var matrix = new MagickColorMatrix(2);

            Assert.Throws<ArgumentNullException>("values", () =>
            {
                matrix.SetRow(0, null);
            });
        }

        [Fact]
        public void ShouldSetColumnForCorrectNumberOfValues()
        {
            var matrix = new MagickColorMatrix(2);

            matrix.SetRow(1, 6, 8);
            Assert.Equal(0, matrix.GetValue(0, 0));
            Assert.Equal(6, matrix.GetValue(0, 1));
            Assert.Equal(0, matrix.GetValue(1, 0));
            Assert.Equal(8, matrix.GetValue(1, 1));
        }

        [Fact]
        public void ShouldThrowExceptionForInvalidNumberOfValues()
        {
            var matrix = new MagickColorMatrix(2);

            Assert.Throws<ArgumentException>("values", () =>
            {
                matrix.SetRow(0, 1, 2, 3);
            });
        }

        private static void TestThrowsException(int y)
        {
            var matrix = new MagickColorMatrix(2);

            Assert.Throws<ArgumentOutOfRangeException>("y", () =>
            {
                matrix.SetRow(y, 1.0, 2.0);
            });
        }
    }
}
