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
#pragma once

MAGICK_NET_EXPORT MontageInfo *MontageSettings_Create(void);

MAGICK_NET_EXPORT void MontageSettings_Dispose(MontageInfo *);

MAGICK_NET_EXPORT void MontageSettings_SetBackgroundColor(MontageInfo *, const PixelInfo *);

MAGICK_NET_EXPORT void MontageSettings_SetBorderColor(MontageInfo *, const PixelInfo *);

MAGICK_NET_EXPORT void MontageSettings_SetBorderWidth(MontageInfo *, const size_t);

MAGICK_NET_EXPORT void MontageSettings_SetFillColor(MontageInfo *, const PixelInfo *);

MAGICK_NET_EXPORT void MontageSettings_SetFont(MontageInfo *, const char *);

MAGICK_NET_EXPORT void MontageSettings_SetFontPointsize(MontageInfo *, double);

MAGICK_NET_EXPORT void MontageSettings_SetFrameGeometry(MontageInfo *, const char *);

MAGICK_NET_EXPORT void MontageSettings_SetGeometry(MontageInfo *, const char *);

MAGICK_NET_EXPORT void MontageSettings_SetGravity(MontageInfo *, const size_t);

MAGICK_NET_EXPORT void MontageSettings_SetShadow(MontageInfo *, const MagickBooleanType);

MAGICK_NET_EXPORT void MontageSettings_SetStrokeColor(MontageInfo *, const PixelInfo *);

MAGICK_NET_EXPORT void MontageSettings_SetTextureFileName(MontageInfo *, const char *);

MAGICK_NET_EXPORT void MontageSettings_SetTileGeometry(MontageInfo *, const char *);

MAGICK_NET_EXPORT void MontageSettings_SetTitle(MontageInfo *, const char *);