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

#include "Stdafx.h"
#include "DrawingSettings.h"
#if defined(NET20)
#include "../Colors/MagickColor.h"
#include "../MagickImage.h"
#else
#include "Colors/MagickColor.h"
#include "MagickImage.h"
#endif

#define MagickPI 3.14159265358979323846264338327950288419716939937510
#define DegreesToRadians(x) (MagickPI*(x)/180.0)

static inline void ResetAffine(AffineMatrix *affine)
{
  affine->sx = 1.0;
  affine->rx = 0.0;
  affine->ry = 0.0;
  affine->sy = 1.0;
  affine->tx = 0.0;
  affine->ty = 0.0;
}

MAGICK_NET_EXPORT DrawInfo *DrawingSettings_Create(void)
{
  return AcquireDrawInfo();
}

MAGICK_NET_EXPORT void DrawingSettings_Dispose(DrawInfo *instance)
{
  DestroyDrawInfo(instance);
}

MAGICK_NET_EXPORT PixelInfo *DrawingSettings_BorderColor_Get(const DrawInfo *instance)
{
  return MagickColor_Clone(&instance->border_color);
}

MAGICK_NET_EXPORT void DrawingSettings_BorderColor_Set(DrawInfo *instance, const PixelInfo *value)
{
  if (value != (PixelInfo *)NULL)
    instance->border_color = *value;
}

MAGICK_NET_EXPORT PixelInfo *DrawingSettings_FillColor_Get(const DrawInfo *instance)
{
  return MagickColor_Clone(&instance->fill);
}

MAGICK_NET_EXPORT void DrawingSettings_FillColor_Set(DrawInfo *instance, const PixelInfo *value)
{
  if (value != (PixelInfo *)NULL)
    instance->fill = *value;
}

MAGICK_NET_EXPORT Image *DrawingSettings_FillPattern_Get(const DrawInfo *instance, ExceptionInfo **exception)
{
  return MagickImage_Clone(instance->fill_pattern, exception);
}

MAGICK_NET_EXPORT void DrawingSettings_FillPattern_Set(DrawInfo *instance, const Image *value, ExceptionInfo **exception)
{
  if (instance->fill_pattern != (Image *)NULL)
    instance->fill_pattern = DestroyImage(instance->fill_pattern);

  if (value != (const Image *)NULL)
    instance->fill_pattern = MagickImage_Clone(value, exception);
}

MAGICK_NET_EXPORT size_t DrawingSettings_FillRule_Get(const DrawInfo *instance)
{
  return instance->fill_rule;
}

MAGICK_NET_EXPORT void DrawingSettings_FillRule_Set(DrawInfo *instance, const size_t value)
{
  instance->fill_rule = value;
}

MAGICK_NET_EXPORT const char *DrawingSettings_Font_Get(const DrawInfo *instance)
{
  return instance->font;
}

MAGICK_NET_EXPORT void DrawingSettings_Font_Set(DrawInfo *instance, const char *value)
{
  if (instance->font != (char *)NULL)
    instance->font = DestroyString(instance->font);
  if (value != (const char *)NULL)
    instance->font = ConstantString(value);
}

MAGICK_NET_EXPORT const char *DrawingSettings_FontFamily_Get(const DrawInfo *instance)
{
  return instance->family;
}

MAGICK_NET_EXPORT void DrawingSettings_FontFamily_Set(DrawInfo *instance, const char *value)
{
  if (instance->family != (char *)NULL)
    instance->family = DestroyString(instance->family);
  if (value != (const char *)NULL)
    instance->family = ConstantString(value);
}

MAGICK_NET_EXPORT const double DrawingSettings_FontPointsize_Get(const DrawInfo *instance)
{
  return instance->pointsize;
}

MAGICK_NET_EXPORT void DrawingSettings_FontPointsize_Set(DrawInfo *instance, const double value)
{
  instance->pointsize = value;
}

MAGICK_NET_EXPORT size_t DrawingSettings_FontStyle_Get(const DrawInfo *instance)
{
  return instance->style;
}

MAGICK_NET_EXPORT void DrawingSettings_FontStyle_Set(DrawInfo *instance, const size_t value)
{
  instance->style = (StyleType)value;
}

MAGICK_NET_EXPORT size_t DrawingSettings_FontWeight_Get(const DrawInfo *instance)
{
  return instance->weight;
}

MAGICK_NET_EXPORT void DrawingSettings_FontWeight_Set(DrawInfo *instance, const size_t value)
{
  instance->weight = value;
}

MAGICK_NET_EXPORT MagickBooleanType DrawingSettings_StrokeAntiAlias_Get(const DrawInfo *instance)
{
  return instance->stroke_antialias;
}

MAGICK_NET_EXPORT void DrawingSettings_StrokeAntiAlias_Set(DrawInfo *instance, const MagickBooleanType value)
{
  instance->stroke_antialias = value;
}

MAGICK_NET_EXPORT PixelInfo *DrawingSettings_StrokeColor_Get(const DrawInfo *instance)
{
  return MagickColor_Clone(&instance->stroke);
}

MAGICK_NET_EXPORT void DrawingSettings_StrokeColor_Set(DrawInfo *instance, const PixelInfo *value)
{
  if (value != (PixelInfo *)NULL)
    instance->stroke = *value;
}

MAGICK_NET_EXPORT double DrawingSettings_StrokeDashOffset_Get(const DrawInfo *instance)
{
  return instance->dash_offset;
}

MAGICK_NET_EXPORT void DrawingSettings_StrokeDashOffset_Set(DrawInfo *instance, const double value)
{
  instance->dash_offset = value;
}

MAGICK_NET_EXPORT size_t DrawingSettings_StrokeLineCap_Get(const DrawInfo *instance)
{
  return instance->linecap;
}

MAGICK_NET_EXPORT void DrawingSettings_StrokeLineCap_Set(DrawInfo  *instance, const size_t value)
{
  instance->linecap = value;
}

MAGICK_NET_EXPORT size_t DrawingSettings_StrokeLineJoin_Get(const DrawInfo *instance)
{
  return instance->linejoin;
}

MAGICK_NET_EXPORT void DrawingSettings_StrokeLineJoin_Set(DrawInfo  *instance, const size_t value)
{
  instance->linejoin = value;
}

MAGICK_NET_EXPORT size_t DrawingSettings_StrokeMiterLimit_Get(const DrawInfo *instance)
{
  return instance->miterlimit;
}

MAGICK_NET_EXPORT void DrawingSettings_StrokeMiterLimit_Set(DrawInfo  *instance, const size_t value)
{
  instance->miterlimit = value;
}

MAGICK_NET_EXPORT Image *DrawingSettings_StrokePattern_Get(const DrawInfo *instance, ExceptionInfo **exception)
{
  return MagickImage_Clone(instance->stroke_pattern, exception);
}

MAGICK_NET_EXPORT void DrawingSettings_StrokePattern_Set(DrawInfo *instance, const Image *value, ExceptionInfo **exception)
{
  if (instance->stroke_pattern != (Image *)NULL)
    instance->stroke_pattern = DestroyImage(instance->stroke_pattern);

  if (value != (const Image *)NULL)
    instance->stroke_pattern = MagickImage_Clone(value, exception);
}

MAGICK_NET_EXPORT double DrawingSettings_StrokeWidth_Get(const DrawInfo *instance)
{
  return instance->stroke_width;
}

MAGICK_NET_EXPORT void DrawingSettings_StrokeWidth_Set(DrawInfo *instance, const double value)
{
  instance->stroke_width = value;
}

MAGICK_NET_EXPORT MagickBooleanType DrawingSettings_TextAntiAlias_Get(const DrawInfo *instance)
{
  return instance->text_antialias;
}

MAGICK_NET_EXPORT void DrawingSettings_TextAntiAlias_Set(DrawInfo *instance, const MagickBooleanType value)
{
  instance->text_antialias = value;
}

MAGICK_NET_EXPORT size_t DrawingSettings_TextDirection_Get(const DrawInfo *instance)
{
  return instance->direction;
}

MAGICK_NET_EXPORT void DrawingSettings_TextDirection_Set(DrawInfo *instance, const size_t value)
{
  instance->direction = value;
}

MAGICK_NET_EXPORT const char *DrawingSettings_TextEncoding_Get(const DrawInfo *instance)
{
  return instance->encoding;
}

MAGICK_NET_EXPORT void DrawingSettings_TextEncoding_Set(DrawInfo *instance, const char *value)
{
  if (instance->encoding != (char *)NULL)
    instance->encoding = DestroyString(instance->encoding);
  if (value != (const char *)NULL)
    instance->encoding = ConstantString(value);
}

MAGICK_NET_EXPORT size_t DrawingSettings_TextGravity_Get(const DrawInfo *instance)
{
  return instance->gravity;
}

MAGICK_NET_EXPORT void DrawingSettings_TextGravity_Set(DrawInfo *instance, const size_t value)
{
  instance->gravity = value;
}

MAGICK_NET_EXPORT double DrawingSettings_TextInterlineSpacing_Get(const DrawInfo *instance)
{
  return instance->interline_spacing;
}

MAGICK_NET_EXPORT void DrawingSettings_TextInterlineSpacing_Set(DrawInfo *instance, const double value)
{
  instance->interline_spacing = value;
}

MAGICK_NET_EXPORT double DrawingSettings_TextInterwordSpacing_Get(const DrawInfo *instance)
{
  return instance->interword_spacing;
}

MAGICK_NET_EXPORT void DrawingSettings_TextInterwordSpacing_Set(DrawInfo  *instance, const double value)
{
  instance->interword_spacing = value;
}

MAGICK_NET_EXPORT double DrawingSettings_TextKerning_Get(const DrawInfo *instance)
{
  return instance->kerning;
}

MAGICK_NET_EXPORT void DrawingSettings_TextKerning_Set(DrawInfo  *instance, const double value)
{
  instance->kerning = value;
}

MAGICK_NET_EXPORT PixelInfo *DrawingSettings_TextUnderColor_Get(const DrawInfo *instance)
{
  return MagickColor_Clone(&instance->undercolor);
}

MAGICK_NET_EXPORT void DrawingSettings_TextUnderColor_Set(DrawInfo *instance, const PixelInfo *value)
{
  if (value != (PixelInfo *)NULL)
    instance->undercolor = *value;
}

MAGICK_NET_EXPORT const double *DrawingSettings_GetStrokeDashArray(DrawInfo *instance, size_t *length)
{
  *length = 0;
  if (instance->dash_pattern == (double *)NULL)
    return (double *)NULL;

  while (instance->dash_pattern[*length] != 0.0)
    (*length)++;

  return instance->dash_pattern;
}

MAGICK_NET_EXPORT void DrawingSettings_ResetTransform(DrawInfo *instance)
{
  ResetAffine(&(instance->affine));
}

MAGICK_NET_EXPORT void DrawingSettings_SetStrokeDashArray(DrawInfo *instance, const double *value, const size_t length)
{
  instance->dash_pattern = (double *)RelinquishMagickMemory(instance->dash_pattern);
  instance->dash_pattern = AcquireMagickMemory((length + 1) * sizeof(double));
  memcpy(instance->dash_pattern, value, length * sizeof(double));
  instance->dash_pattern[length] = 0.0;
}

MAGICK_NET_EXPORT void DrawingSettings_SetText(DrawInfo *instance, const char *value)
{
  if (instance->text != (char *)NULL)
    instance->text = DestroyString(instance->text);
  if (value != (const char *)NULL)
    instance->text = ConstantString(value);
}

MAGICK_NET_EXPORT void DrawingSettings_SetTransformOrigin(DrawInfo *instance, const double x, const double y)
{
  AffineMatrix
    affine,
    current;

  ResetAffine(&affine);

  affine.tx = x;
  affine.ty = y;

  current = instance->affine;
  instance->affine.sx = current.sx*affine.sx + current.ry*affine.rx;
  instance->affine.rx = current.rx*affine.sx + current.sy*affine.rx;
  instance->affine.ry = current.sx*affine.ry + current.ry*affine.sy;
  instance->affine.sy = current.rx*affine.ry + current.sy*affine.sy;
  instance->affine.tx = current.sx*affine.tx + current.ry*affine.ty + current.tx;
  instance->affine.ty = current.rx*affine.tx + current.sy*affine.ty + current.ty;
}

MAGICK_NET_EXPORT void DrawingSettings_SetTransformRotation(DrawInfo *instance, const double angle)
{
  AffineMatrix
    affine,
    current;

  ResetAffine(&affine);

  current = instance->affine;
  affine.sx = cos(DegreesToRadians(fmod(angle, 360.0)));
  affine.rx = (-sin(DegreesToRadians(fmod(angle, 360.0))));
  affine.ry = sin(DegreesToRadians(fmod(angle, 360.0)));
  affine.sy = cos(DegreesToRadians(fmod(angle, 360.0)));

  instance->affine.sx = current.sx*affine.sx + current.ry*affine.rx;
  instance->affine.rx = current.rx*affine.sx + current.sy*affine.rx;
  instance->affine.ry = current.sx*affine.ry + current.ry*affine.sy;
  instance->affine.sy = current.rx*affine.ry + current.sy*affine.sy;
  instance->affine.tx = current.sx*affine.tx + current.ry*affine.ty + current.tx;
  instance->affine.ty = current.rx*affine.tx + current.sy*affine.ty + current.ty;
}

MAGICK_NET_EXPORT void DrawingSettings_SetTransformScale(DrawInfo *instance, const double x, const double y)
{
  AffineMatrix
    affine,
    current;

  ResetAffine(&affine);

  affine.sx = x;
  affine.sy = y;

  current = instance->affine;
  instance->affine.sx = current.sx*affine.sx + current.ry*affine.rx;
  instance->affine.rx = current.rx*affine.sx + current.sy*affine.rx;
  instance->affine.ry = current.sx*affine.ry + current.ry*affine.sy;
  instance->affine.sy = current.rx*affine.ry + current.sy*affine.sy;
  instance->affine.tx = current.sx*affine.tx + current.ry*affine.ty + current.tx;
  instance->affine.ty = current.rx*affine.tx + current.sy*affine.ty + current.ty;
}

MAGICK_NET_EXPORT void DrawingSettings_SetTransformSkewX(DrawInfo *instance, const double value)
{
  AffineMatrix
    affine,
    current;

  ResetAffine(&affine);

  affine.sx = 1.0;
  affine.ry = tan(DegreesToRadians(fmod(value, 360.0)));
  affine.sy = 1.0;

  current = instance->affine;
  instance->affine.sx = current.sx*affine.sx + current.ry*affine.rx;
  instance->affine.rx = current.rx*affine.sx + current.sy*affine.rx;
  instance->affine.ry = current.sx*affine.ry + current.ry*affine.sy;
  instance->affine.sy = current.rx*affine.ry + current.sy*affine.sy;
  instance->affine.tx = current.sx*affine.tx + current.ry*affine.ty + current.tx;
  instance->affine.ty = current.rx*affine.tx + current.sy*affine.ty + current.ty;
}

MAGICK_NET_EXPORT void DrawingSettings_SetTransformSkewY(DrawInfo *instance, const double value)
{
  AffineMatrix
    affine,
    current;

  ResetAffine(&affine);

  affine.sx = 1.0;
  affine.rx = tan(DegreesToRadians(fmod(value, 360.0)));
  affine.sy = 1.0;

  current = instance->affine;
  instance->affine.sx = current.sx*affine.sx + current.ry*affine.rx;
  instance->affine.rx = current.rx*affine.sx + current.sy*affine.rx;
  instance->affine.ry = current.sx*affine.ry + current.ry*affine.sy;
  instance->affine.sy = current.rx*affine.ry + current.sy*affine.sy;
  instance->affine.tx = current.sx*affine.tx + current.ry*affine.ty + current.tx;
  instance->affine.ty = current.rx*affine.tx + current.sy*affine.ty + current.ty;
}