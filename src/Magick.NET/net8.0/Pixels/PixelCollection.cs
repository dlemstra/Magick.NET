// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

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

internal abstract partial class PixelCollection
{
    public virtual unsafe ReadOnlySpan<QuantumType> GetReadOnlyArea(int x, int y, uint width, uint height)
    {
        var area = GetAreaPointer(x, y, width, height);
        if (area == IntPtr.Zero)
        {
            return default;
        }

        var length = (int)(width * height * Image.ChannelCount);

#if Q8
        return new ReadOnlySpan<QuantumType>((byte*)area, length);
#elif Q16
        return new ReadOnlySpan<QuantumType>((ushort*)area, length);
#elif Q16HDRI
        return new ReadOnlySpan<QuantumType>((float*)area, length);
#else
#error Not implemented!
#endif
    }

    public virtual ReadOnlySpan<QuantumType> GetReadOnlyArea(IMagickGeometry geometry)
        => GetReadOnlyArea(geometry.X, geometry.Y, geometry.Width, geometry.Height);

    public virtual void SetArea(int x, int y, uint width, uint height, ReadOnlySpan<QuantumType> values)
        => SetAreaUnchecked(x, y, width, height, values);

    public virtual void SetArea(IMagickGeometry geometry, ReadOnlySpan<QuantumType> values)
        => SetArea(geometry.X, geometry.Y, geometry.Width, geometry.Height, values);

    public virtual void SetPixel(int x, int y, ReadOnlySpan<QuantumType> value)
        => SetPixelUnchecked(x, y, value);

    public virtual void SetPixels(ReadOnlySpan<QuantumType> values)
        => SetAreaUnchecked(0, 0, Image.Width, Image.Height, values);

    private void SetPixelUnchecked(int x, int y, ReadOnlySpan<QuantumType> value)
        => SetAreaUnchecked(x, y, 1, 1, value);

    private void SetAreaUnchecked(int x, int y, uint width, uint height, ReadOnlySpan<QuantumType> values)
        => NativeInstance.SetArea(x, y, width, height, values, (uint)values.Length);
}

#endif
