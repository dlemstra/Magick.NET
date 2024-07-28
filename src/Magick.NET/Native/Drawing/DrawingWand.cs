// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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

namespace ImageMagick.Drawing;

/// <content />
internal partial class DrawingWand
{
    private readonly NativeDrawingWand _nativeInstance;

    [NativeInterop]
    private partial class NativeDrawingWand : NativeInstance
    {
        public static partial NativeDrawingWand Create(IMagickImage image, DrawingSettings settings);

        [Throws]
        public partial void Affine(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY);

        [Throws]
        public partial void Alpha(double x, double y, PaintMethod paintMethod);

        [Throws]
        public partial void Arc(double startX, double startY, double endX, double endY, double startDegrees, double endDegrees);

        [Throws]
        public partial void Bezier(PointInfoCollection coordinates, nuint length);

        [Throws]
        public partial void BorderColor(IMagickColor<QuantumType>? value);

        [Throws]
        public partial void Circle(double originX, double originY, double perimeterX, double perimeterY);

        [Throws]
        public partial void ClipPath(string value);

        [Throws]
        public partial void ClipRule(FillRule value);

        [Throws]
        public partial void ClipUnits(ClipPathUnit value);

        [Throws]
        public partial void Color(double x, double y, PaintMethod paintMethod);

        [Throws]
        public partial void Composite(double x, double y, double width, double height, CompositeOperator compositeOperator, IMagickImage image);

        [Throws]
        public partial void Density(string value);

        [Throws]
        public partial void Ellipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees);

        [Throws]
        public partial void FillColor(IMagickColor<QuantumType> value);

        [Throws]
        public partial void FillOpacity(double value);

        [Throws]
        public partial void FillPatternUrl(string url);

        [Throws]
        public partial void FillRule(FillRule value);

        [Throws]
        public partial void Font(string fontName);

        [Throws]
        public partial void FontFamily(string family, FontStyleType style, FontWeight weight, FontStretch stretch);

        [Throws]
        public partial void FontPointSize(double value);

        [Throws]
        [Cleanup(Name = "TypeMetric.Dispose")]
        public partial TypeMetric FontTypeMetrics(string text, bool ignoreNewLines);

        [Throws]
        public partial void Gravity(Gravity value);

        [Throws]
        public partial void Line(double startX, double startY, double endX, double endY);

        [Throws]
        public partial void PathArcAbs(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc, bool useSweep);

        [Throws]
        public partial void PathArcRel(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc, bool useSweep);

        [Throws]
        public partial void PathClose();

        [Throws]
        public partial void PathCurveToAbs(double x1, double y1, double x2, double y2, double x, double y);

        [Throws]
        public partial void PathCurveToRel(double x1, double y1, double x2, double y2, double x, double y);

        [Throws]
        public partial void PathFinish();

        [Throws]
        public partial void PathLineToAbs(double x, double y);

        [Throws]
        public partial void PathLineToRel(double x, double y);

        [Throws]
        public partial void PathLineToHorizontalAbs(double x);

        [Throws]
        public partial void PathLineToHorizontalRel(double x);

        [Throws]
        public partial void PathLineToVerticalAbs(double y);

        [Throws]
        public partial void PathLineToVerticalRel(double y);

        [Throws]
        public partial void PathMoveToAbs(double x, double y);

        [Throws]
        public partial void PathMoveToRel(double x, double y);

        [Throws]
        public partial void PathQuadraticCurveToAbs(double x1, double y1, double x, double y);

        [Throws]
        public partial void PathQuadraticCurveToRel(double x1, double y1, double x, double y);

        [Throws]
        public partial void PathSmoothCurveToAbs(double x2, double y2, double x, double y);

        [Throws]
        public partial void PathSmoothCurveToRel(double x2, double y2, double x, double y);

        [Throws]
        public partial void PathSmoothQuadraticCurveToAbs(double x, double y);

        [Throws]
        public partial void PathSmoothQuadraticCurveToRel(double x, double y);

        [Throws]
        public partial void PathStart();

        [Throws]
        public partial void Point(double x, double y);

        [Throws]
        public partial void Polygon(PointInfoCollection coordinates, nuint length);

        [Throws]
        public partial void Polyline(PointInfoCollection coordinates, nuint length);

        [Throws]
        public partial void PopClipPath();

        [Throws]
        public partial void PopGraphicContext();

        [Throws]
        public partial void PopPattern();

        [Throws]
        public partial void PushClipPath(string clipPath);

        [Throws]
        public partial void PushGraphicContext();

        [Throws]
        public partial void PushPattern(string id, double x, double y, double width, double height);

        [Throws]
        public partial void Rectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY);

        [Throws]
        public partial void Render();

        [Throws]
        public partial void Rotation(double angle);

        [Throws]
        public partial void RoundRectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY, double cornerWidth, double cornerHeight);

        [Throws]
        public partial void Scaling(double x, double y);

        [Throws]
        public partial void SkewX(double angle);

        [Throws]
        public partial void SkewY(double angle);

        [Throws]
        public partial void StrokeAntialias(bool isEnabled);

        [Throws]
        public partial void StrokeColor(IMagickColor<QuantumType>? value);

        [Throws]
        public partial void StrokeDashArray(double[] dash, nuint length);

        [Throws]
        public partial void StrokeDashOffset(double value);

        [Throws]
        public partial void StrokeLineCap(LineCap value);

        [Throws]
        public partial void StrokeLineJoin(LineJoin value);

        [Throws]
        public partial void StrokeMiterLimit(nuint value);

        [Throws]
        public partial void StrokeOpacity(double value);

        [Throws]
        public partial void StrokePatternUrl(string value);

        [Throws]
        public partial void StrokeWidth(double value);

        [Throws]
        public partial void Text(double x, double y, string text);

        [Throws]
        public partial void TextAlignment(TextAlignment value);

        [Throws]
        public partial void TextAntialias(bool isEnabled);

        [Throws]
        public partial void TextDecoration(TextDecoration value);

        [Throws]
        public partial void TextDirection(TextDirection value);

        [Throws]
        public partial void TextEncoding(string encoding);

        [Throws]
        public partial void TextInterlineSpacing(double value);

        [Throws]
        public partial void TextInterwordSpacing(double value);

        [Throws]
        public partial void TextKerning(double value);

        [Throws]
        public partial void TextUnderColor(IMagickColor<QuantumType>? color);

        [Throws]
        public partial void Translation(double x, double y);

        [Throws]
        public partial void Viewbox(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY);
    }
}
