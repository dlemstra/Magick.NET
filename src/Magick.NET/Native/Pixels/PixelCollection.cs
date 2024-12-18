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

internal partial class PixelCollection
{
    [NativeInterop(CustomInstance = true)]
    private partial class NativePixelCollection : NativeInstance
    {
        [Throws]
        public static partial NativePixelCollection Create(IMagickImage image);

        [Throws]
        public partial IntPtr GetArea(nint x, nint y, nuint width, nuint height);

        [Throws]
        public partial IntPtr GetReadOnlyArea(nint x, nint y, nuint width, nuint height);

        [Throws]
        [SetInstance]
        public partial void SetArea(nint x, nint y, nuint width, nuint height, QuantumType[] values, nuint length);

#if !NETSTANDARD2_0
        [Throws]
        [SetInstance]
        public partial void SetArea(nint x, nint y, nuint width, nuint height, ReadOnlySpan<QuantumType> values, nuint length);
#endif

        [Throws]
        [Cleanup(Name = "MagickMemory.Relinquish")]
        public partial IntPtr ToByteArray(nint x, nint y, nuint width, nuint height, string mapping);

        [Throws]
        [Cleanup(Name = "MagickMemory.Relinquish")]
        public partial IntPtr ToShortArray(nint x, nint y, nuint width, nuint height, string mapping);
    }
}
