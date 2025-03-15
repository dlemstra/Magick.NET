// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.CompilerServices;

namespace ImageMagick;

internal sealed class ByteQuantumScaler : IQuantumScaler<byte>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleFromByte(byte value)
        => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleFromUnsignedShort(ushort value)
        => (byte)((value + 128U) / 257U);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleToByte(byte value)
        => value;
}
