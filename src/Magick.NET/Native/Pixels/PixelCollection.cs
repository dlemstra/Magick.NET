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
        public NativePixelCollection(IMagickImage image)
            => Instance = Create(image);

        [Throws]
        public static partial IntPtr Create(IMagickImage image);

        [Throws]
        public partial IntPtr GetArea(int x, int y, uint width, uint height);

        [Throws]
        public partial IntPtr GetReadOnlyArea(int x, int y, uint width, uint height);

        [Throws]
        public partial void SetArea(int x, int y, uint width, uint height, QuantumType[] values, uint length);

#if NETSTANDARD2_1
        [Throws]
        public partial void SetArea(int x, int y, uint width, uint height, ReadOnlySpan<QuantumType> values, uint length);
#endif

        [Throws]
        [Cleanup(Name = "MagickMemory.Relinquish")]
        public partial IntPtr ToByteArray(int x, int y, uint width, uint height, string mapping);

        [Throws]
        [Cleanup(Name = "MagickMemory.Relinquish")]
        public partial IntPtr ToShortArray(int x, int y, uint width, uint height, string mapping);
    }
}
