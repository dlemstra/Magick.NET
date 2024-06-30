// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <content />
internal partial class DrawingWand
{
    private readonly NativeDrawingWand _nativeInstance;

    [NativeInterop]
    private partial class NativeDrawingWand : NativeInstance
    {
        public static partial NativeDrawingWand Create(IMagickImage image, DrawingSettings settings);

        [Throws]
        [ReturnsVoid]
        public partial void Affine(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY);

        [Throws]
        [ReturnsVoid]
        public partial void Alpha(double x, double y, PaintMethod paintMethod);

        [Throws]
        [ReturnsVoid]
        public partial void Arc(double startX, double startY, double endX, double endY, double startDegrees, double endDegrees);

        [Throws]
        [ReturnsVoid]
        public partial void Bezier(PointInfoCollection coordinates, nuint length);

        [Throws]
        [ReturnsVoid]
        public partial void BorderColor(IMagickColor<QuantumType>? value);

        [Throws]
        [ReturnsVoid]
        public partial void Circle(double originX, double originY, double perimeterX, double perimeterY);

        [Throws]
        [ReturnsVoid]
        public partial void ClipPath(string value);

        [Throws]
        [ReturnsVoid]
        public partial void ClipRule(FillRule value);

        [Throws]
        [ReturnsVoid]
        public partial void ClipUnits(ClipPathUnit value);

        [Throws]
        [ReturnsVoid]
        public partial void Color(double x, double y, PaintMethod paintMethod);

        [Throws]
        [ReturnsVoid]
        public partial void Composite(double x, double y, double width, double height, CompositeOperator compositeOperator, IMagickImage image);

        [Throws]
        [ReturnsVoid]
        public partial void Density(string value);

        [Throws]
        [ReturnsVoid]
        public partial void Ellipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees);

        [Throws]
        [ReturnsVoid]
        public partial void FillColor(IMagickColor<QuantumType> value);

        [Throws]
        [ReturnsVoid]
        public partial void FillOpacity(double value);

        [Throws]
        [ReturnsVoid]
        public partial void FillPatternUrl(string url);

        [Throws]
        [ReturnsVoid]
        public partial void FillRule(FillRule value);

        [Throws]
        [ReturnsVoid]
        public partial void Font(string fontName);

        [Throws]
        [ReturnsVoid]
        public partial void FontFamily(string family, FontStyleType style, FontWeight weight, FontStretch stretch);

        [Throws]
        [ReturnsVoid]
        public partial void FontPointSize(double value);

        [Throws]
        [Cleanup(Name = "TypeMetric.Dispose")]
        public partial TypeMetric? FontTypeMetrics(string text, bool ignoreNewLines);

        [Throws]
        [ReturnsVoid]
        public partial void Gravity(Gravity value);

        [Throws]
        [ReturnsVoid]
        public partial void Line(double startX, double startY, double endX, double endY);

        [Throws]
        [ReturnsVoid]
        public partial void PathArcAbs(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc, bool useSweep);

        [Throws]
        [ReturnsVoid]
        public partial void PathArcRel(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc, bool useSweep);

        [Throws]
        [ReturnsVoid]
        public partial void PathClose();

        [Throws]
        [ReturnsVoid]
        public partial void PathCurveToAbs(double x1, double y1, double x2, double y2, double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathCurveToRel(double x1, double y1, double x2, double y2, double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathFinish();

        [Throws]
        [ReturnsVoid]
        public partial void PathLineToAbs(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathLineToRel(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathLineToHorizontalAbs(double x);

        [Throws]
        [ReturnsVoid]
        public partial void PathLineToHorizontalRel(double x);

        [Throws]
        [ReturnsVoid]
        public partial void PathLineToVerticalAbs(double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathLineToVerticalRel(double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathMoveToAbs(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathMoveToRel(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathQuadraticCurveToAbs(double x1, double y1, double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathQuadraticCurveToRel(double x1, double y1, double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathSmoothCurveToAbs(double x2, double y2, double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathSmoothCurveToRel(double x2, double y2, double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathSmoothQuadraticCurveToAbs(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathSmoothQuadraticCurveToRel(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void PathStart();

        [Throws]
        [ReturnsVoid]
        public partial void Point(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void Polygon(PointInfoCollection coordinates, nuint length);

        [Throws]
        [ReturnsVoid]
        public partial void Polyline(PointInfoCollection coordinates, nuint length);

        [Throws]
        [ReturnsVoid]
        public partial void PopClipPath();

        [Throws]
        [ReturnsVoid]
        public partial void PopGraphicContext();

        [Throws]
        [ReturnsVoid]
        public partial void PopPattern();

        [Throws]
        [ReturnsVoid]
        public partial void PushClipPath(string clipPath);

        [Throws]
        [ReturnsVoid]
        public partial void PushGraphicContext();

        [Throws]
        [ReturnsVoid]
        public partial void PushPattern(string id, double x, double y, double width, double height);

        [Throws]
        [ReturnsVoid]
        public partial void Rectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY);

        [Throws]
        [ReturnsVoid]
        public partial void Render();

        [Throws]
        [ReturnsVoid]
        public partial void Rotation(double angle);

        [Throws]
        [ReturnsVoid]
        public partial void RoundRectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY, double cornerWidth, double cornerHeight);

        [Throws]
        [ReturnsVoid]
        public partial void Scaling(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void SkewX(double angle);

        [Throws]
        [ReturnsVoid]
        public partial void SkewY(double angle);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeAntialias(bool isEnabled);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeColor(IMagickColor<QuantumType>? value);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeDashArray(double[] dash, nuint length);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeDashOffset(double value);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeLineCap(LineCap value);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeLineJoin(LineJoin value);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeMiterLimit(nuint value);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeOpacity(double value);

        [Throws]
        [ReturnsVoid]
        public partial void StrokePatternUrl(string value);

        [Throws]
        [ReturnsVoid]
        public partial void StrokeWidth(double value);

        [Throws]
        [ReturnsVoid]
        public partial void Text(double x, double y, string text);

        [Throws]
        [ReturnsVoid]
        public partial void TextAlignment(TextAlignment value);

        [Throws]
        [ReturnsVoid]
        public partial void TextAntialias(bool isEnabled);

        [Throws]
        [ReturnsVoid]
        public partial void TextDecoration(TextDecoration value);

        [Throws]
        [ReturnsVoid]
        public partial void TextDirection(TextDirection value);

        [Throws]
        [ReturnsVoid]
        public partial void TextEncoding(string encoding);

        [Throws]
        [ReturnsVoid]
        public partial void TextInterlineSpacing(double value);

        [Throws]
        [ReturnsVoid]
        public partial void TextInterwordSpacing(double value);

        [Throws]
        [ReturnsVoid]
        public partial void TextKerning(double value);

        [Throws]
        [ReturnsVoid]
        public partial void TextUnderColor(IMagickColor<QuantumType>? color);

        [Throws]
        [ReturnsVoid]
        public partial void Translation(double x, double y);

        [Throws]
        [ReturnsVoid]
        public partial void Viewbox(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY);
    }
}
