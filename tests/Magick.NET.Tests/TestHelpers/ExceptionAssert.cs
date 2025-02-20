// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit.Sdk;

namespace Magick.NET;

internal static class ExceptionAssert
{
    public static void Contains(string exceptedSubstring, Exception exception)
        => AssertContains(exceptedSubstring, exception.Message);

    public static void Contains(string exceptedSubstring, MagickException exception)
    {
        var message = exception.Message;

        if (exception.RelatedExceptions.Count > 0)
        {
            message += exception.RelatedExceptions[0].Message;
        }

        AssertContains(exceptedSubstring, message);
    }

    private static void AssertContains(string exceptedSubstring, string message)
    {
        if (!message.Contains(exceptedSubstring))
        {
            throw new XunitException($"Expected string to contain: '{exceptedSubstring}'\nActual string was: '{message}'");
        }
    }
}
