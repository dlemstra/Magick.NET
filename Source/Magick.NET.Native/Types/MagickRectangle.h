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

MAGICK_NET_EXPORT RectangleInfo *MagickRectangle_Create(void);

MAGICK_NET_EXPORT void MagickRectangle_Dispose(RectangleInfo *);

MAGICK_NET_EXPORT ssize_t MagickRectangle_X_Get(const RectangleInfo *);
MAGICK_NET_EXPORT void MagickRectangle_X_Set(RectangleInfo *, const ssize_t);

MAGICK_NET_EXPORT ssize_t MagickRectangle_Y_Get(const RectangleInfo *);
MAGICK_NET_EXPORT void MagickRectangle_Y_Set(RectangleInfo *, const ssize_t);

MAGICK_NET_EXPORT size_t MagickRectangle_Width_Get(const RectangleInfo *);
MAGICK_NET_EXPORT void MagickRectangle_Width_Set(RectangleInfo *, const size_t);

MAGICK_NET_EXPORT size_t MagickRectangle_Height_Get(const RectangleInfo *);
MAGICK_NET_EXPORT void MagickRectangle_Height_Set(RectangleInfo *, const size_t);

MAGICK_NET_EXPORT void MagickRectangle_Initialize(RectangleInfo *, const char *);