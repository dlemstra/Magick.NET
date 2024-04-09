// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick;

internal static partial class MagickExceptionHelper
{
    public static MagickException? Check(IntPtr exception)
    {
        var magickException = Create(exception);
        if (magickException is null)
            return null;

        if (magickException is MagickErrorException)
            throw magickException;

        return magickException;
    }

    public static MagickException? Create(IntPtr exception)
    {
        if (exception == IntPtr.Zero)
            return null;

        var magickException = CreateException(exception);

        NativeMagickExceptionHelper.Dispose(exception);

        return magickException;
    }

    public static MagickException CreateException(IntPtr exception)
    {
        var severity = (ExceptionSeverity)NativeMagickExceptionHelper.Severity(exception);
        var message = NativeMagickExceptionHelper.Message(exception);
        var description = NativeMagickExceptionHelper.Description(exception);

        if (!string.IsNullOrEmpty(description))
            message += " (" + description + ")";

        if (message is null || message.Length == 0)
            message = string.Empty;

        var result = Create(severity, message);

        var relatedExceptions = CreateRelatedExceptions(exception);
        if (relatedExceptions is not null)
            result.SetRelatedException(relatedExceptions);

        return result;
    }

    private static List<MagickException>? CreateRelatedExceptions(IntPtr exception)
    {
        var nestedCount = NativeMagickExceptionHelper.RelatedCount(exception);
        if (nestedCount == 0)
            return null;

        var result = new List<MagickException>(nestedCount);
        for (var i = 0; i < nestedCount; i++)
        {
            var nested = NativeMagickExceptionHelper.Related(exception, i);
            result.Add(CreateException(nested));
        }

        return result;
    }

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
