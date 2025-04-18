// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.CompilerServices;

namespace ImageMagick;

internal sealed class UnsignedShortQuantumScaler : IQuantumScaler<ushort>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ushort ScaleFromByte(byte value)
        => (ushort)(value * 257);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ushort ScaleFromDouble(double value)
        => (ushort)Math.Min(Math.Max(0.0, value * ushort.MaxValue), ushort.MaxValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ushort ScaleFromUnsignedShort(ushort value)
        => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleToByte(ushort value)
        => (byte)((value + 128U - ((value + 128U) >> 8)) >> 8);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ScaleToDouble(ushort value)
        => value / 65535.0;
}
