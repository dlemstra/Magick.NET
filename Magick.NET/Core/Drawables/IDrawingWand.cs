//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ImageMagick
{
  /// <summary>
  /// Interface for internal use.
  /// </summary>
  public interface IDrawingWand
  {
#pragma warning disable 1591
#pragma warning disable SA1600 // Elements must be documented

    void Affine(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY);
    void Alpha(double x, double y, PaintMethod paintMethod);
    void Arc(double startX, double startY, double endX, double endY, double startDegrees, double endDegrees);
    void Bezier(IList<PointD> coordinates);
    void BorderColor(MagickColor color);
    void Circle(double originX, double originY, double perimeterX, double perimeterY);
    void ClipPath(string value);
    void ClipRule(FillRule value);
    void ClipUnits(ClipPathUnit value);
    void Color(double x, double y, PaintMethod paintMethod);
    void Composite(double x, double y, double width, double height, CompositeOperator compositeOperator, MagickImage image);
    void Density(PointD value);
    void Ellipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees);
    void FillColor(MagickColor color);
    void FillOpacity(double value);
    [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "Url won't work in all situations.")]
    void FillPatternUrl(string url);
    void FillRule(FillRule value);
    void Font(string fontName);
    void FontFamily(string family, FontStyleType style, FontWeight weight, FontStretch stretch);
    void FontPointSize(double value);
    void Gravity(Gravity value);
    void Line(double startX, double startY, double endX, double endY);
    void PathArcAbs(IEnumerable<PathArc> pathArcs);
    void PathArcRel(IEnumerable<PathArc> pathArcs);
    void PathClose();
    void PathCurveToAbs(PointD controlPointStart, PointD controlPointEnd, PointD endPoint);
    void PathCurveToRel(PointD controlPointStart, PointD controlPointEnd, PointD endPoint);
    void PathFinish();
    void PathLineToAbs(IEnumerable<PointD> coordinates);
    void PathLineToHorizontalAbs(double x);
    void PathLineToHorizontalRel(double x);
    void PathLineToRel(IEnumerable<PointD> coordinates);
    void PathLineToVerticalAbs(double y);
    void PathLineToVerticalRel(double y);
    void PathMoveToAbs(double x, double y);
    void PathMoveToRel(double x, double y);
    void PathQuadraticCurveToAbs(PointD controlPoint, PointD endPoint);
    void PathQuadraticCurveToRel(PointD controlPoint, PointD endPoint);
    void PathSmoothCurveToAbs(PointD controlPoint, PointD endPoint);
    void PathSmoothCurveToRel(PointD controlPoint, PointD endPoint);
    void PathSmoothQuadraticCurveToRel(PointD endPoint);
    void PathSmoothQuadraticCurveToAbs(PointD endPoint);
    void PathStart();
    void Point(double x, double y);
    void Polygon(IList<PointD> coordinates);
    void Polyline(IList<PointD> coordinates);
    void PopClipPath();
    void PopGraphicContext();
    void PopPattern();
    void PushClipPath(string clipPath);
    void PushPattern(string id, double x, double y, double width, double height);
    void PushGraphicContext();
    void Rectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY);
    void Rotation(double angle);
    void RoundRectangle(double centerX, double centerY, double width, double height, double cornerWidth, double cornerHeight);
    void Scaling(double x, double y);
    void SkewX(double angle);
    void SkewY(double angle);
    void StrokeAntialias(bool isEnabled);
    void StrokeColor(MagickColor color);
    void StrokeDashArray(double[] dash);
    void StrokeDashOffset(double value);
    void StrokeLineCap(LineCap value);
    void StrokeLineJoin(LineJoin value);
    void StrokeMiterLimit(int value);
    void StrokeOpacity(double value);
    [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "Url won't work in all situations.")]
    void StrokePatternUrl(string url);
    void StrokeWidth(double value);
    void Text(double x, double y, string value);
    void TextAlignment(TextAlignment value);
    void TextAntialias(bool isEnabled);
    void TextDecoration(TextDecoration value);
    void TextDirection(TextDirection value);
    void TextEncoding(Encoding value);
    void TextInterlineSpacing(double value);
    void TextInterwordSpacing(double value);
    void TextKerning(double value);
    void TextUnderColor(MagickColor color);
    void Translation(double x, double y);
    void Viewbox(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY);

#pragma warning restore SA1600 // Elements must be documented
#pragma warning restore 1591
  }
}