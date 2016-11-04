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

MAGICK_NET_EXPORT GeometryInfo *MagickGeometry_Create(void);

MAGICK_NET_EXPORT void MagickGeometry_Dispose(GeometryInfo *);

MAGICK_NET_EXPORT double MagickGeometry_X_Get(const GeometryInfo *);

MAGICK_NET_EXPORT double MagickGeometry_Y_Get(const GeometryInfo *);

MAGICK_NET_EXPORT double MagickGeometry_Width_Get(const GeometryInfo *);

MAGICK_NET_EXPORT double MagickGeometry_Height_Get(const GeometryInfo *);

MAGICK_NET_EXPORT MagickStatusType MagickGeometry_Initialize(GeometryInfo *, const char *);