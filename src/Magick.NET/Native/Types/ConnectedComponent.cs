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

/// <content />
public partial class ConnectedComponent
{
    [NativeInterop]
    private partial class NativeConnectedComponent
    {
        public static partial void DisposeList(IntPtr list);

        public static partial nuint GetArea(IntPtr instance);

        public static partial PointInfo GetCentroid(IntPtr instance);

        public static partial IMagickColor<QuantumType>? GetColor(IntPtr instance);

        public static partial nuint GetHeight(IntPtr instance);

        public static partial nint GetId(IntPtr instance);

        public static partial IntPtr GetInstance(IntPtr list, nuint index);

        public static partial nuint GetWidth(IntPtr instance);

        public static partial nint GetX(IntPtr instance);

        public static partial nint GetY(IntPtr instance);
    }
}
