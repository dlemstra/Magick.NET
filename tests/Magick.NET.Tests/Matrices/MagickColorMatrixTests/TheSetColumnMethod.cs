// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickColorMatrixTests
{
    public class TheSetColumnMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenXTooLow()
            => TestThrowsException(-1);

        [Fact]
        public void ShouldThrowExceptionWhenXTooHigh()
            => TestThrowsException(2);

        [Fact]
        public void ShouldThrowExceptionWhenValuesIsNull()
        {
            var matrix = new MagickColorMatrix(2);

            Assert.Throws<ArgumentNullException>("values", () => { matrix.SetColumn(0, null!); });
        }

        [Fact]
        public void ShouldSetColumnForCorrectNumberOfValues()
        {
            var matrix = new MagickColorMatrix(2);

            matrix.SetColumn(1, 6, 8);
            Assert.Equal(0, matrix.GetValue(0, 0));
            Assert.Equal(0, matrix.GetValue(0, 1));
            Assert.Equal(6, matrix.GetValue(1, 0));
            Assert.Equal(8, matrix.GetValue(1, 1));
        }

        [Fact]
        public void ShouldThrowExceptionForInvalidNumberOfValues()
        {
            var matrix = new MagickColorMatrix(2);

            Assert.Throws<ArgumentException>("values", () =>
            {
                matrix.SetColumn(0, 1, 2, 3);
            });
        }

        private static void TestThrowsException(int x)
        {
            var matrix = new MagickColorMatrix(2);

            Assert.Throws<ArgumentOutOfRangeException>("x", () =>
            {
                matrix.SetColumn(x, 1.0, 2.0);
            });
        }
    }
}
