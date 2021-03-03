// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;

namespace ImageMagick
{
    internal static class ByteConverter
    {
        public static byte[]? ToArray(IntPtr nativeData)
        {
            if (nativeData == IntPtr.Zero)
                return null;

            unsafe
            {
                int length = 0;
                byte* walk = (byte*)nativeData;

                // find the end of the string
                while (*(walk++) != 0)
                    length++;

                if (length == 0)
                    return new byte[0];

                return ToArray(nativeData, length);
            }
        }

        public static byte[]? ToArray(IntPtr nativeData, int length)
        {
            if (nativeData == IntPtr.Zero)
                return null;

            byte[] buffer = new byte[length];
            Marshal.Copy(nativeData, buffer, 0, length);
            return buffer;
        }
    }
}
