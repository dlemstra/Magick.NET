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
#include "MagickGeometry.h"

MAGICK_NET_EXPORT GeometryInfo *MagickGeometry_Create(void)
{
  GeometryInfo
    *geometry_info;

  geometry_info = (GeometryInfo *)AcquireMagickMemory(sizeof(*geometry_info));
  if (geometry_info == (GeometryInfo *)NULL)
    return (GeometryInfo *)NULL;
  ResetMagickMemory(geometry_info, 0, sizeof(*geometry_info));
  return geometry_info;
}

MAGICK_NET_EXPORT void MagickGeometry_Dispose(GeometryInfo *instance)
{
  RelinquishMagickMemory(instance);
}

MAGICK_NET_EXPORT double MagickGeometry_X_Get(const GeometryInfo *instance)
{
  return instance->xi;
}

MAGICK_NET_EXPORT double MagickGeometry_Y_Get(const GeometryInfo *instance)
{
  return instance->psi;
}

MAGICK_NET_EXPORT double MagickGeometry_Width_Get(const GeometryInfo *instance)
{
  return instance->rho;
}

MAGICK_NET_EXPORT double MagickGeometry_Height_Get(const GeometryInfo *instance)
{
  return instance->sigma;
}

MAGICK_NET_EXPORT MagickStatusType MagickGeometry_Initialize(GeometryInfo *instance, const char *geometry)
{
  return ParseGeometry(geometry, instance);
}