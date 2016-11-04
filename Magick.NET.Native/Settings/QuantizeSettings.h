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

MAGICK_NET_EXPORT QuantizeInfo *QuantizeSettings_Create(void);

MAGICK_NET_EXPORT void QuantizeSettings_Dispose(QuantizeInfo *);

MAGICK_NET_EXPORT void QuantizeSettings_SetColors(QuantizeInfo *, const size_t);

MAGICK_NET_EXPORT void QuantizeSettings_SetColorSpace(QuantizeInfo *, const size_t);

MAGICK_NET_EXPORT void QuantizeSettings_SetDitherMethod(QuantizeInfo *, const size_t);

MAGICK_NET_EXPORT void QuantizeSettings_SetMeasureErrors(QuantizeInfo *, const MagickBooleanType);

MAGICK_NET_EXPORT void QuantizeSettings_SetTreeDepth(QuantizeInfo *, const size_t);