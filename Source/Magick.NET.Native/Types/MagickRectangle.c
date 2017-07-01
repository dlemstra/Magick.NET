// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
#include "MagickRectangle.h"

MAGICK_NET_EXPORT RectangleInfo *MagickRectangle_Create(void)
{
  RectangleInfo
    *rectangle_info;

  rectangle_info = (RectangleInfo *)AcquireMagickMemory(sizeof(*rectangle_info));
  if (rectangle_info == (RectangleInfo *)NULL)
    return (RectangleInfo *)NULL;
  ResetMagickMemory(rectangle_info, 0, sizeof(*rectangle_info));
  return rectangle_info;
}

MAGICK_NET_EXPORT void MagickRectangle_Dispose(RectangleInfo *instance)
{
  RelinquishMagickMemory(instance);
}

MAGICK_NET_EXPORT ssize_t MagickRectangle_X_Get(const RectangleInfo *instance)
{
  return instance->x;
}

MAGICK_NET_EXPORT void MagickRectangle_X_Set(RectangleInfo *instance, const ssize_t value)
{
  instance->x = value;
}

MAGICK_NET_EXPORT ssize_t MagickRectangle_Y_Get(const RectangleInfo *instance)
{
  return instance->y;
}

MAGICK_NET_EXPORT void MagickRectangle_Y_Set(RectangleInfo *instance, const ssize_t value)
{
  instance->y = value;
}

MAGICK_NET_EXPORT size_t MagickRectangle_Width_Get(const RectangleInfo *instance)
{
  return instance->width;
}

MAGICK_NET_EXPORT void MagickRectangle_Width_Set(RectangleInfo *instance, const size_t value)
{
  instance->width = value;
}

MAGICK_NET_EXPORT size_t MagickRectangle_Height_Get(const RectangleInfo *instance)
{
  return instance->height;
}

MAGICK_NET_EXPORT void MagickRectangle_Height_Set(RectangleInfo *instance, const size_t value)
{
  instance->height = value;
}

MAGICK_NET_EXPORT void MagickRectangle_Initialize(RectangleInfo *instance, const char *value)
{
  GetGeometry(value, &instance->x, &instance->y, &instance->width, &instance->height);
}
