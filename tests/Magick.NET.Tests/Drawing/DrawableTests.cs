// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class DrawableTests
{
    [Fact]
    public void Test_Drawables()
    {
        var coordinates = new PointD[3];
        coordinates[0] = new PointD(0, 0);
        coordinates[1] = new PointD(50, 50);
        coordinates[2] = new PointD(99, 99);

        using var image = new MagickImage(MagickColors.Transparent, 100, 100);

        image.Draw(new DrawableAffine(0, 0, 1, 1, 2, 2));
        image.Draw(new DrawableAlpha(0, 0, PaintMethod.Floodfill));
        image.Draw(new DrawableArc(0, 0, 10, 10, 45, 90));

        image.Draw(new DrawableBorderColor(MagickColors.Fuchsia));
        image.Draw(new DrawableCircle(0, 0, 50, 50));
        image.Draw(new DrawableClipPath("foo"));
        image.Draw(new DrawableClipRule(FillRule.Nonzero));
        image.Draw(new DrawableClipUnits(ClipPathUnit.UserSpaceOnUse));
        image.Draw(new DrawableColor(0, 0, PaintMethod.Floodfill));

        using var compositeImage = new MagickImage(new MagickColor("red"), 50, 50);

#pragma warning disable CS0618 // Type or member is obsolete
        image.Draw(new DrawableComposite(0, 0, compositeImage));
        image.Draw(new DrawableComposite(new MagickGeometry(50, 50, 10, 10), compositeImage));
        image.Draw(new DrawableComposite(new MagickGeometry(50, 50, 10, 10), CompositeOperator.Over, compositeImage));
#pragma warning restore CS0618 // Type or member is obsolete
        image.Draw(new DrawableComposite(0, 0, CompositeOperator.Over, compositeImage));
        image.Draw(new DrawableComposite(50, 50, 10, 10, CompositeOperator.Over, compositeImage));

        image.Draw(new DrawableDensity(97));
        image.Draw(new DrawableEllipse(10, 10, 4, 4, 0, 360));
        image.Draw(new DrawableFillColor(MagickColors.Red));
        image.Draw(new DrawableFillOpacity(new Percentage(50)));
        image.Draw(new DrawableFillRule(FillRule.EvenOdd));
        image.Draw(new DrawableFont("Arial.ttf"));
        image.Draw(new DrawableFont("Arial"));
        image.Draw(new DrawableGravity(Gravity.Center));
        image.Draw(new DrawableLine(20, 20, 40, 40));
        image.Draw(new DrawablePoint(60, 60));
        image.Draw(new DrawableFontPointSize(5));
        image.Draw(new DrawablePolygon(coordinates));
        image.Draw(new DrawablePolygon(coordinates.ToList()));
        image.Draw(new DrawablePolyline(coordinates));
        image.Draw(new DrawablePolyline(coordinates.ToList()));
        image.Draw(new DrawableRectangle(30, 30, 70, 70));
        image.Draw(new DrawableRotation(180));
        image.Draw(new DrawableRoundRectangle(30, 30, 50, 50, 70, 70));
        image.Draw(new DrawableScaling(15, 15));
        image.Draw(new DrawableSkewX(90));
        image.Draw(new DrawableSkewY(90));
        image.Draw(new DrawableStrokeColor(MagickColors.Purple));
        image.Draw(new DrawableStrokeDashArray(new double[2] { 10, 20 }));
        image.Draw(new DrawableStrokeDashOffset(2));
        image.Draw(new DrawableStrokeLineCap(LineCap.Square));
        image.Draw(new DrawableStrokeLineJoin(LineJoin.Bevel));
        image.Draw(new DrawableStrokeMiterLimit(5));
        image.Draw(new DrawableStrokeOpacity(new Percentage(80)));
        image.Draw(new DrawableStrokeWidth(4));
        image.Draw(new DrawableText(0, 60, "test"));
        image.Draw(new DrawableTextAlignment(TextAlignment.Center));
        image.Draw(new DrawableTextDecoration(TextDecoration.LineThrough));
        image.Draw(new DrawableTextDirection(TextDirection.RightToLeft));
        image.Draw(new DrawableTextEncoding(Encoding.ASCII));
        image.Draw(new DrawableTextInterlineSpacing(4));
        image.Draw(new DrawableTextInterwordSpacing(6));
        image.Draw(new DrawableTextKerning(2));
        image.Draw(new DrawableTextUnderColor(MagickColors.Yellow));
        image.Draw(new DrawableTranslation(65, 65));
        image.Draw(new DrawableViewbox(0, 0, 100, 100));

        image.Draw(new DrawablePushClipPath("#1"), new DrawablePopClipPath());
        image.Draw(new DrawablePushGraphicContext(), new DrawablePopGraphicContext());
        image.Draw(new DrawablePushPattern("test", 30, 30, 10, 10), new DrawablePopPattern(), new DrawableFillPatternUrl("#test"), new DrawableStrokePatternUrl("#test"));
    }

    [Fact]
    public void Test_Drawables_Draw()
    {
        var coordinates = new PointD[3];
        coordinates[0] = new PointD(0, 0);
        coordinates[1] = new PointD(50, 50);
        coordinates[2] = new PointD(99, 99);

        AssertDraw(new DrawableAffine(0, 0, 1, 1, 2, 2));
        AssertDraw(new DrawableAlpha(0, 0, PaintMethod.Floodfill));
        AssertDraw(new DrawableArc(0, 0, 10, 10, 45, 90));
        AssertDraw(new DrawableBezier(coordinates));
        AssertDraw(new DrawableBorderColor(MagickColors.Fuchsia));
        AssertDraw(new DrawableCircle(0, 0, 50, 50));
        AssertDraw(new DrawableClipPath("foo"));
        AssertDraw(new DrawableClipRule(FillRule.Nonzero));
        AssertDraw(new DrawableClipUnits(ClipPathUnit.UserSpaceOnUse));
        AssertDraw(new DrawableColor(0, 0, PaintMethod.Floodfill));

        using var compositeImage = new MagickImage(new MagickColor("red"), 50, 50);

#pragma warning disable CS0618 // Type or member is obsolete
        AssertDraw(new DrawableComposite(0, 0, compositeImage));
        AssertDraw(new DrawableComposite(new MagickGeometry(50, 50, 10, 10), compositeImage));
        AssertDraw(new DrawableComposite(new MagickGeometry(50, 50, 10, 10), CompositeOperator.Over, compositeImage));
#pragma warning restore CS0618 // Type or member is obsolete
        AssertDraw(new DrawableComposite(0, 0, CompositeOperator.Over, compositeImage));
        AssertDraw(new DrawableComposite(50, 50, 10, 10, CompositeOperator.Over, compositeImage));

        AssertDraw(new DrawableDensity(97));
        AssertDraw(new DrawableEllipse(10, 10, 4, 4, 0, 360));
        AssertDraw(new DrawableFillColor(MagickColors.Red));
        AssertDraw(new DrawableFillOpacity(new Percentage(50)));
        AssertDraw(new DrawableFillRule(FillRule.EvenOdd));
        AssertDraw(new DrawableFont("Arial"));
        AssertDraw(new DrawableGravity(Gravity.Center));
        AssertDraw(new DrawableLine(20, 20, 40, 40));
        AssertDraw(new DrawablePoint(60, 60));
        AssertDraw(new DrawableFontPointSize(5));
        AssertDraw(new DrawablePolygon(coordinates));
        AssertDraw(new DrawablePolyline(coordinates));
        AssertDraw(new DrawableRectangle(30, 30, 70, 70));
        AssertDraw(new DrawableRotation(180));
        AssertDraw(new DrawableRoundRectangle(30, 30, 50, 50, 70, 70));
        AssertDraw(new DrawableScaling(15, 15));
        AssertDraw(new DrawableSkewX(90));
        AssertDraw(new DrawableSkewY(90));
        AssertDraw(new DrawableStrokeColor(MagickColors.Purple));
        AssertDraw(new DrawableStrokeDashArray(new double[2] { 10, 20 }));
        AssertDraw(new DrawableStrokeDashOffset(2));
        AssertDraw(new DrawableStrokeLineCap(LineCap.Square));
        AssertDraw(new DrawableStrokeLineJoin(LineJoin.Bevel));
        AssertDraw(new DrawableStrokeMiterLimit(5));
        AssertDraw(new DrawableStrokeOpacity(new Percentage(80)));
        AssertDraw(new DrawableStrokeWidth(4));
        AssertDraw(new DrawableText(0, 60, "test"));
        AssertDraw(new DrawableTextAlignment(TextAlignment.Center));
        AssertDraw(new DrawableTextDecoration(TextDecoration.LineThrough));
        AssertDraw(new DrawableTextDirection(TextDirection.RightToLeft));
        AssertDraw(new DrawableTextEncoding(Encoding.ASCII));
        AssertDraw(new DrawableTextInterlineSpacing(4));
        AssertDraw(new DrawableTextInterwordSpacing(6));
        AssertDraw(new DrawableTextKerning(2));
        AssertDraw(new DrawableTextUnderColor(MagickColors.Yellow));
        AssertDraw(new DrawableTranslation(65, 65));
        AssertDraw(new DrawableViewbox(0, 0, 100, 100));

        AssertDraw(new DrawablePushClipPath("#1"));
        AssertDraw(new DrawablePopClipPath());
        AssertDraw(new DrawablePushGraphicContext());
        AssertDraw(new DrawablePopGraphicContext());
        AssertDraw(new DrawablePushPattern("test", 30, 30, 10, 10));
        AssertDraw(new DrawablePopPattern());
        AssertDraw(new DrawableFillPatternUrl("#test"));
        AssertDraw(new DrawableStrokePatternUrl("#test"));

        using var image = new MagickImage(MagickColors.Red, 10, 10);

        var yellow = MagickColors.Yellow;
        image.Draw(new DrawableFillColor(yellow), new DrawableRectangle(0, 0, 10, 10));
        ColorAssert.Equal(yellow, image, 5, 5);
    }

    [Fact]
    public void Test_Drawables_Exceptions()
    {
        Assert.Throws<ArgumentNullException>("clipPath", () =>
        {
            new DrawableClipPath(null);
        });

        Assert.Throws<ArgumentException>("clipPath", () =>
        {
            new DrawableClipPath(string.Empty);
        });

        Assert.Throws<ArgumentNullException>("image", () =>
        {
            new DrawableComposite(1, 2, 3, 4, CompositeOperator.In, null);
        });

        Assert.Throws<ArgumentNullException>("color", () =>
        {
            new DrawableFillColor(null);
        });

        Assert.Throws<ArgumentNullException>("family", () =>
        {
            new DrawableFont(null);
        });

        Assert.Throws<ArgumentException>("family", () =>
        {
            new DrawableFont(string.Empty);
        });

        Assert.Throws<MagickDrawErrorException>(() =>
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            image.Draw(new DrawableFillPatternUrl("#fail"));
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new DrawablePolygon(new PointD[] { new PointD(0, 0) });
        });

        Assert.Throws<ArgumentException>("coordinates", () =>
        {
            new DrawablePolyline(new PointD[] { new PointD(0, 0), new PointD(0, 0) });
        });

        Assert.Throws<ArgumentNullException>("color", () =>
        {
            new DrawableStrokeColor(null);
        });

        Assert.Throws<ArgumentNullException>("value", () =>
        {
            new DrawableText(0, 0, null);
        });

        Assert.Throws<ArgumentException>("value", () =>
        {
            new DrawableText(0, 0, string.Empty);
        });

        Assert.Throws<ArgumentNullException>("encoding", () =>
        {
            new DrawableTextEncoding(null);
        });
    }

    private void AssertDraw(IDrawable drawable)
    {
        ((IDrawingWand)drawable).Draw(null);
    }
}
