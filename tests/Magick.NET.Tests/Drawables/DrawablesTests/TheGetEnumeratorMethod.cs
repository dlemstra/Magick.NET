// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawablesTests
{
    public class TheGetEnumeratorMethod
    {
        [Fact]
        public void ShouldReturnAllDrawablesThatWereAdded()
        {
            using var image = new MagickImage();
            var drawables = new Drawables()
              .Affine(1.0, 1.0, 1.0, 1.0, 1.0, 1.0)
              .Alpha(1.0, 1.0, PaintMethod.Point)
              .Arc(1.0, 1.0, 1.0, 1.0, 1.0, 1.0)
              .Bezier(default, default, default)
              .Bezier((IEnumerable<PointD>)[default, default, default])
              .BorderColor(MagickColors.Purple)
              .Circle(1.0, 1.0, 1.0, 1.0)
              .ClipPath("foo")
              .ClipRule(FillRule.EvenOdd)
              .ClipUnits(ClipPathUnit.ObjectBoundingBox)
              .Color(1.0, 1.0, PaintMethod.Point)
              .Composite(new MagickGeometry(), image)
              .Composite(new MagickGeometry(), CompositeOperator.Overlay, image)
              .Density(1.0)
              .Ellipse(1.0, 1.0, 1.0, 1.0, 1.0, 1.0)
              .FillColor(MagickColors.Purple)
              .FillOpacity(new Percentage(1.0))
              .FillPatternUrl("foo")
              .FillRule(FillRule.Nonzero)
              .Font("foo")
              .Font("foo", FontStyleType.Normal, FontWeight.Normal, FontStretch.Normal)
              .FontPointSize(1.0)
              .Gravity(Gravity.Center)
              .Line(1.0, 1.0, 1.0, 1.0)
              .Path(default, default, default)
              .Path((IEnumerable<IPath>)[default, default, default])
              .Point(1.0, 1.0)
              .Polygon(default, default, default)
              .Polygon((IEnumerable<PointD>)[default, default, default])
              .Point(1.0, 1.0)
              .Polyline(default, default, default)
              .Polyline((IEnumerable<PointD>)[default, default, default])
              .PopClipPath()
              .PopPattern()
              .PushClipPath("foo")
              .PushGraphicContext()
              .PushPattern("foo", 1.0, 1.0, 1.0, 1.0)
              .Rectangle(1.0, 1.0, 1.0, 1.0)
              .Rotation(1.0)
              .RoundRectangle(1.0, 1.0, 1.0, 1.0, 1.0, 1.0)
              .Scaling(1.0, 1.0)
              .SkewX(1.0)
              .SkewY(1.0)
              .StrokeColor(MagickColors.Purple)
              .StrokeDashArray([default])
              .StrokeDashOffset(1.0)
              .StrokeLineCap(LineCap.Butt)
              .StrokeLineJoin(LineJoin.Round)
              .StrokeMiterLimit(1)
              .StrokeOpacity(new Percentage(1.0))
              .StrokePatternUrl("foo")
              .StrokeWidth(1.0)
              .Text(1.0, 1.0, "foo")
              .TextAlignment(TextAlignment.Center)
              .TextDirection(TextDirection.LeftToRight)
              .TextEncoding(Encoding.UTF8)
              .TextInterlineSpacing(1.0)
              .TextInterwordSpacing(1.0)
              .TextKerning(1.0)
              .TextUnderColor(MagickColors.Purple)
              .Translation(1.0, 1.0)
              .Viewbox(1.0, 1.0, 1.0, 1.0)
              .DisableStrokeAntialias()
              .EnableStrokeAntialias()
              .DisableTextAntialias()
              .EnableTextAntialias();

            Assert.Equal(66, drawables.Count());
            Assert.Equal(57, drawables.Select(d => d.GetType()).Distinct().Count());
        }
    }
}
