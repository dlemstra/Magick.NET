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
#pragma once

MAGICK_NET_EXPORT DrawInfo *DrawingSettings_Create(void);

MAGICK_NET_EXPORT void DrawingSettings_Dispose(DrawInfo *);

MAGICK_NET_EXPORT PixelInfo *DrawingSettings_BorderColor_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_BorderColor_Set(DrawInfo *, const PixelInfo *);

MAGICK_NET_EXPORT PixelInfo *DrawingSettings_FillColor_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_FillColor_Set(DrawInfo *, const PixelInfo *);

MAGICK_NET_EXPORT size_t DrawingSettings_FillRule_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_FillRule_Set(DrawInfo *, const size_t);

MAGICK_NET_EXPORT const char *DrawingSettings_Font_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_Font_Set(DrawInfo *, const char *);

MAGICK_NET_EXPORT const char *DrawingSettings_FontFamily_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_FontFamily_Set(DrawInfo *, const char *);

MAGICK_NET_EXPORT size_t DrawingSettings_FontStyle_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_FontStyle_Set(DrawInfo *, const size_t);

MAGICK_NET_EXPORT size_t DrawingSettings_FontWeight_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_FontWeight_Set(DrawInfo *, const size_t);

MAGICK_NET_EXPORT MagickBooleanType DrawingSettings_StrokeAntiAlias_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_StrokeAntiAlias_Set(DrawInfo *, const MagickBooleanType);

MAGICK_NET_EXPORT PixelInfo *DrawingSettings_StrokeColor_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_StrokeColor_Set(DrawInfo *, const PixelInfo *);

MAGICK_NET_EXPORT double DrawingSettings_StrokeDashOffset_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_StrokeDashOffset_Set(DrawInfo *, const double);

MAGICK_NET_EXPORT size_t DrawingSettings_StrokeLineCap_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_StrokeLineCap_Set(DrawInfo *, const size_t);

MAGICK_NET_EXPORT size_t DrawingSettings_StrokeLineJoin_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_StrokeLineJoin_Set(DrawInfo *, const size_t);

MAGICK_NET_EXPORT size_t DrawingSettings_StrokeMiterLimit_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_StrokeMiterLimit_Set(DrawInfo *, const size_t);

MAGICK_NET_EXPORT double DrawingSettings_StrokeWidth_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_StrokeWidth_Set(DrawInfo *, const double);

MAGICK_NET_EXPORT MagickBooleanType DrawingSettings_TextAntiAlias_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_TextAntiAlias_Set(DrawInfo *, const MagickBooleanType);

MAGICK_NET_EXPORT size_t DrawingSettings_TextDirection_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_TextDirection_Set(DrawInfo *, const size_t);

MAGICK_NET_EXPORT const char *DrawingSettings_TextEncoding_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_TextEncoding_Set(DrawInfo *, const char *);

MAGICK_NET_EXPORT size_t DrawingSettings_TextGravity_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_TextGravity_Set(DrawInfo *, const size_t);

MAGICK_NET_EXPORT double DrawingSettings_TextInterlineSpacing_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_TextInterlineSpacing_Set(DrawInfo *, const double);

MAGICK_NET_EXPORT double DrawingSettings_TextInterwordSpacing_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_TextInterwordSpacing_Set(DrawInfo *, const double);

MAGICK_NET_EXPORT double DrawingSettings_TextKerning_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_TextKerning_Set(DrawInfo *, const double);

MAGICK_NET_EXPORT PixelInfo *DrawingSettings_TextUnderColor_Get(const DrawInfo *);
MAGICK_NET_EXPORT void DrawingSettings_TextUnderColor_Set(DrawInfo *, const  PixelInfo *);

MAGICK_NET_EXPORT const double *DrawingSettings_GetStrokeDashArray(DrawInfo *, size_t *);

MAGICK_NET_EXPORT void DrawingSettings_SetAffine(DrawInfo *, const double, const double, const double, const double, const double, const double);

MAGICK_NET_EXPORT void DrawingSettings_SetFillPattern(DrawInfo *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingSettings_SetStrokeDashArray(DrawInfo *, const double *, const size_t);

MAGICK_NET_EXPORT void DrawingSettings_SetStrokePattern(DrawInfo *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void DrawingSettings_SetText(DrawInfo *, const char *);