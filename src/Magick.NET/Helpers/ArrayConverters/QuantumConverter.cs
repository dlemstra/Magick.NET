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

namespace ImageMagick
{
    internal static class QuantumConverter
    {
        public static QuantumType[]? ToArray(IntPtr nativeData, int length)
        {
            if (nativeData == IntPtr.Zero)
                return null;

            var result = new QuantumType[length];

            unsafe
            {
#if Q8
                var sourcePtr = (byte*)nativeData;
#elif Q16
                var sourcePtr = (ushort*)nativeData;
#elif Q16HDRI
                var sourcePtr = (float*)nativeData;
#else
#error Not implemented!
#endif
                for (var i = 0; i < length; ++i)
                {
                    result[i] = *sourcePtr++;
                }
            }

            return result;
        }
    }
}
