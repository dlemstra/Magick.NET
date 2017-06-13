//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

MAGICK_NET_EXPORT OffsetInfo *OffsetInfo_Create(void);

MAGICK_NET_EXPORT void OffsetInfo_Dispose(OffsetInfo *);

MAGICK_NET_EXPORT void OffsettInfo_SetX(OffsetInfo *, const size_t);

MAGICK_NET_EXPORT void OffsetInfo_SetY(OffsetInfo *, const size_t);