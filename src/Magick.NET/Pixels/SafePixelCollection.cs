// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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

internal sealed partial class SafePixelCollection : PixelCollection
{
    public SafePixelCollection(MagickImage image)
        : base(image)
    {
    }

    public override QuantumType[]? GetArea(IMagickGeometry geometry)
    {
        Throw.IfNull(geometry);

        return base.GetArea(geometry);
    }

    public override QuantumType[]? GetArea(int x, int y, uint width, uint height)
    {
        CheckArea(x, y, width, height);

        return base.GetArea(x, y, width, height);
    }

    public override IPixel<QuantumType> GetPixel(int x, int y)
    {
        CheckIndex(x, y);

        return base.GetPixel(x, y);
    }

    public override QuantumType[]? GetValue(int x, int y)
    {
        CheckIndex(x, y);

        return base.GetValue(x, y);
    }

    public override void SetArea(int x, int y, uint width, uint height, QuantumType[] values)
    {
        CheckValues(x, y, width, height, values);
        base.SetArea(x, y, width, height, values);
    }

    public override void SetArea(IMagickGeometry geometry, QuantumType[] values)
    {
        Throw.IfNull(geometry);

        base.SetArea(geometry, values);
    }

    public override void SetByteArea(int x, int y, uint width, uint height, byte[] values)
    {
        CheckValues(x, y, width, height, values);
        base.SetByteArea(x, y, width, height, values);
    }

    public override void SetByteArea(IMagickGeometry geometry, byte[] values)
    {
        Throw.IfNull(geometry);

        base.SetByteArea(geometry, values);
    }

    public override void SetBytePixels(byte[] values)
    {
        CheckValues(values);
        base.SetBytePixels(values);
    }

    public override void SetDoubleArea(int x, int y, uint width, uint height, double[] values)
    {
        CheckValues(x, y, width, height, values);
        base.SetDoubleArea(x, y, width, height, values);
    }

    public override void SetDoubleArea(IMagickGeometry geometry, double[] values)
    {
        Throw.IfNull(geometry);

        base.SetDoubleArea(geometry, values);
    }

    public override void SetDoublePixels(double[] values)
    {
        CheckValues(values);
        base.SetDoublePixels(values);
    }

    public override void SetIntArea(int x, int y, uint width, uint height, int[] values)
    {
        CheckValues(x, y, width, height, values);
        base.SetIntArea(x, y, width, height, values);
    }

    public override void SetIntArea(IMagickGeometry geometry, int[] values)
    {
        Throw.IfNull(geometry);

        base.SetIntArea(geometry, values);
    }

    public override void SetIntPixels(int[] values)
    {
        CheckValues(values);
        base.SetIntPixels(values);
    }

    public override void SetPixel(int x, int y, QuantumType[] value)
    {
        Throw.IfNullOrEmpty(value);

        SetPixelPrivate(x, y, value);
    }

    public override void SetPixel(IPixel<QuantumType> pixel)
    {
        Throw.IfNull(pixel);

        SetPixelPrivate(pixel.X, pixel.Y, pixel.ToArray());
    }

    public override void SetPixel(IEnumerable<IPixel<QuantumType>> pixels)
    {
        Throw.IfNull(pixels);

        base.SetPixel(pixels);
    }

    public override void SetPixels(QuantumType[] values)
    {
        CheckValues(values);
        base.SetPixels(values);
    }

    public override byte[]? ToByteArray(IMagickGeometry geometry, string mapping)
    {
        Throw.IfNull(geometry);

        return base.ToByteArray(geometry, mapping);
    }

    public override byte[]? ToByteArray(IMagickGeometry geometry, PixelMapping mapping)
    {
        Throw.IfNull(geometry);

        return base.ToByteArray(geometry, mapping.ToString());
    }

    public override byte[]? ToByteArray(int x, int y, uint width, uint height, string mapping)
    {
        Throw.IfNullOrEmpty(mapping);

        CheckArea(x, y, width, height);
        return base.ToByteArray(x, y, width, height, mapping);
    }

    public override ushort[]? ToShortArray(IMagickGeometry geometry, string mapping)
    {
        Throw.IfNull(geometry);

        return base.ToShortArray(geometry, mapping);
    }

    public override ushort[]? ToShortArray(int x, int y, uint width, uint height, string mapping)
    {
        Throw.IfNullOrEmpty(mapping);

        CheckArea(x, y, width, height);
        return base.ToShortArray(x, y, width, height, mapping);
    }

    private void CheckArea(int x, int y, uint width, uint height)
    {
        CheckIndex(x, y);
        Throw.IfOutOfRange(1, (int)Image.Width - x, (int)width, "Invalid width: {0}.", width, nameof(width));
        Throw.IfOutOfRange(1, (int)Image.Height - y, (int)height, "Invalid height: {0}.", height, nameof(height));
    }

    private void CheckIndex(int x, int y)
    {
        Throw.IfOutOfRange(0, (int)Image.Width - 1, x, "Invalid X coordinate: {0}.", x);
        Throw.IfOutOfRange(0, (int)Image.Height - 1, y, "Invalid Y coordinate: {0}.", y);
    }

    private void CheckValues<T>(T[] values)
        => CheckValues(0, 0, values);

    private void CheckValues<T>(int x, int y, T[] values)
        => CheckValues(x, y, Image.Width, Image.Height, values);

    private void CheckValues<T>(int x, int y, uint width, uint height, T[] values)
    {
        CheckIndex(x, y);
        Throw.IfNullOrEmpty(values);
        Throw.IfFalse(values.Length % Channels == 0, nameof(values), "Values should have {0} channels.", Channels);

        var length = values.Length;
        var max = width * height * Channels;
        Throw.IfTrue(length > max, nameof(values), "Too many values specified.");

        length = (x * y * (int)Channels) + length;
        max = Image.Width * Image.Height * Channels;
        Throw.IfTrue(length > max, nameof(values), "Too many values specified.");
    }

    private void SetPixelPrivate(int x, int y, QuantumType[] value)
    {
        CheckIndex(x, y);

        base.SetPixel(x, y, value);
    }
}
