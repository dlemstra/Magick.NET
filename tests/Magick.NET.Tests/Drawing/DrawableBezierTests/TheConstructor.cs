// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableBezierTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldSetPathsToEmptyCollection()
        {
            PointD[] coordinates = [new PointD(0, 0), new PointD(50, 50), new PointD(99, 99)];

            var bezier = new DrawableBezier(coordinates.ToList());
            Assert.Equal(3, bezier.Coordinates.Count);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCoordinatesAreNotSpecified()
        {
            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new DrawableBezier();
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenCoordinatesAreNull()
        {
            Assert.Throws<ArgumentNullException>("coordinates", () =>
            {
                new DrawableBezier(null);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenCoordinatesAreEmpty()
        {
            Assert.Throws<ArgumentException>("coordinates", () =>
            {
                new DrawableBezier([]);
            });
        }
    }
}
