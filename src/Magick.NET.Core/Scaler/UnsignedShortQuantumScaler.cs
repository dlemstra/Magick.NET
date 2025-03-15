// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.CompilerServices;

namespace ImageMagick;

internal sealed class UnsignedShortQuantumScaler : IQuantumScaler<ushort>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ushort ScaleFromByte(byte value)
        => (ushort)(value * 257);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleToByte(ushort value)
        => (byte)((value + 128U - ((value + 128U) >> 8)) >> 8);
}
