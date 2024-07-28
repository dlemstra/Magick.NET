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

internal sealed partial class UnsafePixelCollection
{
    public override ReadOnlySpan<QuantumType> GetReadOnlyArea(IMagickGeometry geometry)
    {
        if (geometry is null)
            return default;

        return base.GetReadOnlyArea(geometry);
    }

    public override void SetArea(IMagickGeometry geometry, ReadOnlySpan<QuantumType> values)
    {
        if (geometry is not null)
            base.SetArea(geometry, values);
    }
}

#endif
