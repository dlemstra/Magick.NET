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

MAGICK_NET_EXPORT PixelInfo *MagickColor_Create(void);

MAGICK_NET_EXPORT void MagickColor_Dispose(PixelInfo *instance);

MAGICK_NET_EXPORT MagickSizeType MagickColor_Count_Get(const PixelInfo *);

MAGICK_NET_EXPORT Quantum MagickColor_Red_Get(const PixelInfo *);
MAGICK_NET_EXPORT void MagickColor_Red_Set(PixelInfo *, const Quantum);

MAGICK_NET_EXPORT Quantum MagickColor_Green_Get(const PixelInfo *);
MAGICK_NET_EXPORT void MagickColor_Green_Set(PixelInfo *, const Quantum);

MAGICK_NET_EXPORT Quantum MagickColor_Blue_Get(const PixelInfo *);
MAGICK_NET_EXPORT void MagickColor_Blue_Set(PixelInfo *, const Quantum);

MAGICK_NET_EXPORT Quantum MagickColor_Alpha_Get(const PixelInfo *);
MAGICK_NET_EXPORT void MagickColor_Alpha_Set(PixelInfo *, const Quantum);

MAGICK_NET_EXPORT Quantum MagickColor_Black_Get(const PixelInfo *);
MAGICK_NET_EXPORT void MagickColor_Black_Set(PixelInfo *, const Quantum);

MAGICK_NET_EXPORT PixelInfo *MagickColor_Clone(const PixelInfo *);

MAGICK_NET_EXPORT MagickBooleanType MagickColor_FuzzyEquals(const PixelInfo *, const PixelInfo *, const Quantum fuzz);

MAGICK_NET_EXPORT MagickBooleanType MagickColor_Initialize(PixelInfo *, const char *value);