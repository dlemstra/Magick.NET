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
#include "MagickFormatInfo.h"

static inline const MagickInfo *GetInfoByName(const char *name, ExceptionInfo **exception)
{
  const MagickInfo
    *info;

  MAGICK_NET_GET_EXCEPTION;
  info = GetMagickInfo(name, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return info;
}

MAGICK_NET_EXPORT char **MagickFormatInfo_CreateList(size_t *length, ExceptionInfo **exception)
{
  char
    **coder_list;

  MAGICK_NET_GET_EXCEPTION;
  coder_list = GetMagickList("*", length, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return coder_list;
}

MAGICK_NET_EXPORT void MagickFormatInfo_DisposeList(char **list, const size_t length)
{
  ssize_t
    i;

  for (i = 0; i < (ssize_t)length; i++)
  {
    list[i] = (char *)RelinquishMagickMemory(list[i]);
  }

  RelinquishMagickMemory(list);
}

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_CanReadMultithreaded_Get(const MagickInfo *instance)
{
  return GetMagickDecoderThreadSupport(instance);
}

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_CanWriteMultithreaded_Get(const MagickInfo *instance)
{
  return GetMagickEncoderThreadSupport(instance);
}

MAGICK_NET_EXPORT const char *MagickFormatInfo_Description_Get(const MagickInfo *instance)
{
  return GetMagickDescription(instance);
}

MAGICK_NET_EXPORT const char *MagickFormatInfo_Format_Get(const MagickInfo *instance)
{
  return instance->name;
}

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_IsMultiFrame_Get(const MagickInfo *instance)
{
  return GetMagickAdjoin(instance);
}

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_IsReadable_Get(const MagickInfo *instance)
{
  return GetImageDecoder(instance) != (DecodeImageHandler *)NULL;
}

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_IsWritable_Get(const MagickInfo *instance)
{
  return GetImageEncoder(instance) != (EncodeImageHandler *)NULL;
}

MAGICK_NET_EXPORT const char *MagickFormatInfo_MimeType_Get(const MagickInfo *instance)
{
  return GetMagickMimeType(instance);
}

MAGICK_NET_EXPORT const char *MagickFormatInfo_Module_Get(const MagickInfo *instance)
{
  return instance->module;
}

MAGICK_NET_EXPORT const MagickInfo *MagickFormatInfo_GetInfo(char **list, const size_t index, ExceptionInfo **exception)
{
  return GetInfoByName(list[index], exception);
}

MAGICK_NET_EXPORT const MagickInfo *MagickFormatInfo_GetInfoByName(const char *name, ExceptionInfo **exception)
{
  return GetInfoByName(name, exception);
}

MAGICK_NET_EXPORT void MagickFormatInfo_Unregister(const char *format)
{
  UnregisterMagickInfo(format);
}