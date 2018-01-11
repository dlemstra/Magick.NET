// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
#pragma once

MAGICK_NET_EXPORT const char *MagickNET_Features_Get(void);

MAGICK_NET_EXPORT const TypeInfo **MagickNET_GetFontFamilies(size_t *, ExceptionInfo **);

MAGICK_NET_EXPORT const char *MagickNET_GetFontFamily(const TypeInfo **, const size_t);

MAGICK_NET_EXPORT void MagickNET_DisposeFontFamilies(TypeInfo **);

MAGICK_NET_EXPORT void MagickNET_SetLogDelegate(const MagickLogMethod);

MAGICK_NET_EXPORT void MagickNET_SetLogEvents(const char *);

MAGICK_NET_EXPORT void MagickNET_SetRandomSeed(const unsigned long);

MAGICK_NET_EXPORT MagickBooleanType MagickNET_SetOpenCLEnabled(const MagickBooleanType);