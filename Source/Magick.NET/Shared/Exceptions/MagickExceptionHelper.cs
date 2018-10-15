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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    internal static partial class MagickExceptionHelper
    {
        public static MagickException Check(IntPtr exception)
        {
            MagickException magickException = Create(exception);
            if (magickException == null)
                return null;

            if (magickException is MagickErrorException)
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

        public static MagickException CreateException(IntPtr exception)
        {
            ExceptionSeverity severity = (ExceptionSeverity)NativeMagickExceptionHelper.Severity(exception);
            string message = NativeMagickExceptionHelper.Message(exception);
            string description = NativeMagickExceptionHelper.Description(exception);

            if (!string.IsNullOrEmpty(description))
                message += " (" + description + ")";

            List<MagickException> innerExceptions = CreateRelatedExceptions(exception);

            MagickException result = Create(severity, message);
            result.SetRelatedException(innerExceptions);

            return result;
        }

        private static List<MagickException> CreateRelatedExceptions(IntPtr exception)
        {
            int nestedCount = NativeMagickExceptionHelper.RelatedCount(exception);
            if (nestedCount == 0)
                return null;

            List<MagickException> result = new List<MagickException>();
            for (int i = 0; i < nestedCount; i++)
            {
                IntPtr nested = NativeMagickExceptionHelper.Related(exception, i);
                result.Add(CreateException(nested));
            }

            return result;
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cannot avoid it here.")]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Cannot avoid it here.")]
        private static MagickException Create(ExceptionSeverity severity, string message)
        {
            switch (severity)
            {
                case ExceptionSeverity.BlobWarning:
                    return new MagickBlobWarningException(message);
                case ExceptionSeverity.CacheWarning:
                    return new MagickCacheWarningException(message);
                case ExceptionSeverity.CoderWarning:
                    return new MagickCoderWarningException(message);
                case ExceptionSeverity.ConfigureWarning:
                    return new MagickConfigureWarningException(message);
                case ExceptionSeverity.CorruptImageWarning:
                    return new MagickCorruptImageWarningException(message);
                case ExceptionSeverity.DelegateWarning:
                    return new MagickDelegateWarningException(message);
                case ExceptionSeverity.DrawWarning:
                    return new MagickDrawWarningException(message);
                case ExceptionSeverity.FileOpenWarning:
                    return new MagickFileOpenWarningException(message);
                case ExceptionSeverity.ImageWarning:
                    return new MagickImageWarningException(message);
                case ExceptionSeverity.MissingDelegateWarning:
                    return new MagickMissingDelegateWarningException(message);
                case ExceptionSeverity.ModuleWarning:
                    return new MagickModuleWarningException(message);
                case ExceptionSeverity.OptionWarning:
                    return new MagickOptionWarningException(message);
                case ExceptionSeverity.PolicyWarning:
                    return new MagickPolicyWarningException(message);
                case ExceptionSeverity.RegistryWarning:
                    return new MagickRegistryWarningException(message);
                case ExceptionSeverity.ResourceLimitWarning:
                    return new MagickResourceLimitWarningException(message);
                case ExceptionSeverity.StreamWarning:
                    return new MagickStreamWarningException(message);
                case ExceptionSeverity.TypeWarning:
                    return new MagickTypeWarningException(message);
                case ExceptionSeverity.BlobError:
                    return new MagickBlobErrorException(message);
                case ExceptionSeverity.CacheError:
                    return new MagickCacheErrorException(message);
                case ExceptionSeverity.CoderError:
                    return new MagickCoderErrorException(message);
                case ExceptionSeverity.ConfigureError:
                    return new MagickConfigureErrorException(message);
                case ExceptionSeverity.CorruptImageError:
                    return new MagickCorruptImageErrorException(message);
                case ExceptionSeverity.DelegateError:
                    return new MagickDelegateErrorException(message);
                case ExceptionSeverity.DrawError:
                    return new MagickDrawErrorException(message);
                case ExceptionSeverity.FileOpenError:
                    return new MagickFileOpenErrorException(message);
                case ExceptionSeverity.ImageError:
                    return new MagickImageErrorException(message);
                case ExceptionSeverity.MissingDelegateError:
                    return new MagickMissingDelegateErrorException(message);
                case ExceptionSeverity.ModuleError:
                    return new MagickModuleErrorException(message);
                case ExceptionSeverity.OptionError:
                    return new MagickOptionErrorException(message);
                case ExceptionSeverity.PolicyError:
                    return new MagickPolicyErrorException(message);
                case ExceptionSeverity.RegistryError:
                    return new MagickRegistryErrorException(message);
                case ExceptionSeverity.ResourceLimitError:
                    return new MagickResourceLimitErrorException(message);
                case ExceptionSeverity.StreamError:
                    return new MagickStreamErrorException(message);
                case ExceptionSeverity.TypeError:
                    return new MagickTypeErrorException(message);
                default:
                    if (severity < ExceptionSeverity.Error)
                        return new MagickWarningException(message);
                    else
                        return new MagickErrorException(message);
            }
        }
    }
}
