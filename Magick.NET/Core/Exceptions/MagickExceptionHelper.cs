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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
  internal static partial class MagickExceptionHelper
  {
    private static MagickException CreateException(IntPtr exception)
    {
      ExceptionSeverity severity = (ExceptionSeverity)NativeMagickExceptionHelper.Severity(exception);
      string message = NativeMagickExceptionHelper.Message(exception);
      string description = NativeMagickExceptionHelper.Description(exception);

      if (!string.IsNullOrEmpty(description))
        message += " (" + description + ")";

      List<MagickException> innerExceptions = CreateRelatedExceptions(exception);
      MagickException innerException = innerExceptions.Count > 0 ? innerExceptions[0] : null;

      MagickException result = Create(severity, message, innerException);
      result.SetRelatedException(innerExceptions);

      return result;
    }

    private static List<MagickException> CreateRelatedExceptions(IntPtr exception)
    {
      List<MagickException> result = new List<MagickException>();

      int nestedCount = NativeMagickExceptionHelper.RelatedCount(exception);
      if (nestedCount == 0)
        return result;


      for (int i = 0; i < nestedCount; i++)
      {
        IntPtr nested = NativeMagickExceptionHelper.Related(exception, i);
        result.Add(CreateException(nested));
      }

      return result;
    }

    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
    private static MagickException Create(ExceptionSeverity severity, string message, MagickException innerExceptions)
    {
      switch (severity)
      {
        case ExceptionSeverity.BlobWarning:
          return new MagickBlobWarningException(message, innerExceptions);
        case ExceptionSeverity.CacheWarning:
          return new MagickCacheWarningException(message, innerExceptions);
        case ExceptionSeverity.CoderWarning:
          return new MagickCoderWarningException(message, innerExceptions);
        case ExceptionSeverity.ConfigureWarning:
          return new MagickConfigureWarningException(message, innerExceptions);
        case ExceptionSeverity.CorruptImageWarning:
          return new MagickCorruptImageWarningException(message, innerExceptions);
        case ExceptionSeverity.DelegateWarning:
          return new MagickDelegateWarningException(message, innerExceptions);
        case ExceptionSeverity.DrawWarning:
          return new MagickDrawWarningException(message, innerExceptions);
        case ExceptionSeverity.FileOpenWarning:
          return new MagickFileOpenWarningException(message, innerExceptions);
        case ExceptionSeverity.ImageWarning:
          return new MagickImageWarningException(message, innerExceptions);
        case ExceptionSeverity.MissingDelegateWarning:
          return new MagickMissingDelegateWarningException(message, innerExceptions);
        case ExceptionSeverity.ModuleWarning:
          return new MagickModuleWarningException(message, innerExceptions);
        case ExceptionSeverity.OptionWarning:
          return new MagickOptionWarningException(message, innerExceptions);
        case ExceptionSeverity.RegistryWarning:
          return new MagickRegistryWarningException(message, innerExceptions);
        case ExceptionSeverity.ResourceLimitWarning:
          return new MagickResourceLimitWarningException(message, innerExceptions);
        case ExceptionSeverity.StreamWarning:
          return new MagickStreamWarningException(message, innerExceptions);
        case ExceptionSeverity.TypeWarning:
          return new MagickTypeWarningException(message, innerExceptions);
        case ExceptionSeverity.BlobError:
          return new MagickBlobErrorException(message, innerExceptions);
        case ExceptionSeverity.CacheError:
          return new MagickCacheErrorException(message, innerExceptions);
        case ExceptionSeverity.CoderError:
          return new MagickCoderErrorException(message, innerExceptions);
        case ExceptionSeverity.ConfigureError:
          return new MagickConfigureErrorException(message, innerExceptions);
        case ExceptionSeverity.CorruptImageError:
          return new MagickCorruptImageErrorException(message, innerExceptions);
        case ExceptionSeverity.DelegateError:
          return new MagickDelegateErrorException(message, innerExceptions);
        case ExceptionSeverity.DrawError:
          return new MagickDrawErrorException(message, innerExceptions);
        case ExceptionSeverity.FileOpenError:
          return new MagickFileOpenErrorException(message, innerExceptions);
        case ExceptionSeverity.ImageError:
          return new MagickImageErrorException(message, innerExceptions);
        case ExceptionSeverity.MissingDelegateError:
          return new MagickMissingDelegateErrorException(message, innerExceptions);
        case ExceptionSeverity.ModuleError:
          return new MagickModuleErrorException(message, innerExceptions);
        case ExceptionSeverity.OptionError:
          return new MagickOptionErrorException(message, innerExceptions);
        case ExceptionSeverity.RegistryError:
          return new MagickRegistryErrorException(message, innerExceptions);
        case ExceptionSeverity.ResourceLimitError:
          return new MagickResourceLimitErrorException(message, innerExceptions);
        case ExceptionSeverity.StreamError:
          return new MagickStreamErrorException(message, innerExceptions);
        case ExceptionSeverity.TypeError:
          return new MagickTypeErrorException(message, innerExceptions);
        default:
          if (severity < ExceptionSeverity.Error)
            return new MagickWarningException(message, innerExceptions);
          else
            return new MagickErrorException(message, innerExceptions);
      }
    }

    public static MagickException Check(IntPtr exception)
    {
      MagickException magickException = Create(exception);

      if (IsError(magickException))
        throw magickException;

      return magickException;
    }

    public static MagickException Create(IntPtr exception)
    {
      if (exception == IntPtr.Zero)
        return null;

      MagickException magickException = CreateException(exception);

      NativeMagickExceptionHelper.Dispose(exception);

      return magickException;
    }

    public static bool IsError(MagickException exception)
    {
      if (exception == null)
        return false;

      return (exception is MagickErrorException);
    }
  }
}
