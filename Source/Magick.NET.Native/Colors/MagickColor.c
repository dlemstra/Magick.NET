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
#include "MagickColor.h"

MAGICK_NET_EXPORT PixelInfo *MagickColor_Create(void)
{
  PixelInfo
    *pixel_info;

  pixel_info = (PixelInfo *)AcquireMagickMemory(sizeof(*pixel_info));
  if (pixel_info == (PixelInfo *)NULL)
    return (PixelInfo *)NULL;
  GetPixelInfo((Image *)NULL, pixel_info);
  return pixel_info;
}

MAGICK_NET_EXPORT void MagickColor_Dispose(PixelInfo *instance)
{
  RelinquishMagickMemory(instance);
}

MAGICK_NET_EXPORT MagickSizeType MagickColor_Count_Get(const PixelInfo *instance)
{
  return instance->count;
}

MAGICK_NET_EXPORT Quantum MagickColor_Red_Get(const PixelInfo *instance)
{
  return (Quantum)instance->red;
}

MAGICK_NET_EXPORT void MagickColor_Red_Set(PixelInfo *instance, const Quantum value)
{
  instance->red = value;
}

MAGICK_NET_EXPORT Quantum MagickColor_Green_Get(const PixelInfo *instance)
{
  return (Quantum)instance->green;
}

MAGICK_NET_EXPORT void MagickColor_Green_Set(PixelInfo *instance, const Quantum value)
{
  instance->green = value;
}

MAGICK_NET_EXPORT Quantum MagickColor_Blue_Get(const PixelInfo *instance)
{
  return (Quantum)instance->blue;
}

MAGICK_NET_EXPORT void MagickColor_Blue_Set(PixelInfo *instance, const Quantum value)
{
  instance->blue = value;
}

MAGICK_NET_EXPORT Quantum MagickColor_Alpha_Get(const PixelInfo *instance)
{
  return (Quantum)instance->alpha;
}

MAGICK_NET_EXPORT void MagickColor_Alpha_Set(PixelInfo *instance, const Quantum value)
{
  instance->alpha = value;
  instance->alpha_trait = value != OpaqueAlpha ? BlendPixelTrait : UndefinedPixelTrait;
}

MAGICK_NET_EXPORT Quantum MagickColor_Black_Get(const PixelInfo *instance)
{
  return (Quantum)instance->black;
}

MAGICK_NET_EXPORT void MagickColor_Black_Set(PixelInfo *instance, const Quantum value)
{
  instance->black = value;
}

MAGICK_NET_EXPORT MagickBooleanType MagickColor_IsCMYK_Get(const PixelInfo *color)
{
  return color->colorspace == CMYKColorspace ? MagickTrue : MagickFalse;
}

MAGICK_NET_EXPORT PixelInfo *MagickColor_Clone(const PixelInfo *color)
{
  PixelInfo
    *pixel_info;

  if (color == (PixelInfo *)NULL)
    return (PixelInfo *)NULL;

  pixel_info = (PixelInfo *)AcquireMagickMemory(sizeof(*pixel_info));
  if (pixel_info == (PixelInfo *)NULL)
    return (PixelInfo *)NULL;

  *pixel_info = *color;
  return pixel_info;
}

MAGICK_NET_EXPORT MagickBooleanType MagickColor_FuzzyEquals(const PixelInfo *instance, const PixelInfo *other, const Quantum fuzz)
{
  PixelInfo
    p,
    q;

  p = *instance;
  p.fuzz = fuzz;
  q = *other;
  q.fuzz = fuzz;
  return IsFuzzyEquivalencePixelInfo(&p, &q);
}

MAGICK_NET_EXPORT MagickBooleanType MagickColor_Initialize(PixelInfo *instance, const char *value)
{
  MagickBooleanType
    status;

  PixelInfo
    target_color;

  MAGICK_NET_GET_EXCEPTION;
  status = QueryColorCompliance(value, AllCompliance, &target_color, exceptionInfo);
  if (status != MagickFalse)
    *instance = target_color;
  MAGICK_NET_DESTROY_EXCEPTION;
  return status;
}