// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
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

internal partial class MagickColorCollection : IDisposable
{
    private readonly NativeMagickColorCollection _nativeInstance;

    [NativeInterop]
    private partial class NativeMagickColorCollection : NativeInstance
    {
        public static partial NativeMagickColorCollection Create(nuint length);

        public partial IntPtr Get(nuint index);

        public partial void Set(nuint index, IMagickColor<QuantumType> value);
    }
}
