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
#include "QuantizeSettings.h"

MAGICK_NET_EXPORT QuantizeInfo *QuantizeSettings_Create(void)
{
  return AcquireQuantizeInfo((const ImageInfo *)NULL);
}

MAGICK_NET_EXPORT void QuantizeSettings_Dispose(QuantizeInfo *instance)
{
  DestroyQuantizeInfo(instance);
}

MAGICK_NET_EXPORT void QuantizeSettings_SetColors(QuantizeInfo *instance, const size_t value)
{
  instance->number_colors = value;
}

MAGICK_NET_EXPORT void QuantizeSettings_SetColorSpace(QuantizeInfo *instance, const size_t value)
{
  instance->colorspace = (ColorspaceType)value;
}

MAGICK_NET_EXPORT void QuantizeSettings_SetDitherMethod(QuantizeInfo *instance, const size_t value)
{
  instance->dither_method = (DitherMethod)value;
}

MAGICK_NET_EXPORT void QuantizeSettings_SetMeasureErrors(QuantizeInfo *instance, const MagickBooleanType value)
{
  instance->measure_error = value;
}

MAGICK_NET_EXPORT void QuantizeSettings_SetTreeDepth(QuantizeInfo *instance, const size_t value)
{
  instance->tree_depth = value;
}