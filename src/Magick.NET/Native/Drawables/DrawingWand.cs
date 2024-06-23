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
internal partial class DrawingWand : IDisposable
{
    private readonly NativeDrawingWand _nativeInstance;

    [NativeInterop]
    private partial class NativeDrawingWand : NativeInstance
    {
        public static partial NativeDrawingWand Create(IMagickImage image, DrawingSettings settings);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Affine(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Alpha(double x, double y, PaintMethod paintMethod);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Arc(double startX, double startY, double endX, double endY, double startDegrees, double endDegrees);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Bezier(PointInfoCollection coordinates, nuint length);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void BorderColor(IMagickColor<QuantumType>? value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Circle(double originX, double originY, double perimeterX, double perimeterY);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void ClipPath(string value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void ClipRule(FillRule value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void ClipUnits(ClipPathUnit value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Color(double x, double y, PaintMethod paintMethod);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Composite(double x, double y, double width, double height, CompositeOperator compositeOperator, IMagickImage image);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Density(string value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Ellipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void FillColor(IMagickColor<QuantumType> value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void FillOpacity(double value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void FillPatternUrl(string url);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void FillRule(FillRule value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Font(string fontName);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void FontFamily(string family, FontStyleType style, FontWeight weight, FontStretch stretch);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void FontPointSize(double value);

        [Throws]
        public partial IntPtr FontTypeMetrics(string text, bool ignoreNewLines);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Gravity(Gravity value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Line(double startX, double startY, double endX, double endY);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathArcAbs(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc, bool useSweep);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathArcRel(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc, bool useSweep);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathClose();

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathCurveToAbs(double x1, double y1, double x2, double y2, double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathCurveToRel(double x1, double y1, double x2, double y2, double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathFinish();

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathLineToAbs(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathLineToRel(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathLineToHorizontalAbs(double x);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathLineToHorizontalRel(double x);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathLineToVerticalAbs(double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathLineToVerticalRel(double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathMoveToAbs(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathMoveToRel(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathQuadraticCurveToAbs(double x1, double y1, double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathQuadraticCurveToRel(double x1, double y1, double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathSmoothCurveToAbs(double x2, double y2, double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathSmoothCurveToRel(double x2, double y2, double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathSmoothQuadraticCurveToAbs(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathSmoothQuadraticCurveToRel(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PathStart();

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Point(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Polygon(PointInfoCollection coordinates, nuint length);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Polyline(PointInfoCollection coordinates, nuint length);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PopClipPath();

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PopGraphicContext();

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PopPattern();

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PushClipPath(string clipPath);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PushGraphicContext();

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void PushPattern(string id, double x, double y, double width, double height);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Rectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Render();

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Rotation(double angle);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void RoundRectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY, double cornerWidth, double cornerHeight);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Scaling(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void SkewX(double angle);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void SkewY(double angle);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeAntialias(bool isEnabled);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeColor(IMagickColor<QuantumType>? value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeDashArray(double[] dash, nuint length);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeDashOffset(double value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeLineCap(LineCap value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeLineJoin(LineJoin value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeMiterLimit(nuint value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeOpacity(double value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokePatternUrl(string value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void StrokeWidth(double value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Text(double x, double y, string text);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextAlignment(TextAlignment value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextAntialias(bool isEnabled);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextDecoration(TextDecoration value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextDirection(TextDirection value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextEncoding(string encoding);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextInterlineSpacing(double value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextInterwordSpacing(double value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextKerning(double value);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void TextUnderColor(IMagickColor<QuantumType>? color);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Translation(double x, double y);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Viewbox(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY);
    }
}
