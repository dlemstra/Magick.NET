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
        Throw.IfNull(nameof(geometry), geometry);

        return base.GetArea(geometry);
    }

    public override QuantumType[]? GetArea(int x, int y, int width, int height)
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

    public override void SetArea(int x, int y, int width, int height, QuantumType[] values)
    {
        CheckValues(x, y, width, height, values);
        base.SetArea(x, y, width, height, values);
    }

    public override void SetArea(IMagickGeometry geometry, QuantumType[] values)
    {
        Throw.IfNull(nameof(geometry), geometry);

        base.SetArea(geometry, values);
    }

    public override void SetByteArea(int x, int y, int width, int height, byte[] values)
    {
        CheckValues(x, y, width, height, values);
        base.SetByteArea(x, y, width, height, values);
    }

    public override void SetByteArea(IMagickGeometry geometry, byte[] values)
    {
        Throw.IfNull(nameof(geometry), geometry);

        base.SetByteArea(geometry, values);
    }

    public override void SetBytePixels(byte[] values)
    {
        CheckValues(values);
        base.SetBytePixels(values);
    }

    public override void SetDoubleArea(int x, int y, int width, int height, double[] values)
    {
        CheckValues(x, y, width, height, values);
        base.SetDoubleArea(x, y, width, height, values);
    }

    public override void SetDoubleArea(IMagickGeometry geometry, double[] values)
    {
        Throw.IfNull(nameof(geometry), geometry);

        base.SetDoubleArea(geometry, values);
    }

    public override void SetDoublePixels(double[] values)
    {
        CheckValues(values);
        base.SetDoublePixels(values);
    }

    public override void SetIntArea(int x, int y, int width, int height, int[] values)
    {
        CheckValues(x, y, width, height, values);
        base.SetIntArea(x, y, width, height, values);
    }

    public override void SetIntArea(IMagickGeometry geometry, int[] values)
    {
        Throw.IfNull(nameof(geometry), geometry);

        base.SetIntArea(geometry, values);
    }

    public override void SetIntPixels(int[] values)
    {
        CheckValues(values);
        base.SetIntPixels(values);
    }

    public override void SetPixel(int x, int y, QuantumType[] value)
    {
        Throw.IfNullOrEmpty(nameof(value), value);

        SetPixelPrivate(x, y, value);
    }

    public override void SetPixel(IPixel<QuantumType> pixel)
    {
        Throw.IfNull(nameof(pixel), pixel);

        SetPixelPrivate(pixel.X, pixel.Y, pixel.ToArray());
    }

    public override void SetPixel(IEnumerable<IPixel<QuantumType>> pixels)
    {
        Throw.IfNull(nameof(pixels), pixels);

        base.SetPixel(pixels);
    }

    public override void SetPixels(QuantumType[] values)
    {
        CheckValues(values);
        base.SetPixels(values);
    }

    public override byte[]? ToByteArray(IMagickGeometry geometry, string mapping)
    {
        Throw.IfNull(nameof(geometry), geometry);

        return base.ToByteArray(geometry, mapping);
    }

    public override byte[]? ToByteArray(IMagickGeometry geometry, PixelMapping mapping)
    {
        Throw.IfNull(nameof(geometry), geometry);

        return base.ToByteArray(geometry, mapping.ToString());
    }

    public override byte[]? ToByteArray(int x, int y, int width, int height, string mapping)
    {
        Throw.IfNullOrEmpty(nameof(mapping), mapping);

        CheckArea(x, y, width, height);
        return base.ToByteArray(x, y, width, height, mapping);
    }

    public override ushort[]? ToShortArray(IMagickGeometry geometry, string mapping)
    {
        Throw.IfNull(nameof(geometry), geometry);

        return base.ToShortArray(geometry, mapping);
    }

    public override ushort[]? ToShortArray(int x, int y, int width, int height, string mapping)
    {
        Throw.IfNullOrEmpty(nameof(mapping), mapping);

        CheckArea(x, y, width, height);
        return base.ToShortArray(x, y, width, height, mapping);
    }

    private void CheckArea(int x, int y, int width, int height)
    {
        CheckIndex(x, y);
        Throw.IfOutOfRange(nameof(width), 1, Image.Width - x, width, "Invalid width: {0}.", width);
        Throw.IfOutOfRange(nameof(height), 1, Image.Height - y, height, "Invalid height: {0}.", height);
    }

    private void CheckIndex(int x, int y)
    {
        Throw.IfOutOfRange(nameof(x), 0, Image.Width - 1, x, "Invalid X coordinate: {0}.", x);
        Throw.IfOutOfRange(nameof(y), 0, Image.Height - 1, y, "Invalid Y coordinate: {0}.", y);
    }

    private void CheckValues<T>(T[] values)
        => CheckValues(0, 0, values);

    private void CheckValues<T>(int x, int y, T[] values)
        => CheckValues(x, y, Image.Width, Image.Height, values);

    private void CheckValues<T>(int x, int y, int width, int height, T[] values)
    {
        CheckIndex(x, y);
        Throw.IfNullOrEmpty(nameof(values), values);
        Throw.IfFalse(nameof(values), values.Length % Channels == 0, "Values should have {0} channels.", Channels);

        var length = values.Length;
        var max = width * height * Channels;
        Throw.IfTrue(nameof(values), length > max, "Too many values specified.");

        length = (x * y * Channels) + length;
        max = Image.Width * Image.Height * Channels;
        Throw.IfTrue(nameof(values), length > max, "Too many values specified.");
    }

    private void SetPixelPrivate(int x, int y, QuantumType[] value)
    {
        CheckIndex(x, y);

        base.SetPixel(x, y, value);
    }
}
