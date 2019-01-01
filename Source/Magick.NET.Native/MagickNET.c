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
#include "MagickNET.h"

MAGICK_NET_EXPORT const char *MagickNET_Delegates_Get(void)
{
  return GetMagickDelegates();
}

MAGICK_NET_EXPORT const char *MagickNET_Features_Get(void)
{
  return GetMagickFeatures();
}

MAGICK_NET_EXPORT const TypeInfo **MagickNET_GetFontFamilies(size_t *length, ExceptionInfo **exception)
{
  const TypeInfo
    **font_families;

  MAGICK_NET_GET_EXCEPTION;
  font_families = GetTypeInfoList("*", length, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return font_families;
}

MAGICK_NET_EXPORT const char *MagickNET_GetFontFamily(const TypeInfo **list, const size_t index)
{
  if (list[index]->stealth != MagickFalse)
    return (const char *) NULL;

  return list[index]->family;
}

MAGICK_NET_EXPORT void MagickNET_DisposeFontFamilies(TypeInfo **list)
{
  RelinquishMagickMemory((void *) list);
}

MAGICK_NET_EXPORT void MagickNET_SetRandomSeed(const unsigned long seed)
{
  SetRandomSecretKey(seed);
}

MAGICK_NET_EXPORT void MagickNET_SetLogDelegate(const MagickLogMethod method)
{
  SetLogMethod(method);
}

MAGICK_NET_EXPORT void MagickNET_SetLogEvents(const char *events)
{
  SetLogEventMask(events);
}

MAGICK_NET_EXPORT MagickBooleanType MagickNET_SetOpenCLEnabled(const MagickBooleanType value)
{
  return SetOpenCLEnabled(value);
}