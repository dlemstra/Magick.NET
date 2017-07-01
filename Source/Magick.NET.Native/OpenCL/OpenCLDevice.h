// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
#pragma once

MAGICK_NET_EXPORT const MagickCLDeviceType OpenCLDevice_DeviceType_Get(const MagickCLDevice);

MAGICK_NET_EXPORT double OpenCLDevice_BenchmarkScore_Get(const MagickCLDevice);

MAGICK_NET_EXPORT MagickBooleanType OpenCLDevice_IsEnabled_Get(const MagickCLDevice);
MAGICK_NET_EXPORT void OpenCLDevice_IsEnabled_Set(const MagickCLDevice, const MagickBooleanType);

MAGICK_NET_EXPORT const char *OpenCLDevice_Name_Get(const MagickCLDevice);

MAGICK_NET_EXPORT const char *OpenCLDevice_Version_Get(const MagickCLDevice);

MAGICK_NET_EXPORT const KernelProfileRecord *OpenCLDevice_GetKernelProfileRecords(const MagickCLDevice, size_t *);

MAGICK_NET_EXPORT const KernelProfileRecord OpenCLDevice_GetKernelProfileRecord(const KernelProfileRecord *, const size_t);

MAGICK_NET_EXPORT void OpenCLDevice_SetProfileKernels(const MagickCLDevice, const MagickBooleanType);