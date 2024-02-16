// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickColorMatrixTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenOrderIsTooLow()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new MagickColorMatrix(0);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenOrderIsTooHigh()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new MagickColorMatrix(7);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenNotEnoughValuesAreProvided()
        {
            Assert.Throws<ArgumentException>("values", () =>
            {
                new MagickColorMatrix(2, 1.0);
            });
        }

        [Fact]
        public void ShouldSetTheProperties()
        {
            var matrix = new MagickColorMatrix(2, 0.0, 1.0, 0.1, 1.1);

            Assert.Equal(2, matrix.Order);
            Assert.Equal(0.0, matrix.GetValue(0, 0));
            Assert.Equal(1.0, matrix.GetValue(1, 0));
            Assert.Equal(0.1, matrix.GetValue(0, 1));
            Assert.Equal(1.1, matrix.GetValue(1, 1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenOrderIsTooLowAndValuesAreProvided()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new MagickColorMatrix(0, 1);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenOrderIsTooHighAndValuesAreProvided()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                var values = Enumerable.Repeat(1.0, 7 * 7).ToArray();

                new MagickColorMatrix(7, values);
            });
        }
    }
}
