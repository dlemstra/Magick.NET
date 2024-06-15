// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal static partial class MagickColorCollection
{
    [NativeInterop]
    private static partial class NativeMagickColorCollection
    {
        public static partial void DisposeList(IntPtr list);

        public static partial IntPtr GetInstance(IntPtr list, uint index);
    }
}
