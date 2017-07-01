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

#include "Stdafx.h"
#include "DrawingSettings.h"
#include "Colors/MagickColor.h"
#include "MagickImage.h"

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
  CloneString(&instance->font, value);
}

MAGICK_NET_EXPORT const char *DrawingSettings_FontFamily_Get(const DrawInfo *instance)
{
  return instance->family;
}

MAGICK_NET_EXPORT void DrawingSettings_FontFamily_Set(DrawInfo *instance, const char *value)
{
  CloneString(&instance->family, value);
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
  CloneString(&instance->encoding, value);
}

MAGICK_NET_EXPORT size_t DrawingSettings_TextGravity_Get(const DrawInfo *instance)
{
  return (size_t)instance->gravity;
}

MAGICK_NET_EXPORT void DrawingSettings_TextGravity_Set(DrawInfo *instance, const size_t value)
{
  instance->gravity = (GravityType)value;
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

MAGICK_NET_EXPORT void DrawingSettings_SetAffine(DrawInfo *instance, const double scaleX, const double scaleY, const double shearX, const double shearY, const double translateX, const double translateY)
{
  instance->affine.sx = scaleX;
  instance->affine.sy = scaleY;
  instance->affine.rx = shearX;
  instance->affine.ry = shearY;
  instance->affine.tx = translateX;
  instance->affine.ty = translateY;
}

MAGICK_NET_EXPORT void DrawingSettings_SetFillPattern(DrawInfo *instance, const Image *value, ExceptionInfo **exception)
{
  if (instance->fill_pattern != (Image *)NULL)
    instance->fill_pattern = DestroyImage(instance->fill_pattern);

  if (value != (const Image *)NULL)
    instance->fill_pattern = MagickImage_Clone(value, exception);
}

MAGICK_NET_EXPORT void DrawingSettings_SetStrokeDashArray(DrawInfo *instance, const double *value, const size_t length)
{
  MagickBooleanType
    isTerminated;

  instance->dash_pattern = (double *)RelinquishMagickMemory(instance->dash_pattern);
  isTerminated = (length > 0 && value[length - 1] == 0.0) ? MagickTrue : MagickFalse;
  instance->dash_pattern = AcquireMagickMemory((length + (isTerminated == MagickFalse ? 1 : 0)) * sizeof(double));
  if (length > 0)
    memcpy(instance->dash_pattern, value, length * sizeof(double));
  if (isTerminated == MagickFalse)
    instance->dash_pattern[length] = 0.0;
}

MAGICK_NET_EXPORT void DrawingSettings_SetStrokePattern(DrawInfo *instance, const Image *value, ExceptionInfo **exception)
{
  if (instance->stroke_pattern != (Image *)NULL)
    instance->stroke_pattern = DestroyImage(instance->stroke_pattern);

  if (value != (const Image *)NULL)
    instance->stroke_pattern = MagickImage_Clone(value, exception);
}

MAGICK_NET_EXPORT void DrawingSettings_SetText(DrawInfo *instance, const char *value)
{
  CloneString(&instance->text, value);
}