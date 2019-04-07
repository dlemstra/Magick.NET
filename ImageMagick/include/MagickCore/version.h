/*
  Copyright 1999-2018 ImageMagick Studio LLC, a non-profit organization
  dedicated to making software imaging solutions freely available.
  
  You may not use this file except in compliance with the License.
  obtain a copy of the License at
  
    http://www.imagemagick.org/script/license.php
  
  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.

  MagickCore version methods.
*/
#ifndef _MAGICKCORE_VERSION_H
#define _MAGICKCORE_VERSION_H

#if defined(__cplusplus) || defined(c_plusplus)
extern "C" {
#endif

#define MagickStringify(macro_or_string)  MagickStringifyArg(macro_or_string)
#define MagickStringifyArg(contents)  #contents

/*
  Define declarations.
*/
#define MagickPackageName "ImageMagick"
#define MagickCopyright  "Copyright (C) 1999-2018 ImageMagick Studio LLC"
#define MagickLibVersion  0x708
#define MagickLibVersionText  "7.0.8"
#define MagickLibVersionNumber  7,0,8,39
#define MagickLibAddendum  "-39"
#define MagickLibInterface  6
#define MagickLibMinInterface  0
#if defined(_WIN64)
#  define MagickPlatform "x64"
#else
#  define MagickPlatform "x86"
#endif
#define MagickReleaseDate  "2019-04-07"
#define MagickAuthoritativeLicense  \
  "http://www.imagemagick.org/script/license.php"
#define MagickAuthoritativeURL  "http://www.imagemagick.org"
#define MagickHomeURL  ""
#define MagickQuantumDepth "Q" MagickStringify(MAGICKCORE_QUANTUM_DEPTH)
#define MagickQuantumRange MagickStringify(QuantumRange)
#define MagickVersion  \
  MagickPackageName " " MagickLibVersionText MagickLibAddendum " " \
  MagickQuantumDepth " " MagickPlatform " " MagickReleaseDate " " \
  MagickAuthoritativeURL

extern MagickExport char
  *GetMagickHomeURL(void);

extern MagickExport const char
  *GetMagickCopyright(void),
  *GetMagickDelegates(void),
  *GetMagickFeatures(void),
  *GetMagickLicense(void),
  *GetMagickPackageName(void),
  *GetMagickQuantumDepth(size_t *),
  *GetMagickQuantumRange(size_t *),
  *GetMagickReleaseDate(void),
  *GetMagickVersion(size_t *);

extern MagickExport void
  ListMagickVersion(FILE *);

#if defined(__cplusplus) || defined(c_plusplus)
}
#endif

#endif
