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

MAGICK_NET_EXPORT PrimaryInfo *PrimaryInfo_Create(void);

MAGICK_NET_EXPORT void PrimaryInfo_Dispose(PrimaryInfo *);

MAGICK_NET_EXPORT double PrimaryInfo_X_Get(const PrimaryInfo *);
MAGICK_NET_EXPORT void PrimaryInfo_X_Set(PrimaryInfo *, const double);

MAGICK_NET_EXPORT double PrimaryInfo_Y_Get(const PrimaryInfo *);
MAGICK_NET_EXPORT void PrimaryInfo_Y_Set(PrimaryInfo *, const double);

MAGICK_NET_EXPORT double PrimaryInfo_Z_Get(const PrimaryInfo *);
MAGICK_NET_EXPORT void PrimaryInfo_Z_Set(PrimaryInfo *, const double);
