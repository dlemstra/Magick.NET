// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.CompilerServices;

namespace ImageMagick;

internal sealed class ByteQuantumScaler : IQuantumScaler<byte>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleFromByte(byte value)
        => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleFromDouble(double value)
        => (byte)Math.Min(Math.Max(0.0, value * byte.MaxValue), byte.MaxValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleFromUnsignedShort(ushort value)
        => (byte)((value + 128U) / 257U);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleToByte(byte value)
        => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ScaleToDouble(byte value)
        => value / 255.0;
}
