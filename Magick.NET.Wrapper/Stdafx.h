//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
#pragma once

#pragma warning(disable: 4820)
#pragma warning(disable: 4514)

#pragma warning(disable: 4244)

#define STATIC_MAGICK
#include "Magick++.h"

#pragma warning(default: 4244)

#if (MAGICKCORE_QUANTUM_DEPTH == 8)
#define QUANTUM_CLS_COMPLIANT
#elif (MAGICKCORE_QUANTUM_DEPTH == 16 && !defined(MAGICKCORE_HDRI_SUPPORT))
#define QUANTUM_CLS_COMPLIANT [CLSCompliantAttribute(false)]
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
#define QUANTUM_CLS_COMPLIANT
#else
#error Not implemented!
#endif

using namespace System;

#include "Helpers\Marshaller.h"
#include "Helpers\Throw.h"