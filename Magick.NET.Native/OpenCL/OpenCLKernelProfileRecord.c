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
#include "OpenCLKernelProfileRecord.h"

MAGICK_NET_EXPORT unsigned long OpenCLKernelProfileRecord_Count_Get(const KernelProfileRecord record)
{
  return record->count;
}

MAGICK_NET_EXPORT const char *OpenCLKernelProfileRecord_Name_Get(const KernelProfileRecord record)
{
  return record->kernel_name;
}

MAGICK_NET_EXPORT unsigned long OpenCLKernelProfileRecord_MaximumDuration_Get(const KernelProfileRecord record)
{
  return record->max;
}

MAGICK_NET_EXPORT unsigned long OpenCLKernelProfileRecord_MinimumDuration_Get(const KernelProfileRecord record)
{
  return record->min;
}

MAGICK_NET_EXPORT unsigned long OpenCLKernelProfileRecord_TotalDuration_Get(const KernelProfileRecord record)
{
  return record->total;
}