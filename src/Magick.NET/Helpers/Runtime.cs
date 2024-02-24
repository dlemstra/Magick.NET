// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;

namespace ImageMagick;

internal static partial class Runtime
{
    public static bool Is64Bit { get; } = IntPtr.Size == 8;

    public static bool IsWindows { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
}
