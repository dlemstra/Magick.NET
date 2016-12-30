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
#include "MagickExceptionHelper.h"

static inline size_t AreExceptionsEqual(const ExceptionInfo *a, const ExceptionInfo *b)
{
  if ((a->severity != b->severity) ||
    (LocaleCompare(a->reason, b->reason) != 0) ||
    (LocaleCompare(a->description, b->description) != 0))
    return MagickFalse;

  return MagickTrue;
}

MAGICK_NET_EXPORT const char *MagickExceptionHelper_Description(const ExceptionInfo *instance)
{
  return instance->description;
}

MAGICK_NET_EXPORT void MagickExceptionHelper_Dispose(ExceptionInfo *instance)
{
  DestroyExceptionInfo(instance);
}

MAGICK_NET_EXPORT const ExceptionInfo *MagickExceptionHelper_Related(const ExceptionInfo *instance, const size_t idx)
{
  const ExceptionInfo
    *p,
    *q;

  size_t
    count,
    index;

  count = 0;
  if (instance->exceptions == (void *)NULL)
    return (ExceptionInfo *)NULL;

  q = instance;
  index = GetNumberOfElementsInLinkedList((LinkedListInfo *)instance->exceptions);
  while (index > 0)
  {
    p = (const ExceptionInfo *)GetValueFromLinkedList((LinkedListInfo *)instance->exceptions, --index);
    if (AreExceptionsEqual(p, q) == MagickFalse)
    {
      if (count == idx)
        return p;

      q = p;
      count++;
    }
  }

  return (ExceptionInfo *)NULL;
}

MAGICK_NET_EXPORT size_t MagickExceptionHelper_RelatedCount(const ExceptionInfo *instance)
{
  const ExceptionInfo
    *p,
    *q;

  size_t
    count,
    index;

  count = 0;
  if (instance->exceptions == (void *)NULL)
    return count;

  q = instance;
  index = GetNumberOfElementsInLinkedList((LinkedListInfo *)instance->exceptions);
  while (index > 0)
  {
    p = (const ExceptionInfo *)GetValueFromLinkedList((LinkedListInfo *)instance->exceptions, --index);
    if (AreExceptionsEqual(p, q) == MagickFalse)
    {
      q = p;
      count++;
    }
  }

  return count;
}

MAGICK_NET_EXPORT const char *MagickExceptionHelper_Message(const ExceptionInfo *instance)
{
  return instance->reason;
}

MAGICK_NET_EXPORT ExceptionType MagickExceptionHelper_Severity(const ExceptionInfo *instance)
{
  return instance->severity;
}