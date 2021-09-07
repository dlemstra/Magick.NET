// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.Helpers
{
    internal static unsafe class NativeMemory
    {
#if Q16
        public static void Copy(ushort* source, ushort* destination, long length)
        {
            var size = sizeof(ushort) * length;
#if NET20
            while (size >= 4)
            {
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                size -= 4;
            }

            while (size-- > 0)
            {
                *(destination++) = *(source++);
            }
#else
            Buffer.MemoryCopy(source, destination, length, size);
#endif
        }
#endif

#if Q16HDRI
        public static void Copy(float* source, float* destination, long length)
        {
            var size = sizeof(float) * length;
#if NET20
            while (size >= 4)
            {
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                size -= 4;
            }

            while (size-- > 0)
            {
                *(destination++) = *(source++);
            }
#else
            Buffer.MemoryCopy(source, destination, length, size);
#endif
        }
#endif

        public static void Copy(byte* source, byte* destination, long length)
        {
#if NET20
            while (length >= 4)
            {
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                *(destination++) = *(source++);
                length -= 4;
            }

            while (length-- > 0)
            {
                *(destination++) = *(source++);
            }
#else
            Buffer.MemoryCopy(source, destination, length, length);
#endif
        }
    }
}
