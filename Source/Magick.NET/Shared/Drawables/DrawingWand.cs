// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ImageMagick
{
    internal sealed partial class DrawingWand : IDisposable
    {
        public DrawingWand(IMagickImage image)
        {
            _nativeInstance = new NativeDrawingWand(image, image.Settings.Drawing);
        }

        public void Draw(IEnumerable<IDrawable> drawables)
        {
            foreach (IDrawable drawable in drawables)
            {
                ((IDrawingWand)drawable).Draw(this);
            }

            _nativeInstance.Render();
        }

        public void Affine(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY)
        {
            _nativeInstance.Affine(scaleX, scaleY, shearX, shearY, translateX, translateY);
        }

        public void Alpha(double x, double y, PaintMethod paintMethod)
        {
            _nativeInstance.Alpha(x, y, paintMethod);
        }

        public void Arc(double startX, double startY, double endX, double endY, double startDegrees, double endDegrees)
        {
            _nativeInstance.Arc(startX, startY, endX, endY, startDegrees, endDegrees);
        }

        public void Bezier(IList<PointD> coordinates)
        {
            using (PointInfoCollection pointInfo = new PointInfoCollection(coordinates))
            {
                _nativeInstance.Bezier(pointInfo, pointInfo.Count);
            }
        }

        public void BorderColor(MagickColor color)
        {
            _nativeInstance.BorderColor(color);
        }

        public void Circle(double originX, double originY, double perimeterX, double perimeterY)
        {
            _nativeInstance.Circle(originX, originY, perimeterX, perimeterY);
        }

        public void ClipPath(string value)
        {
            _nativeInstance.ClipPath(value);
        }

        public void ClipRule(FillRule value)
        {
            _nativeInstance.ClipRule(value);
        }

        public void ClipUnits(ClipPathUnit value)
        {
            _nativeInstance.ClipUnits(value);
        }

        public void Color(double x, double y, PaintMethod paintMethod)
        {
            _nativeInstance.Color(x, y, paintMethod);
        }

        public void Composite(double x, double y, double width, double height, CompositeOperator compositeOperator, IMagickImage image)
        {
            _nativeInstance.Composite(x, y, width, height, compositeOperator, image);
        }

        public void Density(PointD value)
        {
            _nativeInstance.Density(value.ToString());
        }

        public void Dispose()
        {
            DebugThrow.IfNull(_nativeInstance);
            _nativeInstance.Dispose();
        }

        public void Ellipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees)
        {
            _nativeInstance.Ellipse(originX, originY, radiusX, radiusY, startDegrees, endDegrees);
        }

        public void FillColor(MagickColor color)
        {
            _nativeInstance.FillColor(color);
        }

        public void FillOpacity(double value)
        {
            _nativeInstance.FillOpacity(value);
        }

        public void FillPatternUrl(string url)
        {
            _nativeInstance.FillPatternUrl(url);
        }

        public void FillRule(FillRule value)
        {
            _nativeInstance.FillRule(value);
        }

        public void Font(string fontName)
        {
            _nativeInstance.Font(fontName);
        }

        public void FontFamily(string family, FontStyleType style, FontWeight weight, FontStretch stretch)
        {
            _nativeInstance.FontFamily(family, style, weight, stretch);
        }

        public void FontPointSize(double value)
        {
            _nativeInstance.FontPointSize(value);
        }

        public TypeMetric FontTypeMetrics(string text, bool ignoreNewlines)
        {
            IntPtr result = _nativeInstance.FontTypeMetrics(text, ignoreNewlines);
            return TypeMetric.CreateInstance(result);
        }

        public void Gravity(Gravity value)
        {
            _nativeInstance.Gravity(value);
        }

        public void Line(double startX, double startY, double endX, double endY)
        {
            _nativeInstance.Line(startX, startY, endX, endY);
        }

        public void PathArcAbs(IEnumerable<PathArc> pathArcs)
        {
            DebugThrow.IfNull(nameof(pathArcs), pathArcs);

            foreach (PathArc pathArc in pathArcs)
            {
                _nativeInstance.PathArcAbs(pathArc.X, pathArc.Y, pathArc.RadiusX, pathArc.RadiusY, pathArc.RotationX, pathArc.UseLargeArc, pathArc.UseSweep);
            }
        }

        public void PathArcRel(IEnumerable<PathArc> pathArcs)
        {
            DebugThrow.IfNull(nameof(pathArcs), pathArcs);

            foreach (PathArc pathArc in pathArcs)
            {
                _nativeInstance.PathArcRel(pathArc.X, pathArc.Y, pathArc.RadiusX, pathArc.RadiusY, pathArc.RotationX, pathArc.UseLargeArc, pathArc.UseSweep);
            }
        }

        public void PathClose()
        {
            _nativeInstance.PathClose();
        }

        public void PathCurveToAbs(PointD controlPointStart, PointD controlPointEnd, PointD endPoint)
        {
            _nativeInstance.PathCurveToAbs(controlPointStart.X, controlPointStart.Y, controlPointEnd.X, controlPointEnd.Y, endPoint.X, endPoint.Y);
        }

        public void PathCurveToRel(PointD controlPointStart, PointD controlPointEnd, PointD endPoint)
        {
            _nativeInstance.PathCurveToRel(controlPointStart.X, controlPointStart.Y, controlPointEnd.X, controlPointEnd.Y, endPoint.X, endPoint.Y);
        }

        public void PathFinish()
        {
            _nativeInstance.PathFinish();
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Using assert instead.")]
        public void PathLineToAbs(IEnumerable<PointD> coordinates)
        {
            DebugThrow.IfNull(nameof(coordinates), coordinates);

            foreach (PointD coordinate in coordinates)
            {
                _nativeInstance.PathLineToAbs(coordinate.X, coordinate.Y);
            }
        }

        public void PathLineToHorizontalAbs(double x)
        {
            _nativeInstance.PathLineToHorizontalAbs(x);
        }

        public void PathLineToVerticalRel(double y)
        {
            _nativeInstance.PathLineToVerticalRel(y);
        }

        public void PathLineToHorizontalRel(double x)
        {
            _nativeInstance.PathLineToHorizontalRel(x);
        }

        public void PathLineToVerticalAbs(double y)
        {
            _nativeInstance.PathLineToVerticalAbs(y);
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Using assert instead.")]
        public void PathLineToRel(IEnumerable<PointD> coordinates)
        {
            DebugThrow.IfNull(nameof(coordinates), coordinates);

            foreach (PointD coordinate in coordinates)
            {
                _nativeInstance.PathLineToRel(coordinate.X, coordinate.Y);
            }
        }

        public void PathMoveToAbs(double x, double y)
        {
            _nativeInstance.PathMoveToAbs(x, y);
        }

        public void PathMoveToRel(double x, double y)
        {
            _nativeInstance.PathMoveToRel(x, y);
        }

        public void PathQuadraticCurveToAbs(PointD controlPoint, PointD endPoint)
        {
            _nativeInstance.PathQuadraticCurveToAbs(controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);
        }

        public void PathQuadraticCurveToRel(PointD controlPoint, PointD endPoint)
        {
            _nativeInstance.PathQuadraticCurveToRel(controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);
        }

        public void PathSmoothCurveToAbs(PointD controlPoint, PointD endPoint)
        {
            _nativeInstance.PathSmoothCurveToAbs(controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);
        }

        public void PathSmoothCurveToRel(PointD controlPoint, PointD endPoint)
        {
            _nativeInstance.PathSmoothCurveToRel(controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);
        }

        public void PathSmoothQuadraticCurveToAbs(PointD endPoint)
        {
            _nativeInstance.PathSmoothQuadraticCurveToAbs(endPoint.X, endPoint.Y);
        }

        public void PathSmoothQuadraticCurveToRel(PointD endPoint)
        {
            _nativeInstance.PathSmoothQuadraticCurveToRel(endPoint.X, endPoint.Y);
        }

        public void PathStart()
        {
            _nativeInstance.PathStart();
        }

        public void Point(double x, double y)
        {
            _nativeInstance.Point(x, y);
        }

        public void Polygon(IList<PointD> coordinates)
        {
            DebugThrow.IfNull(nameof(coordinates), coordinates);

            using (PointInfoCollection pointInfo = new PointInfoCollection(coordinates))
            {
                _nativeInstance.Polygon(pointInfo, pointInfo.Count);
            }
        }

        public void Polyline(IList<PointD> coordinates)
        {
            DebugThrow.IfNull(nameof(coordinates), coordinates);

            using (PointInfoCollection pointInfo = new PointInfoCollection(coordinates))
            {
                _nativeInstance.Polyline(pointInfo, pointInfo.Count);
            }
        }

        public void PopClipPath()
        {
            _nativeInstance.PopClipPath();
        }

        public void PopGraphicContext()
        {
            _nativeInstance.PopGraphicContext();
        }

        public void PopPattern()
        {
            _nativeInstance.PopPattern();
        }

        public void PushClipPath(string clipPath)
        {
            _nativeInstance.PushClipPath(clipPath);
        }

        public void PushGraphicContext()
        {
            _nativeInstance.PushGraphicContext();
        }

        public void PushPattern(string id, double x, double y, double width, double height)
        {
            _nativeInstance.PushPattern(id, x, y, width, height);
        }

        public void Rectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY)
        {
            _nativeInstance.Rectangle(upperLeftX, upperLeftY, lowerRightX, lowerRightY);
        }

        public void Rotation(double angle)
        {
            _nativeInstance.Rotation(angle);
        }

        public void RoundRectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY, double cornerWidth, double cornerHeight)
        {
            _nativeInstance.RoundRectangle(upperLeftX, upperLeftY, lowerRightX, lowerRightY, cornerWidth, cornerHeight);
        }

        public void Scaling(double x, double y)
        {
            _nativeInstance.Scaling(x, y);
        }

        public void SkewX(double angle)
        {
            _nativeInstance.SkewX(angle);
        }

        public void SkewY(double angle)
        {
            _nativeInstance.SkewY(angle);
        }

        public void StrokeAntialias(bool isEnabled)
        {
            _nativeInstance.StrokeAntialias(isEnabled);
        }

        public void StrokeColor(MagickColor color)
        {
            _nativeInstance.StrokeColor(color);
        }

        public void StrokeDashArray(double[] dash)
        {
            DebugThrow.IfNull(nameof(dash), dash);

            _nativeInstance.StrokeDashArray(dash, dash.Length);
        }

        public void StrokeDashOffset(double value)
        {
            _nativeInstance.StrokeDashOffset(value);
        }

        public void StrokeLineCap(LineCap value)
        {
            _nativeInstance.StrokeLineCap(value);
        }

        public void StrokeLineJoin(LineJoin value)
        {
            _nativeInstance.StrokeLineJoin(value);
        }

        public void StrokeMiterLimit(int value)
        {
            _nativeInstance.StrokeMiterLimit(value);
        }

        public void StrokeOpacity(double value)
        {
            _nativeInstance.StrokeOpacity(value);
        }

        public void StrokePatternUrl(string url)
        {
            _nativeInstance.StrokePatternUrl(url);
        }

        public void StrokeWidth(double value)
        {
            _nativeInstance.StrokeWidth(value);
        }

        public void Text(double x, double y, string value)
        {
            _nativeInstance.Text(x, y, value);
        }

        public void TextAlignment(TextAlignment value)
        {
            _nativeInstance.TextAlignment(value);
        }

        public void TextAntialias(bool isEnabled)
        {
            _nativeInstance.TextAntialias(isEnabled);
        }

        public void TextDecoration(TextDecoration value)
        {
            _nativeInstance.TextDecoration(value);
        }

        public void TextDirection(TextDirection value)
        {
            _nativeInstance.TextDirection(value);
        }

        public void TextEncoding(Encoding value)
        {
            if (value != null)
                _nativeInstance.TextEncoding(value.WebName);
        }

        public void TextInterlineSpacing(double spacing)
        {
            _nativeInstance.TextInterlineSpacing(spacing);
        }

        public void TextInterwordSpacing(double spacing)
        {
            _nativeInstance.TextInterwordSpacing(spacing);
        }

        public void TextKerning(double value)
        {
            _nativeInstance.TextKerning(value);
        }

        public void TextUnderColor(MagickColor color)
        {
            _nativeInstance.TextUnderColor(color);
        }

        public void Translation(double x, double y)
        {
            _nativeInstance.Translation(x, y);
        }

        public void Viewbox(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY)
        {
            _nativeInstance.Viewbox(upperLeftX, upperLeftY, lowerRightX, lowerRightY);
        }
    }
}