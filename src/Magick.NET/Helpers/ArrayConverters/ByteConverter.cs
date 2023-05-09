// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;

namespace ImageMagick;

internal static class ByteConverter
{
    public static byte[]? ToArray(IntPtr nativeData, int length)
    {
        if (nativeData == IntPtr.Zero)
            return null;

        var buffer = new byte[length];
        Marshal.Copy(nativeData, buffer, 0, length);
        return buffer;
    }
}
