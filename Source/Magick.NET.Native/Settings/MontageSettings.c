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
#include "MontageSettings.h"

MAGICK_NET_EXPORT MontageInfo *MontageSettings_Create(void)
{
  MontageInfo
    *montage_info;

  ImageInfo
    *image_info;

  image_info=AcquireImageInfo();
  montage_info = AcquireMagickMemory(sizeof(*montage_info));
  GetMontageInfo(image_info, montage_info);
  DestroyImageInfo(image_info);
  return montage_info;
}

MAGICK_NET_EXPORT void MontageSettings_Dispose(MontageInfo *instance)
{
  DestroyMontageInfo(instance);
}

MAGICK_NET_EXPORT void MontageSettings_SetBackgroundColor(MontageInfo *instance, const PixelInfo *value)
{
  if (value != (PixelInfo*)NULL)
    instance->background_color = *value;
}

MAGICK_NET_EXPORT void MontageSettings_SetBorderColor(MontageInfo *instance, const PixelInfo *value)
{
  if (value != (PixelInfo*)NULL)
    instance->border_color = *value;
}

MAGICK_NET_EXPORT void MontageSettings_SetBorderWidth(MontageInfo *instance, const size_t value)
{
  instance->border_width = value;
}

MAGICK_NET_EXPORT void MontageSettings_SetFillColor(MontageInfo *instance, const PixelInfo *value)
{
  if (value != (PixelInfo*)NULL)
    instance->fill = *value;
}

MAGICK_NET_EXPORT void MontageSettings_SetFont(MontageInfo *instance, const char *value)
{
  CloneString(&instance->font, value);
}

MAGICK_NET_EXPORT void MontageSettings_SetFontPointsize(MontageInfo *instance, double value)
{
  instance->pointsize = value;
}

MAGICK_NET_EXPORT void MontageSettings_SetFrameGeometry(MontageInfo *instance, const char *value)
{
  CloneString(&instance->frame, value);
}

MAGICK_NET_EXPORT void MontageSettings_SetGeometry(MontageInfo *instance, const char *value)
{
  CloneString(&instance->geometry, value);
}

MAGICK_NET_EXPORT void MontageSettings_SetGravity(MontageInfo *instance, const size_t value)
{
  instance->gravity = (GravityType)value;
}

MAGICK_NET_EXPORT void MontageSettings_SetShadow(MontageInfo *instance, const MagickBooleanType value)
{
  instance->shadow = value;
}

MAGICK_NET_EXPORT void MontageSettings_SetStrokeColor(MontageInfo *instance, const PixelInfo *value)
{
  if (value != (PixelInfo*)NULL)
    instance->stroke = *value;
}

MAGICK_NET_EXPORT void MontageSettings_SetTextureFileName(MontageInfo *instance, const char *value)
{
  CloneString(&instance->texture, value);
}

MAGICK_NET_EXPORT void MontageSettings_SetTileGeometry(MontageInfo *instance, const char *value)
{
  CloneString(&instance->tile, value);
}

MAGICK_NET_EXPORT void MontageSettings_SetTitle(MontageInfo *instance, const char *value)
{
  CloneString(&instance->title, value);
}