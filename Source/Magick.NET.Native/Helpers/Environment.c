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
#include "Environment.h"
#include <stdlib.h>

MAGICK_NET_EXPORT void Environment_Initialize(void)
{
  MagickCoreGenesis((const char *)NULL, MagickFalse);
  SetMagickResourceLimit(HeightResource, 10000000);
  SetMagickResourceLimit(WidthResource, 10000000);
}

MAGICK_NET_EXPORT void Environment_SetEnv(const char *name, const char *value)
{
#ifdef MAGICK_NET_LINUX
  (void) setenv(name, value, 1);
#else
  _putenv_s(name, value);
#endif
}