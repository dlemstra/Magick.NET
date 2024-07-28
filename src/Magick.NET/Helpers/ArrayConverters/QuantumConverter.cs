// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

internal static class QuantumConverter
{
    public static QuantumType[]? ToArray(IntPtr nativeData, uint length)
    {
        if (nativeData == IntPtr.Zero)
            return null;

        var result = new QuantumType[length];
        unsafe
        {
#if Q8
            var source = (byte*)nativeData;
            fixed (byte* destination = result)
#elif Q16
            var source = (ushort*)nativeData;
            fixed (ushort* destination = result)
#elif Q16HDRI
            var source = (float*)nativeData;
            fixed (float* destination = result)
#else
#error Not implemented!
#endif
            {
                NativeMemory.Copy(source, destination, length);
            }
        }

        return result;
    }
}
