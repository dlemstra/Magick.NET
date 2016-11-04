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

MAGICK_NET_EXPORT TypeMetric *TypeMetric_Create(void);

MAGICK_NET_EXPORT void TypeMetric_Dispose(TypeMetric *);

MAGICK_NET_EXPORT const double TypeMetric_Ascent_Get(const TypeMetric *);

MAGICK_NET_EXPORT const double TypeMetric_Descent_Get(const TypeMetric *);

MAGICK_NET_EXPORT const double TypeMetric_MaxHorizontalAdvance_Get(const TypeMetric *);

MAGICK_NET_EXPORT const double TypeMetric_TextHeight_Get(const TypeMetric *);

MAGICK_NET_EXPORT const double TypeMetric_TextWidth_Get(const TypeMetric *);

MAGICK_NET_EXPORT const double TypeMetric_UnderlinePosition_Get(const TypeMetric *);

MAGICK_NET_EXPORT const double TypeMetric_UnderlineThickness_Get(const TypeMetric *);
