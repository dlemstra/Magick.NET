// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal static partial class MagickMemory
    {
        public static void Relinquish(IntPtr value)
        {
            NativeMagickMemory.Relinquish(value);
        }
    }
}
