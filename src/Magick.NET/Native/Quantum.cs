// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

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

/// <content />
public partial class Quantum
{
    [NativeInterop]
    private static partial class NativeQuantum
    {
        public static partial nuint Depth_Get();

        public static partial QuantumType Max_Get();

        public static partial byte ScaleToByte(QuantumType value);
    }
}
