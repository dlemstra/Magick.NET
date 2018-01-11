// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
#include "OpenCL.h"

MAGICK_NET_EXPORT MagickCLDevice *OpenCL_GetDevices(size_t *length)
{
  MagickCLDevice
    *devices;

  MAGICK_NET_GET_EXCEPTION;
  devices = GetOpenCLDevices(length, exceptionInfo);
  MAGICK_NET_DESTROY_EXCEPTION;
  return devices;
}

MAGICK_NET_EXPORT MagickCLDevice OpenCL_GetDevice(const MagickCLDevice *devices, const size_t index)
{
  return devices[index];
}

MAGICK_NET_EXPORT MagickBooleanType OpenCL_SetEnabled(const MagickBooleanType value)
{
  return SetOpenCLEnabled(value);
}