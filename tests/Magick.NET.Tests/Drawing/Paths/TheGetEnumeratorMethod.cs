// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class PathsTests
{
    public class TheGetEnumeratorMethod
    {
        [Fact]
        public void ShouldReturnAllPathsThatWereAdded()
        {
            using var image = new MagickImage();
            var paths = new Paths()
                .ArcAbs(new PathArc())
                .ArcRel(new PathArc())
                .Close()
                .CurveToAbs(1.0, 2.0, 3.0, 4.0, 5.0, 6.0)
                .CurveToRel(1.0, 2.0, 3.0, 4.0, 5.0, 6.0)
                .LineToAbs(1.0, 2.0)
                .LineToRel(1.0, 2.0)
                .LineToHorizontalAbs(1.0)
                .LineToHorizontalRel(1.0)
                .LineToVerticalAbs(1.0)
                .LineToVerticalRel(1.0)
                .MoveToAbs(1.0, 2.0)
                .MoveToRel(1.0, 2.0)
                .QuadraticCurveToAbs(1.0, 2.0, 3.0, 4.0)
                .QuadraticCurveToRel(1.0, 2.0, 3.0, 4.0)
                .SmoothCurveToAbs(1.0, 2.0, 3.0, 4.0)
                .SmoothCurveToRel(1.0, 2.0, 3.0, 4.0)
                .SmoothQuadraticCurveToAbs(1.0, 2.0)
                .SmoothQuadraticCurveToRel(1.0, 2.0);

            Assert.Equal(19, paths.Count());
            Assert.Equal(19, paths.Select(d => d.GetType()).Distinct().Count());
        }
    }
}
