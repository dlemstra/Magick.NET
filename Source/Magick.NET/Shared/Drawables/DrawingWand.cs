// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ImageMagick
{
    internal sealed partial class DrawingWand : IDisposable
    {
        public DrawingWand(MagickImage image)
        {
            _NativeInstance = new NativeDrawingWand(image, image.Settings.Drawing);
        }

        public void Draw(IEnumerable<IDrawable> drawables)
        {
            foreach (IDrawable drawable in drawables)
            {
                ((IDrawingWand)drawable).Draw(this);
            }

            _NativeInstance.Render();
        }

        public void Affine(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY)
        {
            _NativeInstance.Affine(scaleX, scaleY, shearX, shearY, translateX, translateY);
        }

        public void Alpha(double x, double y, PaintMethod paintMethod)
        {
            _NativeInstance.Alpha(x, y, paintMethod);
        }

        public void Arc(double startX, double startY, double endX, double endY, double startDegrees, double endDegrees)
        {
            _NativeInstance.Arc(startX, startY, endX, endY, startDegrees, endDegrees);
        }

        public void Bezier(IList<PointD> coordinates)
        {
            using (PointInfoCollection pointInfo = new PointInfoCollection(coordinates))
            {
                _NativeInstance.Bezier(pointInfo, pointInfo.Count);
            }
        }

        public void BorderColor(MagickColor color)
        {
            _NativeInstance.BorderColor(color);
        }

        public void Circle(double originX, double originY, double perimeterX, double perimeterY)
        {
            _NativeInstance.Circle(originX, originY, perimeterX, perimeterY);
        }

        public void ClipPath(string value)
        {
            _NativeInstance.ClipPath(value);
        }

        public void ClipRule(FillRule value)
        {
            _NativeInstance.ClipRule(value);
        }

        public void ClipUnits(ClipPathUnit value)
        {
            _NativeInstance.ClipUnits(value);
        }

        public void Color(double x, double y, PaintMethod paintMethod)
        {
            _NativeInstance.Color(x, y, paintMethod);
        }

        public void Composite(double x, double y, double width, double height, CompositeOperator compositeOperator, IMagickImage image)
        {
            _NativeInstance.Composite(x, y, width, height, compositeOperator, image);
        }

        public void Density(PointD value)
        {
            _NativeInstance.Density(value.ToString());
        }

        public void Dispose()
        {
            DebugThrow.IfNull(_NativeInstance);
            _NativeInstance.Dispose();
        }

        public void Ellipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees)
        {
            _NativeInstance.Ellipse(originX, originY, radiusX, radiusY, startDegrees, endDegrees);
        }

        public void FillColor(MagickColor color)
        {
            _NativeInstance.FillColor(color);
        }

        public void FillOpacity(double value)
        {
            _NativeInstance.FillOpacity(value);
        }

        public void FillPatternUrl(string url)
        {
            _NativeInstance.FillPatternUrl(url);
        }

        public void FillRule(FillRule value)
        {
            _NativeInstance.FillRule(value);
        }

        public void Font(string fontName)
        {
            _NativeInstance.Font(fontName);
        }

        public void FontFamily(string family, FontStyleType style, FontWeight weight, FontStretch stretch)
        {
            _NativeInstance.FontFamily(family, style, weight, stretch);
        }

        public void FontPointSize(double value)
        {
            _NativeInstance.FontPointSize(value);
        }

        public void Gravity(Gravity value)
        {
            _NativeInstance.Gravity(value);
        }

        public void Line(double startX, double startY, double endX, double endY)
        {
            _NativeInstance.Line(startX, startY, endX, endY);
        }

        public void PathArcAbs(IEnumerable<PathArc> pathArcs)
        {
            DebugThrow.IfNull(nameof(pathArcs), pathArcs);

            foreach (PathArc pathArc in pathArcs)
            {
                _NativeInstance.PathArcAbs(pathArc.X, pathArc.Y, pathArc.RadiusX, pathArc.RadiusY, pathArc.RotationX, pathArc.UseLargeArc, pathArc.UseSweep);
            }
        }

        public void PathArcRel(IEnumerable<PathArc> pathArcs)
        {
            DebugThrow.IfNull(nameof(pathArcs), pathArcs);

            foreach (PathArc pathArc in pathArcs)
            {
                _NativeInstance.PathArcRel(pathArc.X, pathArc.Y, pathArc.RadiusX, pathArc.RadiusY, pathArc.RotationX, pathArc.UseLargeArc, pathArc.UseSweep);
            }
        }

        public void PathClose()
        {
            _NativeInstance.PathClose();
        }

        public void PathCurveToAbs(PointD controlPointStart, PointD controlPointEnd, PointD endPoint)
        {
            _NativeInstance.PathCurveToAbs(controlPointStart.X, controlPointStart.Y, controlPointEnd.X, controlPointEnd.Y, endPoint.X, endPoint.Y);
        }

        public void PathCurveToRel(PointD controlPointStart, PointD controlPointEnd, PointD endPoint)
        {
            _NativeInstance.PathCurveToRel(controlPointStart.X, controlPointStart.Y, controlPointEnd.X, controlPointEnd.Y, endPoint.X, endPoint.Y);
        }

        public void PathFinish()
        {
            _NativeInstance.PathFinish();
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Using assert instead.")]
        public void PathLineToAbs(IEnumerable<PointD> coordinates)
        {
            DebugThrow.IfNull(nameof(coordinates), coordinates);

            foreach (PointD coordinate in coordinates)
            {
                _NativeInstance.PathLineToAbs(coordinate.X, coordinate.Y);
            }
        }

        public void PathLineToHorizontalAbs(double x)
        {
            _NativeInstance.PathLineToHorizontalAbs(x);
        }

        public void PathLineToVerticalRel(double y)
        {
            _NativeInstance.PathLineToVerticalRel(y);
        }

        public void PathLineToHorizontalRel(double x)
        {
            _NativeInstance.PathLineToHorizontalRel(x);
        }

        public void PathLineToVerticalAbs(double y)
        {
            _NativeInstance.PathLineToVerticalAbs(y);
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Using assert instead.")]
        public void PathLineToRel(IEnumerable<PointD> coordinates)
        {
            DebugThrow.IfNull(nameof(coordinates), coordinates);

            foreach (PointD coordinate in coordinates)
            {
                _NativeInstance.PathLineToRel(coordinate.X, coordinate.Y);
            }
        }

        public void PathMoveToAbs(double x, double y)
        {
            _NativeInstance.PathMoveToAbs(x, y);
        }

        public void PathMoveToRel(double x, double y)
        {
            _NativeInstance.PathMoveToRel(x, y);
        }

        public void PathQuadraticCurveToAbs(PointD controlPoint, PointD endPoint)
        {
            _NativeInstance.PathQuadraticCurveToAbs(controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);
        }

        public void PathQuadraticCurveToRel(PointD controlPoint, PointD endPoint)
        {
            _NativeInstance.PathQuadraticCurveToRel(controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);
        }

        public void PathSmoothCurveToAbs(PointD controlPoint, PointD endPoint)
        {
            _NativeInstance.PathSmoothCurveToAbs(controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);
        }

        public void PathSmoothCurveToRel(PointD controlPoint, PointD endPoint)
        {
            _NativeInstance.PathSmoothCurveToRel(controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);
        }

        public void PathSmoothQuadraticCurveToAbs(PointD endPoint)
        {
            _NativeInstance.PathSmoothQuadraticCurveToAbs(endPoint.X, endPoint.Y);
        }

        public void PathSmoothQuadraticCurveToRel(PointD endPoint)
        {
            _NativeInstance.PathSmoothQuadraticCurveToRel(endPoint.X, endPoint.Y);
        }

        public void PathStart()
        {
            _NativeInstance.PathStart();
        }

        public void Point(double x, double y)
        {
            _NativeInstance.Point(x, y);
        }

        public void Polygon(IList<PointD> coordinates)
        {
            DebugThrow.IfNull(nameof(coordinates), coordinates);

            using (PointInfoCollection pointInfo = new PointInfoCollection(coordinates))
            {
                _NativeInstance.Polygon(pointInfo, pointInfo.Count);
            }
        }

        public void Polyline(IList<PointD> coordinates)
        {
            DebugThrow.IfNull(nameof(coordinates), coordinates);

            using (PointInfoCollection pointInfo = new PointInfoCollection(coordinates))
            {
                _NativeInstance.Polyline(pointInfo, pointInfo.Count);
            }
        }

        public void PopClipPath()
        {
            _NativeInstance.PopClipPath();
        }

        public void PopGraphicContext()
        {
            _NativeInstance.PopGraphicContext();
        }

        public void PopPattern()
        {
            _NativeInstance.PopPattern();
        }

        public void PushClipPath(string clipPath)
        {
            _NativeInstance.PushClipPath(clipPath);
        }

        public void PushGraphicContext()
        {
            _NativeInstance.PushGraphicContext();
        }

        public void PushPattern(string id, double x, double y, double width, double height)
        {
            _NativeInstance.PushPattern(id, x, y, width, height);
        }

        public void Rectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY)
        {
            _NativeInstance.Rectangle(upperLeftX, upperLeftY, lowerRightX, lowerRightY);
        }

        public void Rotation(double angle)
        {
            _NativeInstance.Rotation(angle);
        }

        public void RoundRectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY, double cornerWidth, double cornerHeight)
        {
            _NativeInstance.RoundRectangle(upperLeftX, upperLeftY, lowerRightX, lowerRightY, cornerWidth, cornerHeight);
        }

        public void Scaling(double x, double y)
        {
            _NativeInstance.Scaling(x, y);
        }

        public void SkewX(double angle)
        {
            _NativeInstance.SkewX(angle);
        }

        public void SkewY(double angle)
        {
            _NativeInstance.SkewY(angle);
        }

        public void StrokeAntialias(bool isEnabled)
        {
            _NativeInstance.StrokeAntialias(isEnabled);
        }

        public void StrokeColor(MagickColor color)
        {
            _NativeInstance.StrokeColor(color);
        }

        public void StrokeDashArray(double[] dash)
        {
            DebugThrow.IfNull(nameof(dash), dash);

            _NativeInstance.StrokeDashArray(dash, dash.Length);
        }

        public void StrokeDashOffset(double value)
        {
            _NativeInstance.StrokeDashOffset(value);
        }

        public void StrokeLineCap(LineCap value)
        {
            _NativeInstance.StrokeLineCap(value);
        }

        public void StrokeLineJoin(LineJoin value)
        {
            _NativeInstance.StrokeLineJoin(value);
        }

        public void StrokeMiterLimit(int value)
        {
            _NativeInstance.StrokeMiterLimit(value);
        }

        public void StrokeOpacity(double value)
        {
            _NativeInstance.StrokeOpacity(value);
        }

        public void StrokePatternUrl(string url)
        {
            _NativeInstance.StrokePatternUrl(url);
        }

        public void StrokeWidth(double value)
        {
            _NativeInstance.StrokeWidth(value);
        }

        public void Text(double x, double y, string value)
        {
            _NativeInstance.Text(x, y, value);
        }

        public void TextAlignment(TextAlignment value)
        {
            _NativeInstance.TextAlignment(value);
        }

        public void TextAntialias(bool isEnabled)
        {
            _NativeInstance.TextAntialias(isEnabled);
        }

        public void TextDecoration(TextDecoration value)
        {
            _NativeInstance.TextDecoration(value);
        }

        public void TextDirection(TextDirection value)
        {
            _NativeInstance.TextDirection(value);
        }

        public void TextEncoding(Encoding value)
        {
            if (value != null)
                _NativeInstance.TextEncoding(value.WebName);
        }

        public void TextInterlineSpacing(double spacing)
        {
            _NativeInstance.TextInterlineSpacing(spacing);
        }

        public void TextInterwordSpacing(double spacing)
        {
            _NativeInstance.TextInterwordSpacing(spacing);
        }

        public void TextKerning(double value)
        {
            _NativeInstance.TextKerning(value);
        }

        public void TextUnderColor(MagickColor color)
        {
            _NativeInstance.TextUnderColor(color);
        }

        public void Translation(double x, double y)
        {
            _NativeInstance.Translation(x, y);
        }

        public void Viewbox(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY)
        {
            _NativeInstance.Viewbox(upperLeftX, upperLeftY, lowerRightX, lowerRightY);
        }
    }
}