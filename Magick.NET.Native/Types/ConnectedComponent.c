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

#include "Stdafx.h"
#include "ConnectedComponent.h"

MAGICK_NET_EXPORT void ConnectedComponent_DisposeList(CCObjectInfo *list)
{
  RelinquishMagickMemory(list);
}

MAGICK_NET_EXPORT size_t ConnectedComponent_GetHeight(const CCObjectInfo *instance)
{
  return instance->bounding_box.height;
}

MAGICK_NET_EXPORT size_t ConnectedComponent_GetWidth(const CCObjectInfo *instance)
{
  return instance->bounding_box.width;
}

MAGICK_NET_EXPORT ssize_t ConnectedComponent_GetX(const CCObjectInfo *instance)
{
  return instance->bounding_box.x;
}

MAGICK_NET_EXPORT ssize_t ConnectedComponent_GetY(const CCObjectInfo *instance)
{
  return instance->bounding_box.y;
}

MAGICK_NET_EXPORT const CCObjectInfo *ConnectedComponent_GetInstance(const CCObjectInfo *list, const size_t index)
{
  return list + index;
}