// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal static partial class MagickMemory
{
    [NativeInterop]
    private static partial class NativeMagickMemory
    {
        public static partial void Relinquish(IntPtr value);
    }
}
