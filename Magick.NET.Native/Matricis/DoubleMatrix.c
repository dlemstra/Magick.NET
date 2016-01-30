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
#include "DoubleMatrix.h"

MAGICK_NET_EXPORT KernelInfo *DoubleMatrix_Create(const double *values, const size_t order)
{
  KernelInfo
    *kernel;

  ssize_t
    i;

  MAGICK_NET_GET_EXCEPTION;
  kernel = AcquireKernelInfo((const char *)NULL, exceptionInfo);
  MAGICK_NET_DESTROY_EXCEPTION;

  if (kernel == (KernelInfo *)NULL)
    return (KernelInfo *)NULL;

  kernel->width = order;
  kernel->height = order;
  kernel->x = (ssize_t)(order - 1) / 2;
  kernel->y = (ssize_t)(order - 1) / 2;
  kernel->values = (MagickRealType *)AcquireAlignedMemory(order, order*sizeof(*kernel->values));
  if (kernel->values != (MagickRealType *)NULL)
  {
    for (i = 0; i < (ssize_t)(order*order); i++)
      kernel->values[i] = (MagickRealType)values[i];
  }

  return kernel;
}

MAGICK_NET_EXPORT void DoubleMatrix_Dispose(KernelInfo *instance)
{
  DestroyKernelInfo(instance);
}