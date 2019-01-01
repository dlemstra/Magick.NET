// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#include "Stdafx.h"
#include "DrawingWand.h"
#include "Types/TypeMetric.h"

#define MAGICK_NET_GET_PIXEL_WAND(color) \
  PixelWand \
    *pixel_wand; \
  pixel_wand=NewPixelWand(); \
  PixelSetPixelColor(pixel_wand,color)

#define MAGICK_NET_REMOVE_PIXEL_WAND \
  pixel_wand=DestroyPixelWand(pixel_wand)

#define MAGICK_NET_SET_DRAW_EXCEPTION \
  *exception=DrawingWand_DestroyException(instance)

static inline ExceptionInfo *DrawingWand_DestroyException(DrawingWand *instance)
{
  if (DrawGetExceptionType(instance) == UndefinedException)
    return (ExceptionInfo *) NULL;

  return DrawCloneExceptionInfo(instance);
}

MAGICK_NET_EXPORT DrawingWand *DrawingWand_Create(Image *image, const DrawInfo *drawInfo)
{
  return AcquireDrawingWand(drawInfo, image);
}

MAGICK_NET_EXPORT void DrawingWand_Dispose(DrawingWand *instance)
{
  DestroyDrawingWand(instance);
}

MAGICK_NET_EXPORT void DrawingWand_Affine(DrawingWand *instance, const double scaleX, const double scaleY, const double shearX, const double shearY, const double translateX, const double translateY, ExceptionInfo **exception)
{
  AffineMatrix
    matrix;

  matrix.sx = scaleX;
  matrix.sy = scaleY;
  matrix.rx = shearX;
  matrix.ry = shearY;
  matrix.tx = translateX;
  matrix.ty = translateY;

  DrawAffine(instance, &matrix);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Alpha(DrawingWand *instance, const double x, const double y, const size_t paintMethod, ExceptionInfo **exception)
{
  DrawAlpha(instance, x, y, (const PaintMethod) paintMethod);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Arc(DrawingWand *instance, const double startX, const double startY, const double endX, const double endY, const double startDegrees, const double endDegrees, ExceptionInfo **exception)
{
  DrawArc(instance, startX, startY, endX, endY, startDegrees, endDegrees);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Bezier(DrawingWand *instance, const PointInfo *coordinates, const size_t length, ExceptionInfo **exception)
{
  DrawBezier(instance, length, coordinates);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_BorderColor(DrawingWand *instance, const PixelInfo *color, ExceptionInfo **exception)
{
  MAGICK_NET_GET_PIXEL_WAND(color);
  DrawSetBorderColor(instance, pixel_wand);
  MAGICK_NET_REMOVE_PIXEL_WAND;
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Circle(DrawingWand *instance, const double originX, const double originY, const double perimeterX, const double perimeterY, ExceptionInfo **exception)
{
  DrawCircle(instance, originX, originY, perimeterX, perimeterY);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_ClipPath(DrawingWand *instance, const char *value, ExceptionInfo **exception)
{
  DrawSetClipPath(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_ClipRule(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetClipRule(instance, (const FillRule) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_ClipUnits(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetClipUnits(instance, (const ClipPathUnits) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Color(DrawingWand *instance, const double x, const double y, const size_t paintMethod, ExceptionInfo **exception)
{
  DrawColor(instance, x, y, (const PaintMethod) paintMethod);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Composite(DrawingWand *instance, const double x, const double y, const double width, const double height, const size_t compositeOperator, const Image *image, ExceptionInfo **exception)
{
  MagickWand
    *magick_wand;

  magick_wand = NewMagickWandFromImage(image);
  DrawComposite(instance, (const CompositeOperator) compositeOperator, x, y, width, height, magick_wand);
  DestroyMagickWand(magick_wand);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Density(DrawingWand *instance, const char *value, ExceptionInfo **exception)
{
  DrawSetDensity(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Ellipse(DrawingWand *instance, const double originX, const double originY, const double radiusX, const double radiusY, const double startDegrees, const double endDegrees, ExceptionInfo **exception)
{
  DrawEllipse(instance, originX, originY, radiusX, radiusY, startDegrees, endDegrees);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_FillColor(DrawingWand *instance, const PixelInfo *color, ExceptionInfo **exception)
{
  MAGICK_NET_GET_PIXEL_WAND(color);
  DrawSetFillColor(instance, pixel_wand);
  MAGICK_NET_REMOVE_PIXEL_WAND;
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_FillOpacity(DrawingWand *instance, const double value, ExceptionInfo **exception)
{
  DrawSetFillOpacity(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_FillPatternUrl(DrawingWand *instance, const char *url, ExceptionInfo **exception)
{
  DrawSetFillPatternURL(instance, url);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_FillRule(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetFillRule(instance, (const FillRule) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Font(DrawingWand *instance, const char *fontName, ExceptionInfo **exception)
{
  DrawSetFont(instance, fontName);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_FontFamily(DrawingWand *instance, const char *family, const size_t style, const size_t weight, const size_t stretch, ExceptionInfo **exception)
{
  DrawSetFontFamily(instance, family);
  DrawSetFontStyle(instance, (const StyleType) style);
  DrawSetFontWeight(instance, weight);
  DrawSetFontStretch(instance, stretch);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_FontPointSize(DrawingWand *instance, const double value, ExceptionInfo **exception)
{
  DrawSetFontSize(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT TypeMetric *DrawingWand_FontTypeMetrics(DrawingWand *instance, const char *text, const MagickBooleanType ignoreNewlines, ExceptionInfo **exception)
{
  TypeMetric
    *result;

  result = TypeMetric_Create();
  DrawGetTypeMetrics(instance, text, ignoreNewlines, result);
  MAGICK_NET_SET_DRAW_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void DrawingWand_Gravity(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetGravity(instance, (const GravityType) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Line(DrawingWand *instance, const double startX, const double startY, const double endX, const double endY, ExceptionInfo **exception)
{
  DrawLine(instance, startX, startY, endX, endY);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathArcAbs(DrawingWand *instance, const double x, const double y, const double radiusX, const double radiusY, const double rotationX, MagickBooleanType useLargeArc, MagickBooleanType useSweep, ExceptionInfo **exception)
{
  DrawPathEllipticArcAbsolute(instance, radiusX, radiusY, rotationX, useLargeArc, useSweep, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathArcRel(DrawingWand *instance, const double x, const double y, const double radiusX, const double radiusY, const double rotationX, MagickBooleanType useLargeArc, MagickBooleanType useSweep, ExceptionInfo **exception)
{
  DrawPathEllipticArcRelative(instance, radiusX, radiusY, rotationX, useLargeArc, useSweep, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathClose(DrawingWand *instance, ExceptionInfo **exception)
{
  DrawPathClose(instance);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathCurveToAbs(DrawingWand *instance, const double x1, const double y1, const double x2, const double y2, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathCurveToAbsolute(instance, x1, y1, x2, y2, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathCurveToRel(DrawingWand *instance, const double x1, const double y1, const double x2, const double y2, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathCurveToRelative(instance, x1, y1, x2, y2, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathFinish(DrawingWand *instance, ExceptionInfo **exception)
{
  DrawPathFinish(instance);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathLineToAbs(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathLineToAbsolute(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathLineToHorizontalAbs(DrawingWand *instance, const double x, ExceptionInfo **exception)
{
  DrawPathLineToHorizontalAbsolute(instance, x);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathLineToHorizontalRel(DrawingWand *instance, const double x, ExceptionInfo **exception)
{
  DrawPathLineToHorizontalRelative(instance, x);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathLineToRel(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathLineToRelative(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathLineToVerticalAbs(DrawingWand *instance, const double y, ExceptionInfo **exception)
{
  DrawPathLineToVerticalAbsolute(instance, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathLineToVerticalRel(DrawingWand *instance, const double y, ExceptionInfo **exception)
{
  DrawPathLineToVerticalRelative(instance, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathMoveToAbs(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathMoveToAbsolute(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathMoveToRel(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathMoveToRelative(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathQuadraticCurveToAbs(DrawingWand *instance, const double x1, const double y1, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathCurveToQuadraticBezierAbsolute(instance, x1, y1, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathQuadraticCurveToRel(DrawingWand *instance, const double x1, const double y1, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathCurveToQuadraticBezierRelative(instance, x1, y1, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathSmoothCurveToAbs(DrawingWand *instance, const double x2, const double y2, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathCurveToSmoothAbsolute(instance, x2, y2, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathSmoothCurveToRel(DrawingWand *instance, const double x2, const double y2, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathCurveToSmoothRelative(instance, x2, y2, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathSmoothQuadraticCurveToAbs(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathCurveToQuadraticBezierSmoothAbsolute(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathSmoothQuadraticCurveToRel(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawPathCurveToQuadraticBezierSmoothRelative(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PathStart(DrawingWand *instance, ExceptionInfo **exception)
{
  DrawPathStart(instance);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Point(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawPoint(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Polygon(DrawingWand *instance, const PointInfo *coordinates, const size_t length, ExceptionInfo **exception)
{
  DrawPolygon(instance, length, coordinates);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Polyline(DrawingWand *instance, const PointInfo *coordinates, const size_t length, ExceptionInfo **exception)
{
  DrawPolyline(instance, length, coordinates);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PopClipPath(DrawingWand *instance, ExceptionInfo **exception)
{
  DrawPopClipPath(instance);
  DrawPopDefs(instance);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PopGraphicContext(DrawingWand *instance, ExceptionInfo **exception)
{
  PopDrawingWand(instance);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PopPattern(DrawingWand *instance, ExceptionInfo **exception)
{
  DrawPopPattern(instance);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PushClipPath(DrawingWand *instance, const char *clipPath, ExceptionInfo **exception)
{
  DrawPushDefs(instance);
  DrawPushClipPath(instance, clipPath);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PushGraphicContext(DrawingWand *instance, ExceptionInfo **exception)
{
  PushDrawingWand(instance);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_PushPattern(DrawingWand *instance, const char *id, const double x, const double y, const double width, const double height, ExceptionInfo **exception)
{
  DrawPushPattern(instance, id, x, y, width, height);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Rectangle(DrawingWand *instance, const double upperLeftX, const double upperLeftY, const double lowerRightX, const double lowerRightY, ExceptionInfo **exception)
{
  DrawRectangle(instance, upperLeftX, upperLeftY, lowerRightX, lowerRightY);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Render(DrawingWand *instance, ExceptionInfo **exception)
{
  DrawRender(instance);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Rotation(DrawingWand *instance, const double angle, ExceptionInfo **exception)
{
  DrawRotate(instance, angle);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_RoundRectangle(DrawingWand *instance, const double upperLeftX, const double upperLeftY, const double lowerRightX, const double lowerRightY, const double cornerWidth, const double cornerHeight, ExceptionInfo **exception)
{
  DrawRoundRectangle(instance, upperLeftX, upperLeftY, lowerRightX, lowerRightY, cornerWidth, cornerHeight);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Scaling(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawScale(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_SkewX(DrawingWand *instance, const double angle, ExceptionInfo **exception)
{
  DrawSkewX(instance, angle);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_SkewY(DrawingWand *instance, const double angle, ExceptionInfo **exception)
{
  DrawSkewY(instance, angle);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeAntialias(DrawingWand *instance, MagickBooleanType isEnabled, ExceptionInfo **exception)
{
  DrawSetStrokeAntialias(instance, isEnabled);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeColor(DrawingWand *instance, PixelInfo *color, ExceptionInfo **exception)
{
  MAGICK_NET_GET_PIXEL_WAND(color);
  DrawSetStrokeColor(instance, pixel_wand);
  MAGICK_NET_REMOVE_PIXEL_WAND;
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeDashArray(DrawingWand *instance, double *dash, const size_t length, ExceptionInfo **exception)
{
  DrawSetStrokeDashArray(instance, length, dash);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeDashOffset(DrawingWand *instance, const double value, ExceptionInfo **exception)
{
  DrawSetStrokeDashOffset(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeLineCap(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetStrokeLineCap(instance, (const LineCap) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeLineJoin(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetStrokeLineJoin(instance, (const LineJoin) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeMiterLimit(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetStrokeMiterLimit(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeOpacity(DrawingWand *instance, const double value, ExceptionInfo **exception)
{
  DrawSetStrokeOpacity(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokePatternUrl(DrawingWand *instance, const char *url, ExceptionInfo **exception)
{
  DrawSetStrokePatternURL(instance, url);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_StrokeWidth(DrawingWand *instance, const double value, ExceptionInfo **exception)
{
  DrawSetStrokeWidth(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Text(DrawingWand *instance, const double x, const double y, const unsigned char *text, ExceptionInfo **exception)
{
  DrawAnnotation(instance, x, y, text);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextAlignment(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetTextAlignment(instance, (const AlignType) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextAntialias(DrawingWand *instance, MagickBooleanType value, ExceptionInfo **exception)
{
  DrawSetTextAntialias(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextDecoration(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetTextDecoration(instance, (const DecorationType) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextDirection(DrawingWand *instance, const size_t value, ExceptionInfo **exception)
{
  DrawSetTextDirection(instance, (const DirectionType) value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextEncoding(DrawingWand *instance, const char *encoding, ExceptionInfo **exception)
{
  DrawSetTextEncoding(instance, encoding);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextInterlineSpacing(DrawingWand *instance, const double value, ExceptionInfo **exception)
{
  DrawSetTextInterlineSpacing(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextInterwordSpacing(DrawingWand *instance, const double value, ExceptionInfo **exception)
{
  DrawSetTextInterwordSpacing(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextKerning(DrawingWand *instance, const double value, ExceptionInfo **exception)
{
  DrawSetTextKerning(instance, value);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_TextUnderColor(DrawingWand *instance, const PixelInfo *color, ExceptionInfo **exception)
{
  MAGICK_NET_GET_PIXEL_WAND(color);
  DrawSetTextUnderColor(instance, pixel_wand);
  MAGICK_NET_REMOVE_PIXEL_WAND;
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Translation(DrawingWand *instance, const double x, const double y, ExceptionInfo **exception)
{
  DrawTranslate(instance, x, y);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}

MAGICK_NET_EXPORT void DrawingWand_Viewbox(DrawingWand *instance, const double upperLeftX, const double upperLeftY, const double lowerRightX, const double lowerRightY, ExceptionInfo **exception)
{
  DrawSetViewbox(instance, upperLeftX, upperLeftY, lowerRightX, lowerRightY);
  MAGICK_NET_SET_DRAW_EXCEPTION;
}
