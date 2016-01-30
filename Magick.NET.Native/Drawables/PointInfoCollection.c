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
#include "PointInfoCollection.h"

MAGICK_NET_EXPORT PointInfo *PointInfoCollection_Create(const size_t length)
{
  return (PointInfo *)AcquireMagickMemory(sizeof(PointInfo)*length);
}

MAGICK_NET_EXPORT void PointInfoCollection_Dispose(PointInfo *instance)
{
  RelinquishMagickMemory(instance);
}

MAGICK_NET_EXPORT void PointInfoCollection_Set(PointInfo *instance, const size_t index, const double x, const double y)
{
  instance[index].x = x;
  instance[index].y = y;
}