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
#include "PixelCollection.h"

#define ExportStart(type) \
  type \
    *result; \
  size_t \
    length; \
  length = width*height*strlen(mapping)*sizeof(type);

MAGICK_NET_EXPORT CacheView *PixelCollection_Create(const Image *image, ExceptionInfo **exception)
{
  CacheView
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = AcquireAuthenticCacheView(image, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void PixelCollection_Dispose(CacheView *instance)
{
  DestroyCacheView(instance);
}

MAGICK_NET_EXPORT const Quantum *PixelCollection_GetArea(const CacheView *instance, const size_t x, const size_t y, const size_t width, const size_t height, ExceptionInfo **exception)
{
  const Quantum
    *pixels;

  MAGICK_NET_GET_EXCEPTION;
  pixels = GetCacheViewVirtualPixels(instance, x, y, width, height, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return pixels;
}

MAGICK_NET_EXPORT void PixelCollection_SetArea(CacheView *instance, const size_t x, const  size_t y, const size_t width, const size_t height, const Quantum *values, const size_t length, ExceptionInfo **exception)
{
  const Quantum
    *q;

  const Image
    *image;

  size_t
    len,
    r,
    size;

  Quantum
    *pixels;

  MAGICK_NET_GET_EXCEPTION;
  image = GetCacheViewImage(instance);
  q = values;
  size = image->number_channels*width;
  for (r = 0; r < height; r++)
  {
    pixels = QueueCacheViewAuthenticPixels(instance, x, y + r, width, 1, exceptionInfo);
    if (pixels == (Quantum *)NULL)
      break;
    len = length - (size*r);
    memcpy(pixels, q, (size < len ? size : len)*sizeof(*pixels));
    if (SyncCacheViewAuthenticPixels(instance, exceptionInfo) == MagickFalse)
      break;
    if (size > len)
      break;
    q += size;
  }
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT unsigned char *PixelCollection_ToByteArray(const CacheView *instance, const size_t x, const size_t y, const size_t width, const size_t height, const char *mapping, ExceptionInfo **exception)
{
  ExportStart(unsigned char);
  result = AcquireMagickMemory(length);
  MAGICK_NET_GET_EXCEPTION;
  ExportImagePixels(GetCacheViewImage(instance), x, y, width, height, mapping, CharPixel, result, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}