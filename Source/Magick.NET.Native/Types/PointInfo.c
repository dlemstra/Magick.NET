// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

#include "Stdafx.h"
#include "PointInfo.h"

MAGICK_NET_EXPORT double PointInfo_X_Get(const PointInfo *instance)
{
  return instance->x;
}

MAGICK_NET_EXPORT double PointInfo_Y_Get(const PointInfo *instance)
{
  return instance->y;
}