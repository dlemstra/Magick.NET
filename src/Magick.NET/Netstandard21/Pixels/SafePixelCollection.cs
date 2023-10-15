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

internal sealed partial class SafePixelCollection
{
    public override ReadOnlySpan<QuantumType> GetReadOnlyArea(int x, int y, int width, int height)
    {
        CheckArea(x, y, width, height);

        return base.GetReadOnlyArea(x, y, width, height);
    }

    public override ReadOnlySpan<QuantumType> GetReadOnlyArea(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        return base.GetReadOnlyArea(geometry);
    }

    public override void SetArea(int x, int y, int width, int height, ReadOnlySpan<QuantumType> values)
    {
        CheckValues(x, y, width, height, values);

        base.SetArea(x, y, width, height, values);
    }

    public override void SetArea(IMagickGeometry geometry, ReadOnlySpan<QuantumType> values)
    {
        Throw.IfNull(nameof(geometry), geometry);

        base.SetArea(geometry, values);
    }

    public override void SetPixels(ReadOnlySpan<QuantumType> values)
    {
        CheckValues(values);

        base.SetPixels(values);
    }

    private void CheckValues<T>(ReadOnlySpan<T> values)
        => CheckValues(0, 0, values);

    private void CheckValues<T>(int x, int y, ReadOnlySpan<T> values)
        => CheckValues(x, y, Image.Width, Image.Height, values);

    private void CheckValues<T>(int x, int y, int width, int height, ReadOnlySpan<T> values)
    {
        CheckIndex(x, y);
        Throw.IfEmpty(nameof(values), values);
        Throw.IfFalse(nameof(values), values.Length % Channels == 0, "Values should have {0} channels.", Channels);

        var length = values.Length;
        var max = width * height * Channels;
        Throw.IfTrue(nameof(values), length > max, "Too many values specified.");

        length = (x * y * Channels) + length;
        max = Image.Width * Image.Height * Channels;
        Throw.IfTrue(nameof(values), length > max, "Too many values specified.");
    }
}

#endif
