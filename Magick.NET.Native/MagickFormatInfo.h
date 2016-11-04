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

MAGICK_NET_EXPORT char **MagickFormatInfo_CreateList(size_t *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickFormatInfo_DisposeList(char **, const size_t);

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_CanReadMultithreaded_Get(const MagickInfo *);

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_CanWriteMultithreaded_Get(const MagickInfo *);

MAGICK_NET_EXPORT const char *MagickFormatInfo_Description_Get(const MagickInfo *);

MAGICK_NET_EXPORT const char *MagickFormatInfo_Format_Get(const MagickInfo *);

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_IsMultiFrame_Get(const MagickInfo *);

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_IsReadable_Get(const MagickInfo *);

MAGICK_NET_EXPORT MagickBooleanType MagickFormatInfo_IsWritable_Get(const MagickInfo *);

MAGICK_NET_EXPORT const char *MagickFormatInfo_MimeType_Get(const MagickInfo *);

MAGICK_NET_EXPORT const char *MagickFormatInfo_Module_Get(const MagickInfo *);

MAGICK_NET_EXPORT const MagickInfo *MagickFormatInfo_GetInfo(char **, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT const MagickInfo *MagickFormatInfo_GetInfoByName(const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickFormatInfo_Unregister(const char *format);