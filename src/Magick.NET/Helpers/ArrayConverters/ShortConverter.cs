// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal static class ShortConverter
    {
        public static ushort[]? ToArray(IntPtr nativeData, int length)
        {
            if (nativeData == IntPtr.Zero)
                return null;

            ushort[] buffer = new ushort[length];

            unsafe
            {
                ushort* walk = (ushort*)nativeData;
                for (int i = 0; i < length; i++)
                {
                    buffer[i] = *walk++;
                }
            }

            return buffer;
        }
    }
}
