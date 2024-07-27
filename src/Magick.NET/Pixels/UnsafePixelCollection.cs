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

internal sealed partial class UnsafePixelCollection : PixelCollection, IUnsafePixelCollection<QuantumType>
{
    public UnsafePixelCollection(MagickImage image)
        : base(image)
    {
    }

    public override QuantumType[]? GetArea(IMagickGeometry geometry)
    {
        if (geometry is null)
            return null;

        return base.GetArea(geometry);
    }

    public override IntPtr GetAreaPointer(IMagickGeometry geometry)
    {
        if (geometry is null)
            return IntPtr.Zero;

        return base.GetAreaPointer(geometry);
    }

    public override void SetArea(int x, int y, uint width, uint height, QuantumType[] values)
    {
        if (values is not null)
            base.SetArea(x, y, width, height, values);
    }

    public override void SetArea(IMagickGeometry geometry, QuantumType[] values)
    {
        if (geometry is not null)
            base.SetArea(geometry, values);
    }

    public override void SetByteArea(int x, int y, uint width, uint height, byte[] values)
    {
        if (values is not null)
            base.SetByteArea(x, y, width, height, values);
    }

    public override void SetByteArea(IMagickGeometry geometry, byte[] values)
    {
        if (geometry is not null)
            base.SetByteArea(geometry, values);
    }

    public override void SetBytePixels(byte[] values)
    {
        if (values is not null)
            base.SetBytePixels(values);
    }

    public override void SetDoubleArea(int x, int y, uint width, uint height, double[] values)
    {
        if (values is not null)
            base.SetDoubleArea(x, y, width, height, values);
    }

    public override void SetDoubleArea(IMagickGeometry geometry, double[] values)
    {
        if (geometry is not null)
            base.SetDoubleArea(geometry, values);
    }

    public override void SetDoublePixels(double[] values)
    {
        if (values is not null)
            base.SetDoublePixels(values);
    }

    public override void SetIntArea(int x, int y, uint width, uint height, int[] values)
    {
        if (values is not null)
            base.SetIntArea(x, y, width, height, values);
    }

    public override void SetIntArea(IMagickGeometry geometry, int[] values)
    {
        if (geometry is not null)
            base.SetIntArea(geometry, values);
    }

    public override void SetIntPixels(int[] values)
    {
        if (values is not null)
            base.SetIntPixels(values);
    }

    public override void SetPixel(IEnumerable<IPixel<QuantumType>> pixels)
    {
        if (pixels is not null)
            base.SetPixel(pixels);
    }

    public override void SetPixel(int x, int y, QuantumType[] value)
    {
        if (value is not null)
            base.SetPixel(x, y, value);
    }

    public override void SetPixels(QuantumType[] values)
    {
        if (values is not null)
            base.SetPixels(values);
    }

    public override byte[]? ToByteArray(IMagickGeometry geometry, string mapping)
    {
        if (geometry is null)
            return null;

        return base.ToByteArray(geometry, mapping);
    }

    public override byte[]? ToByteArray(int x, int y, uint width, uint height, string mapping)
    {
        if (mapping is null)
            return null;

        return base.ToByteArray(x, y, width, height, mapping);
    }

    public override ushort[]? ToShortArray(IMagickGeometry geometry, string mapping)
    {
        if (geometry is null)
            return null;

        return base.ToShortArray(geometry, mapping);
    }

    public override ushort[]? ToShortArray(int x, int y, uint width, uint height, string mapping)
    {
        if (mapping is null)
            return null;

        return base.ToShortArray(x, y, width, height, mapping);
    }
}
