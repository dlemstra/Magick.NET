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
#pragma once

#define _LIB

#pragma warning(disable : 4710)
#pragma warning(disable : 4711)
#pragma warning(disable : 4820)

#pragma warning(push)
#pragma warning(disable : 4255)
#pragma warning(disable : 4668)

#include "MagickCore/magick-config.h"

#include <MagickCore/MagickCore.h>
#include <MagickWand/MagickWand.h>

#pragma warning(pop)

#if defined(MAGICKCORE_BZLIB_DELEGATE)
#  pragma comment(lib, "CORE_RL_bzlib_.lib")
#endif
#pragma comment(lib, "CORE_RL_coders_.lib")
#if defined(MAGICKCORE_OPENEXR_DELEGATE)
#  pragma comment(lib, "CORE_RL_exr_.lib")
#endif
#if defined(MAGICKCORE_LQR_DELEGATE)
#  pragma comment(lib, "CORE_RL_ffi_.lib")
#endif
#pragma comment(lib, "CORE_RL_filters_.lib")
#if defined(MAGICKCORE_LQR_DELEGATE)
#  pragma comment(lib, "CORE_RL_glib_.lib")
#  pragma comment(lib, "winmm.lib")
#endif
#if defined(MAGICKCORE_JBIG_DELEGATE)
#  pragma comment(lib, "CORE_RL_jbig_.lib")
#endif
#if defined(MAGICKCORE_JP2_DELEGATE)
#  pragma comment(lib, "CORE_RL_jp2_.lib")
#endif
#if defined(MAGICKCORE_JPEG_DELEGATE)
#  pragma comment(lib, "CORE_RL_jpeg_.lib")
#endif
#if defined(MAGICKCORE_LCMS_DELEGATE)
#  pragma comment(lib, "CORE_RL_lcms_.lib")
#endif
#if defined(MAGICKCORE_LIBOPENJP2_DELEGATE)
#  pragma comment(lib, "CORE_RL_openjpeg_.lib")
#endif
#pragma comment(lib, "CORE_RL_libxml_.lib")
#if defined(MAGICKCORE_LQR_DELEGATE)
#  pragma comment(lib, "CORE_RL_lqr_.lib")
#endif
#pragma comment(lib, "CORE_RL_MagickCore_.lib")
#pragma comment(lib, "CORE_RL_MagickWand_.lib")
#if defined(MAGICKCORE_PANGOCAIRO_DELEGATE)
#  pragma comment(lib, "CORE_RL_cairo_.lib")
#  pragma comment(lib, "CORE_RL_pango_.lib")
#  pragma comment(lib, "CORE_RL_pixman_.lib")
#endif
#if defined(MAGICKCORE_PNG_DELEGATE)
#  pragma comment(lib, "CORE_RL_png_.lib")
#endif
#if defined(MAGICKCORE_RSVG_DELEGATE)
#  pragma comment(lib, "CORE_RL_croco_.lib")
#  pragma comment(lib, "CORE_RL_librsvg_.lib")
#endif
#if defined(MAGICKCORE_TIFF_DELEGATE)
#  pragma comment(lib, "CORE_RL_tiff_.lib")
#endif
#if defined(MAGICKCORE_FREETYPE_DELEGATE)
#  pragma comment(lib, "CORE_RL_ttf_.lib")
#endif
#if defined(MAGICKCORE_WEBP_DELEGATE)
#  pragma comment(lib, "CORE_RL_webp_.lib")
#endif
#if defined(MAGICKCORE_ZLIB_DELEGATE)
#  pragma comment(lib, "CORE_RL_zlib_.lib")
#endif
#pragma comment(lib, "ws2_32.lib")
#pragma comment(lib, "urlmon.lib")

#define MAGICK_NET_EXPORT __declspec(dllexport)

#include "Exceptions\MagickExceptionHelper.h"