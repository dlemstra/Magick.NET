// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
internal static partial class MagickExceptionHelper
{
    [NativeInterop]
    private static unsafe partial class NativeMagickExceptionHelper
    {
        public static partial string? Description(IntPtr exception);

        public static partial void Dispose(IntPtr exception);

        public static partial string? Message(IntPtr exception);

        public static partial IntPtr Related(IntPtr exception, int index);

        public static partial int RelatedCount(IntPtr exception);

        public static partial int Severity(IntPtr exception);
    }
}
