// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.CompilerServices;

namespace ImageMagick;

internal sealed class UnsignedShortQuantumScaler : IQuantumScaler<ushort>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ScaleToByte(ushort value)
        => (byte)((value + 128UL - ((value + 128UL) >> 8)) >> 8);
}
