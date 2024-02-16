// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConvolveMatrixTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenOrderIsTooLow()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new ConvolveMatrix(0);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenOrderIsNotAnOddNumber()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new ConvolveMatrix(2);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenNotEnoughValuesAreProvided()
        {
            Assert.Throws<ArgumentException>("values", () =>
            {
                new ConvolveMatrix(3, 1.0);
            });
        }

        [Fact]
        public void ShouldSetTheProperties()
        {
            var matrix = new ConvolveMatrix(3, 0.0, 1.0, 2.0, 0.1, 1.1, 2.1, 0.2, 1.2, 2.2);

            Assert.Equal(3, matrix.Order);
            Assert.Equal(0.0, matrix.GetValue(0, 0));
            Assert.Equal(1.0, matrix.GetValue(1, 0));
            Assert.Equal(2.0, matrix.GetValue(2, 0));
            Assert.Equal(0.1, matrix.GetValue(0, 1));
            Assert.Equal(1.1, matrix.GetValue(1, 1));
            Assert.Equal(2.1, matrix.GetValue(2, 1));
            Assert.Equal(0.2, matrix.GetValue(0, 2));
            Assert.Equal(1.2, matrix.GetValue(1, 2));
            Assert.Equal(2.2, matrix.GetValue(2, 2));
        }

        [Fact]
        public void ShouldThrowExceptionWhenOrderIsTooLowAndValuesAreProvided()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new ConvolveMatrix(0, 1);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenOrderIsNotAnOddNumberAndValuesAreProvided()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new ConvolveMatrix(2, 1, 2, 3, 4);
            });
        }
    }
}
