// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal static unsafe class NativeMemory
{
#if Q16
    public static void Copy(ushort* source, ushort* destination, long length)
    {
        var size = sizeof(ushort) * length;
        Buffer.MemoryCopy(source, destination, size, size);
    }
#endif

#if Q16HDRI
    public static void Copy(float* source, float* destination, long length)
    {
        var size = sizeof(float) * length;
        Buffer.MemoryCopy(source, destination, size, size);
    }
#endif

    public static void Copy(byte* source, byte* destination, long length)
        => Buffer.MemoryCopy(source, destination, length, length);
}
