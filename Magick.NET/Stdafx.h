//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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

#pragma comment(lib, "CORE_RL_bzlib_.lib")
#pragma comment(lib, "CORE_RL_coders_.lib")
#pragma comment(lib, "CORE_RL_filters_.lib")
#pragma comment(lib, "CORE_RL_jbig_.lib")
#pragma comment(lib, "CORE_RL_jp2_.lib")
#pragma comment(lib, "CORE_RL_jpeg_.lib")
#pragma comment(lib, "CORE_RL_lcms_.lib")
#pragma comment(lib, "CORE_RL_libxml_.lib")
#pragma comment(lib, "CORE_RL_magick_.lib")
#pragma comment(lib, "CORE_RL_Magick++_.lib")
#pragma comment(lib, "CORE_RL_png_.lib")
#pragma comment(lib, "CORE_RL_tiff_.lib")
#pragma comment(lib, "CORE_RL_ttf_.lib")
#pragma comment(lib, "CORE_RL_wand_.lib")
#pragma comment(lib, "CORE_RL_wmf_.lib")
#pragma comment(lib, "CORE_RL_zlib_.lib")
#pragma comment(lib, "wsock32.lib")

#pragma warning(disable: 4244)

#define STATIC_MAGICK
#include "Magick++.h"

#pragma warning(default: 4244)

using namespace System;

#include "Helpers\Marshaller.h"
#include "Helpers\Throw.h"