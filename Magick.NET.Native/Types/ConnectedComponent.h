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

MAGICK_NET_EXPORT void ConnectedComponent_DisposeList(CCObjectInfo *);

MAGICK_NET_EXPORT double ConnectedComponent_GetArea(const CCObjectInfo *);

MAGICK_NET_EXPORT size_t ConnectedComponent_GetHeight(const CCObjectInfo *);

MAGICK_NET_EXPORT size_t ConnectedComponent_GetWidth(const CCObjectInfo *);

MAGICK_NET_EXPORT ssize_t ConnectedComponent_GetX(const CCObjectInfo *);

MAGICK_NET_EXPORT ssize_t ConnectedComponent_GetY(const CCObjectInfo *);

MAGICK_NET_EXPORT const CCObjectInfo *ConnectedComponent_GetInstance(const CCObjectInfo *, const size_t);