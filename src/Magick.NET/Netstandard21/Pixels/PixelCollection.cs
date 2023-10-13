// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

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
    public virtual void SetArea(int x, int y, int width, int height, ReadOnlySpan<QuantumType> values)
        => SetAreaUnchecked(x, y, width, height, values);

    public virtual void SetArea(IMagickGeometry geometry, ReadOnlySpan<QuantumType> values)
        => SetArea(geometry.X, geometry.Y, geometry.Width, geometry.Height, values);

    public virtual void SetPixel(int x, int y, ReadOnlySpan<QuantumType> value)
        => SetPixelUnchecked(x, y, value);

    public virtual void SetPixels(ReadOnlySpan<QuantumType> values)
        => SetAreaUnchecked(0, 0, Image.Width, Image.Height, values);

    private void SetPixelUnchecked(int x, int y, ReadOnlySpan<QuantumType> value)
        => SetAreaUnchecked(x, y, 1, 1, value);

    private void SetAreaUnchecked(int x, int y, int width, int height, ReadOnlySpan<QuantumType> values)
        => NativeInstance.SetArea(x, y, width, height, values, values.Length);
}

#endif
