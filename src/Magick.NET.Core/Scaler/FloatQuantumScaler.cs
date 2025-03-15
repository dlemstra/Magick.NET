// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.CompilerServices;

namespace ImageMagick;

internal sealed class FloatQuantumScaler : IQuantumScaler<float>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ScaleFromByte(byte value)
        => 257.0f * value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ScaleFromUnsignedShort(ushort value)
        => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleToByte(float value)
    {
        if (float.IsNaN(value) || value <= 0.0f)
            return 0;

        var scaledValue = value / 257.0f;
        if (scaledValue >= 255.0f)
            return 255;

        return (byte)(scaledValue + 0.5f);
    }
}
