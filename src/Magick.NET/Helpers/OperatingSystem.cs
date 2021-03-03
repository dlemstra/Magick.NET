// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD
using System.Runtime.InteropServices;
#endif

using System;

namespace ImageMagick
{
    internal static class OperatingSystem
    {
        public static bool Is64Bit =>
            IntPtr.Size == 8;

#if NETSTANDARD
        public static bool IsWindows =>
           RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
#else
        public static bool IsWindows =>
           true;

        public static bool IsMacOS =>
            false;

        public static bool IsLinux =>
            false;
#endif
    }
}
