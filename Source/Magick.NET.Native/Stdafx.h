// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.
#pragma once

#define _LIB

#pragma warning(disable : 4710)
#pragma warning(disable : 4711)
#pragma warning(disable : 4820)

#pragma warning(push)
#pragma warning(disable : 4255)
#pragma warning(disable : 4668)
#pragma warning(disable : 4996)

#include "MagickCore/magick-config.h"

#include <MagickCore/MagickCore.h>
#include <MagickWand/MagickWand.h>
#include <MagickCore/utility-private.h>

#pragma warning(pop)

#include <string.h>

#define MAGICK_NET_STRINGIFY(s) #s
#if defined(_DEBUG)
#define MAGICK_NET_LINK_LIB(name) \
 __pragma(comment(lib,MAGICK_NET_STRINGIFY(CORE_DB_##name##_.lib)))
#else
#define MAGICK_NET_LINK_LIB(name) \
 __pragma(comment(lib,MAGICK_NET_STRINGIFY(CORE_RL_##name##_.lib)))
#endif

#define MAGICK_NET_EXPORT __declspec(dllexport)

#if defined(MAGICKCORE_BZLIB_DELEGATE)
MAGICK_NET_LINK_LIB("bzlib")
#endif

MAGICK_NET_LINK_LIB("coders")

#if defined(MAGICKCORE_OPENEXR_DELEGATE)
MAGICK_NET_LINK_LIB("exr")
#endif

#if defined(MAGICKCORE_LQR_DELEGATE)
MAGICK_NET_LINK_LIB("ffi")
#endif

#if defined(MAGICKCORE_FLIF_DELEGATE)
MAGICK_NET_LINK_LIB("flif")
#endif

#if defined(MAGICKCORE_LQR_DELEGATE)
MAGICK_NET_LINK_LIB("glib")
#pragma comment(lib, "winmm.lib")
#endif

#if defined(MAGICKCORE_JBIG_DELEGATE)
MAGICK_NET_LINK_LIB("jbig")
#endif

#if defined(MAGICKCORE_JP2_DELEGATE)
MAGICK_NET_LINK_LIB("jp2")
#endif

#if defined(MAGICKCORE_JPEG_DELEGATE)
MAGICK_NET_LINK_LIB("jpeg")
#endif

#if defined(MAGICKCORE_LCMS_DELEGATE)
MAGICK_NET_LINK_LIB("lcms")
#endif

#if defined(MAGICKCORE_LIBOPENJP2_DELEGATE)
MAGICK_NET_LINK_LIB("openjpeg")
#endif

MAGICK_NET_LINK_LIB("libxml")

#if defined(MAGICKCORE_LQR_DELEGATE)
MAGICK_NET_LINK_LIB("lqr")
#endif

MAGICK_NET_LINK_LIB("MagickCore")
MAGICK_NET_LINK_LIB("MagickWand")

#if defined(MAGICKCORE_PANGOCAIRO_DELEGATE)
MAGICK_NET_LINK_LIB("cairo")
MAGICK_NET_LINK_LIB("pango")
MAGICK_NET_LINK_LIB("pixman")
#endif

#if defined(MAGICKCORE_PNG_DELEGATE)
MAGICK_NET_LINK_LIB("png")
#endif

#if defined(MAGICKCORE_RSVG_DELEGATE)
MAGICK_NET_LINK_LIB("croco")
MAGICK_NET_LINK_LIB("librsvg")
#endif

#if defined(MAGICKCORE_TIFF_DELEGATE)
MAGICK_NET_LINK_LIB("tiff")
#endif

#if defined(MAGICKCORE_FREETYPE_DELEGATE)
MAGICK_NET_LINK_LIB("ttf")
#endif

#if defined(MAGICKCORE_WEBP_DELEGATE)
MAGICK_NET_LINK_LIB("webp")
#endif

#if defined(MAGICKCORE_ZLIB_DELEGATE)
MAGICK_NET_LINK_LIB("zlib")
#endif

#pragma comment(lib, "ws2_32.lib")
#pragma comment(lib, "urlmon.lib")

#include "Exceptions\MagickExceptionHelper.h"