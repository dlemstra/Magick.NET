// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

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
    internal static partial class MagickColorCollection
    {
        public static void DisposeList(IntPtr list)
        {
            if (list != IntPtr.Zero)
                NativeMagickColorCollection.DisposeList(list);
        }

        public static Dictionary<IMagickColor<QuantumType>, int> ToDictionary(IntPtr list, int length)
        {
            var colors = new Dictionary<IMagickColor<QuantumType>, int>();

            if (list == IntPtr.Zero)
                return colors;

            for (int i = 0; i < length; i++)
            {
                var instance = NativeMagickColorCollection.GetInstance(list, i);

                var color = MagickColor.CreateInstance(instance, out var count);
                if (color == null)
                    continue;

                colors[color] = count;
            }

            return colors;
        }
    }
}