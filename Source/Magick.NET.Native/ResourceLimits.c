// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

#include "Stdafx.h"
#include "ResourceLimits.h"

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Disk_Get(void)
{
  return GetMagickResourceLimit(DiskResource);
}

MAGICK_NET_EXPORT void ResourceLimits_Disk_Set(const MagickSizeType limit)
{
  SetMagickResourceLimit(DiskResource, limit);
}

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Height_Get(void)
{
  return GetMagickResourceLimit(HeightResource);
}

MAGICK_NET_EXPORT void ResourceLimits_Height_Set(const MagickSizeType limit)
{
  SetMagickResourceLimit(HeightResource, limit);
}

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_ListLength_Get(void)
{
  return GetMagickResourceLimit(ListLengthResource);
}

MAGICK_NET_EXPORT void ResourceLimits_ListLength_Set(const MagickSizeType limit)
{
  SetMagickResourceLimit(ListLengthResource, limit);
}

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Memory_Get(void)
{
  return GetMagickResourceLimit(MemoryResource);
}

MAGICK_NET_EXPORT void ResourceLimits_Memory_Set(const MagickSizeType limit)
{
  SetMagickResourceLimit(AreaResource, limit);
  SetMagickResourceLimit(MemoryResource, limit);
}

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Thread_Get(void)
{
  return GetMagickResourceLimit(ThreadResource);
}

MAGICK_NET_EXPORT void ResourceLimits_Thread_Set(const MagickSizeType limit)
{
  SetMagickResourceLimit(ThreadResource, limit);
}

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Throttle_Get(void)
{
  return GetMagickResourceLimit(ThrottleResource);
}

MAGICK_NET_EXPORT void ResourceLimits_Throttle_Set(const MagickSizeType limit)
{
  SetMagickResourceLimit(ThrottleResource, limit);
}

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Width_Get(void)
{
  return GetMagickResourceLimit(WidthResource);
}

MAGICK_NET_EXPORT void ResourceLimits_Width_Set(const MagickSizeType limit)
{
  SetMagickResourceLimit(WidthResource, limit);;
}