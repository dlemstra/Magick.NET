// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public class PathTests
{
    [Fact]
    public void Test_Paths()
    {
        var paths = new List<IPath>
        {
            new PathArcAbs(new PathArc(50, 50, 20, 20, 45, true, false)),
            new PathArcAbs(new PathArc[] { new PathArc(50, 50, 20, 20, 45, true, false) }.ToList()),
            new PathArcRel(new PathArc(10, 10, 5, 5, 40, false, true)),
            new PathArcRel(new PathArc[] { new PathArc(10, 10, 5, 5, 40, false, true) }.ToList()),
            new PathClose(),
            new PathCurveToAbs(80, 80, 10, 10, 60, 60),
            new PathCurveToRel(30, 30, 60, 60, 90, 90),
            new PathLineToAbs(new PointD(70, 70)),
            new PathLineToAbs(new PointD[] { new PointD(70, 70) }.ToList()),
            new PathLineToHorizontalAbs(20),
            new PathLineToHorizontalRel(90),
            new PathLineToRel(new PointD(0, 0)),
            new PathLineToRel(new PointD[] { new PointD(0, 0) }.ToList()),
            new PathLineToVerticalAbs(70),
            new PathLineToVerticalRel(30),
            new PathMoveToAbs(new PointD(50, 50)),
            new PathMoveToAbs(new PointD(50, 50)),
            new PathMoveToRel(new PointD(20, 20)),
            new PathMoveToRel(20, 20),
            new PathQuadraticCurveToAbs(70, 70, 30, 30),
            new PathQuadraticCurveToRel(10, 10, 40, 40),
            new PathSmoothCurveToAbs(0, 0, 30, 30),
            new PathSmoothCurveToAbs(new PointD(0, 0), new PointD(30, 30)),
            new PathSmoothCurveToRel(60, 60, 10, 10),
            new PathSmoothCurveToRel(new PointD(60, 60), new PointD(10, 10)),
            new PathSmoothQuadraticCurveToAbs(50, 50),
            new PathSmoothQuadraticCurveToRel(80, 80),
        };

        using var image = new MagickImage(MagickColors.Transparent, 100, 100);
        image.Draw(new DrawablePath(paths));
    }

    [Fact]
    public void Test_Paths_Draw()
    {
        AssertDraw(new PathArcAbs(new PathArc(50, 50, 20, 20, 45, true, false)));
        AssertDraw(new PathArcRel(new PathArc()));
        AssertDraw(new PathClose());
        AssertDraw(new PathCurveToAbs(80, 80, 10, 10, 60, 60));
        AssertDraw(new PathCurveToRel(30, 30, 60, 60, 90, 90));
        AssertDraw(new PathLineToAbs(new PointD(70, 70)));
        AssertDraw(new PathLineToHorizontalAbs(20));
        AssertDraw(new PathLineToHorizontalRel(90));
        AssertDraw(new PathLineToRel(new PointD(0, 0)));
        AssertDraw(new PathLineToVerticalAbs(70));
        AssertDraw(new PathLineToVerticalRel(30));
        AssertDraw(new PathMoveToAbs(new PointD(50, 50)));
        AssertDraw(new PathMoveToRel(new PointD(20, 20)));
        AssertDraw(new PathQuadraticCurveToAbs(70, 70, 30, 30));
        AssertDraw(new PathQuadraticCurveToRel(10, 10, 40, 40));
        AssertDraw(new PathSmoothCurveToAbs(new PointD(0, 0), new PointD(30, 30)));
        AssertDraw(new PathSmoothCurveToRel(new PointD(60, 60), new PointD(10, 10)));
        AssertDraw(new PathSmoothQuadraticCurveToAbs(50, 50));
        AssertDraw(new PathSmoothQuadraticCurveToRel(80, 80));
    }

    [Fact]
    public void Test_Path_Exceptions()
    {
        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new PathArcAbs();
        });

        Assert.Throws<ArgumentNullException>("coordinates", () =>
        {
            new PathArcAbs(null);
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new PathArcAbs(Array.Empty<PathArc>());
        });

        Assert.Throws<ArgumentNullException>("coordinates", () =>
        {
            new PathArcAbs(new PathArc[] { null });
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new PathArcRel();
        });

        Assert.Throws<ArgumentNullException>("coordinates", () =>
        {
            new PathArcRel(null);
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new PathArcRel(Array.Empty<PathArc>());
        });

        Assert.Throws<ArgumentNullException>("coordinates", () =>
        {
            new PathArcRel(new PathArc[] { null });
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new PathLineToAbs();
        });

        Assert.Throws<ArgumentNullException>("coordinates", () =>
        {
            new PathLineToAbs(null);
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new PathLineToAbs(Array.Empty<PointD>());
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new PathLineToRel();
        });

        Assert.Throws<ArgumentNullException>("coordinates", () =>
        {
            new PathLineToRel(null);
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new PathLineToRel(Array.Empty<PointD>());
        });
    }

    private void AssertDraw(IPath path)
    {
        ((IDrawingWand)path).Draw(null);
    }
}
