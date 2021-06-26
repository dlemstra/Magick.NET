// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NET20

namespace ImageMagick
{
    internal static partial class OperatingSystem
    {
        public static bool IsWindows =>
           true;

        public static bool IsMacOS =>
            false;

        public static bool IsLinux =>
            false;
    }
}

#endif
