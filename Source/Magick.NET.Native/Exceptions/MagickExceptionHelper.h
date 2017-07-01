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

#define MAGICK_NET_GET_EXCEPTION \
  { \
    ExceptionInfo \
      *exceptionInfo; \
    exceptionInfo = AcquireExceptionInfo()

#define MAGICK_NET_DESTROY_EXCEPTION \
    exceptionInfo = DestroyExceptionInfo(exceptionInfo); \
  }

#define MAGICK_NET_RAISE_EXCEPTION(type, message) \
    ThrowException(exceptionInfo, type, message, (const char *)NULL); \
    *exception = exceptionInfo

#define MAGICK_NET_SET_EXCEPTION \
    if (exceptionInfo->severity != UndefinedException) \
      *exception = exceptionInfo; \
    else \
      exceptionInfo = DestroyExceptionInfo(exceptionInfo); \
  }

MAGICK_NET_EXPORT const char *MagickExceptionHelper_Description(const ExceptionInfo *);

MAGICK_NET_EXPORT void MagickExceptionHelper_Dispose(ExceptionInfo *);

MAGICK_NET_EXPORT const char *MagickExceptionHelper_Message(const ExceptionInfo *);

MAGICK_NET_EXPORT const ExceptionInfo *MagickExceptionHelper_Related(const ExceptionInfo *, const size_t);

MAGICK_NET_EXPORT size_t MagickExceptionHelper_RelatedCount(const ExceptionInfo *);

MAGICK_NET_EXPORT ExceptionType MagickExceptionHelper_Severity(const ExceptionInfo *);