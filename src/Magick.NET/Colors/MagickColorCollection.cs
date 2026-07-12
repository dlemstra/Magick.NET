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

namespace ImageMagick;

internal partial class MagickColorCollection
{
    public MagickColorCollection(IReadOnlyList<IMagickColor<QuantumType>> colors)
    {
        Count = (uint)colors.Count;
        _nativeInstance = NativeMagickColorCollection.Create(Count);
        for (var i = 0; i < colors.Count; i++)
        {
            var color = colors[i];
            _nativeInstance.Set((nuint)i, color);
        }
    }

    public uint Count { get; }

    public static IReadOnlyDictionary<IMagickColor<QuantumType>, uint> ToDictionary(IntPtr list, uint length)
    {
        var colors = new Dictionary<IMagickColor<QuantumType>, uint>((int)length);

        if (list == IntPtr.Zero)
            return colors;

        using var colorCollection = new NativeMagickColorCollection(list);

        for (var i = 0U; i < length; i++)
        {
            var instance = colorCollection.Get(i);

            var color = MagickColor.CreateInstance(instance, out var count);
            if (color is null)
                continue;

            colors.Add(color, count);
        }

        return colors;
    }

    public void Dispose()
        => _nativeInstance.Dispose();

    internal static IntPtr GetInstance(MagickColorCollection pointInfoCollection)
        => pointInfoCollection._nativeInstance.Instance;
}
