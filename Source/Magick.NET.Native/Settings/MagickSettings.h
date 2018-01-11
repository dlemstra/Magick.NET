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

MAGICK_NET_EXPORT ImageInfo *MagickSettings_Create(void);

MAGICK_NET_EXPORT void MagickSettings_Dispose(ImageInfo *);

MAGICK_NET_EXPORT PixelInfo *MagickSettings_BackgroundColor_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_BackgroundColor_Set(ImageInfo *, const  PixelInfo *);

MAGICK_NET_EXPORT size_t MagickSettings_ColorSpace_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_ColorSpace_Set(ImageInfo *, const size_t value);

MAGICK_NET_EXPORT size_t MagickSettings_ColorType_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_ColorType_Set(ImageInfo *, const size_t value);

MAGICK_NET_EXPORT size_t MagickSettings_Compression_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Compression_Set(ImageInfo *, const size_t);

MAGICK_NET_EXPORT const MagickBooleanType MagickSettings_Debug_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Debug_Set(ImageInfo *, const MagickBooleanType);

MAGICK_NET_EXPORT const char *MagickSettings_Density_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Density_Set(ImageInfo *, const char *);

MAGICK_NET_EXPORT size_t MagickSettings_Endian_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Endian_Set(ImageInfo *, const size_t);

MAGICK_NET_EXPORT const char *MagickSettings_Extract_Get(ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Extract_Set(ImageInfo *, const char *);

MAGICK_NET_EXPORT const char *MagickSettings_Format_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Format_Set(ImageInfo *, const char *);

MAGICK_NET_EXPORT const char *MagickSettings_Font_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Font_Set(ImageInfo *, const char *);

MAGICK_NET_EXPORT const double MagickSettings_FontPointsize_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_FontPointsize_Set(ImageInfo *, const double);

MAGICK_NET_EXPORT const size_t MagickSettings_Interlace_Get(const ImageInfo *instance);
MAGICK_NET_EXPORT void MagickSettings_Interlace_Set(ImageInfo *instance, const size_t value);

MAGICK_NET_EXPORT const MagickBooleanType MagickSettings_Monochrome_Get(ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Monochrome_Set(ImageInfo *, const MagickBooleanType);

MAGICK_NET_EXPORT MagickBooleanType MagickSettings_Verbose_Get(const ImageInfo *);
MAGICK_NET_EXPORT void MagickSettings_Verbose_Set(ImageInfo *, const MagickBooleanType);

MAGICK_NET_EXPORT void MagickSettings_SetColorFuzz(ImageInfo *, const double);

MAGICK_NET_EXPORT void MagickSettings_SetFileName(ImageInfo *, const char *);

MAGICK_NET_EXPORT void MagickSettings_SetInterlace(ImageInfo *, const size_t);

MAGICK_NET_EXPORT void MagickSettings_SetNumberScenes(ImageInfo *, const size_t);

MAGICK_NET_EXPORT void MagickSettings_SetOption(ImageInfo *, const char *, const char *);

MAGICK_NET_EXPORT void MagickSettings_SetPage(ImageInfo *, const char *);

MAGICK_NET_EXPORT void MagickSettings_SetPing(ImageInfo *, const MagickBooleanType);

MAGICK_NET_EXPORT void MagickSettings_SetQuality(ImageInfo *, const size_t);

MAGICK_NET_EXPORT void MagickSettings_SetScenes(ImageInfo *, const char *);

MAGICK_NET_EXPORT void MagickSettings_SetScene(ImageInfo *, const size_t);

MAGICK_NET_EXPORT void MagickSettings_SetSize(ImageInfo *, const char *);