//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
#pragma once

MAGICK_NET_EXPORT DrawingWand *DrawingWand_Create(Image *, const DrawInfo *);

MAGICK_NET_EXPORT void DrawingWand_Dispose(DrawingWand *);

MAGICK_NET_EXPORT void DrawingWand_Affine(DrawingWand *, const double, const double, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Alpha(DrawingWand *, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Arc(DrawingWand *, const double, const double, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Bezier(DrawingWand *, const PointInfo*, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_BorderColor(DrawingWand *, const PixelInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Circle(DrawingWand *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_ClipPath(DrawingWand *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_ClipRule(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_ClipUnits(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Color(DrawingWand *, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Composite(DrawingWand *, const double, const double, const double, const double, const size_t, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Density(DrawingWand *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Ellipse(DrawingWand *, const double, const double, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_FillColor(DrawingWand *, const PixelInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_FillOpacity(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_FillPatternUrl(DrawingWand *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_FillRule(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Font(DrawingWand *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_FontFamily(DrawingWand *, const char *, const size_t, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_FontPointSize(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Gravity(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Line(DrawingWand *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathArcAbs(DrawingWand *, const double, const double, const double, const double, const double, MagickBooleanType, MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathArcRel(DrawingWand *, const double, const double, const double, const double, const double, MagickBooleanType, MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathClose(DrawingWand *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathCurveToAbs(DrawingWand *, const double, const double, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathCurveToRel(DrawingWand *, const double, const double, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathFinish(DrawingWand *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathLineToAbs(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathLineToHorizontalAbs(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathLineToHorizontalRel(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathLineToRel(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathLineToVerticalAbs(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathLineToVerticalRel(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathMoveToAbs(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathMoveToRel(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathQuadraticCurveToAbs(DrawingWand *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathQuadraticCurveToRel(DrawingWand *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathSmoothCurveToAbs(DrawingWand *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathSmoothCurveToRel(DrawingWand *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathSmoothQuadraticCurveToAbs(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathSmoothQuadraticCurveToRel(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PathStart(DrawingWand *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Point(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Polygon(DrawingWand *, const PointInfo*, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Polyline(DrawingWand *, const PointInfo*, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PopClipPath(DrawingWand *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PopGraphicContext(DrawingWand *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PopPattern(DrawingWand *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PushClipPath(DrawingWand *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PushGraphicContext(DrawingWand *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_PushPattern(DrawingWand *, const char *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Rectangle(DrawingWand *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Render(DrawingWand *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Rotation(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_RoundRectangle(DrawingWand *, const double, const double, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Scaling(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_SkewX(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_SkewY(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeAntialias(DrawingWand *, MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeColor(DrawingWand *, PixelInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeDashArray(DrawingWand *, double *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeDashOffset(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeLineCap(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeLineJoin(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeMiterLimit(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeOpacity(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokePatternUrl(DrawingWand *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_StrokeWidth(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Text(DrawingWand *, const double, const double, const unsigned char *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextAlignment(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextAntialias(DrawingWand *, MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextDirection(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextDecoration(DrawingWand *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextEncoding(DrawingWand *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextInterlineSpacing(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextInterwordSpacing(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextKerning(DrawingWand *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_TextUnderColor(DrawingWand *, const PixelInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Translation(DrawingWand *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingWand_Viewbox(DrawingWand *, const double, const double, const double, const double, ExceptionInfo **);
