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

#include "Stdafx.h"
#include "OpenCLDevice.h"

MAGICK_NET_EXPORT const MagickCLDeviceType OpenCLDevice_DeviceType_Get(const MagickCLDevice device)
{
  return GetOpenCLDeviceType(device);
}

MAGICK_NET_EXPORT double OpenCLDevice_BenchmarkScore_Get(const MagickCLDevice device)
{
  return GetOpenCLDeviceBenchmarkScore(device);
}

MAGICK_NET_EXPORT MagickBooleanType OpenCLDevice_IsEnabled_Get(const MagickCLDevice device)
{
  return GetOpenCLDeviceEnabled(device);
}

MAGICK_NET_EXPORT void OpenCLDevice_IsEnabled_Set(const MagickCLDevice device, const MagickBooleanType value)
{
  SetOpenCLDeviceEnabled(device, value);
}

MAGICK_NET_EXPORT const char *OpenCLDevice_Name_Get(const MagickCLDevice device)
{
  return GetOpenCLDeviceName(device);
}

MAGICK_NET_EXPORT const char *OpenCLDevice_Version_Get(const MagickCLDevice device)
{
  return GetOpenCLDeviceVersion(device);
}

MAGICK_NET_EXPORT const KernelProfileRecord *OpenCLDevice_GetKernelProfileRecords(const MagickCLDevice device, size_t *length)
{
  return GetOpenCLKernelProfileRecords(device, length);
}

MAGICK_NET_EXPORT const KernelProfileRecord OpenCLDevice_GetKernelProfileRecord(const KernelProfileRecord *records, const size_t index)
{
  return records[index];
}

MAGICK_NET_EXPORT void OpenCLDevice_SetProfileKernels(const MagickCLDevice device, const MagickBooleanType value)
{
  SetOpenCLKernelProfileEnabled(device, value);
}