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

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Disk_Get(void);
MAGICK_NET_EXPORT void ResourceLimits_Disk_Set(const MagickSizeType);

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Height_Get(void);
MAGICK_NET_EXPORT void ResourceLimits_Height_Set(const MagickSizeType);

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Memory_Get(void);
MAGICK_NET_EXPORT void ResourceLimits_Memory_Set(const MagickSizeType);

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Thread_Get(void);
MAGICK_NET_EXPORT void ResourceLimits_Thread_Set(const MagickSizeType);

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Throttle_Get(void);
MAGICK_NET_EXPORT void ResourceLimits_Throttle_Set(const MagickSizeType);

MAGICK_NET_EXPORT MagickSizeType ResourceLimits_Width_Get(void);
MAGICK_NET_EXPORT void ResourceLimits_Width_Set(const MagickSizeType);