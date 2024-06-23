// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class DoubleMatrix
{
    [NativeInterop(ManagedToNative = true)]
    private partial class NativeDoubleMatrix : NativeInstance
    {
        public NativeDoubleMatrix(IntPtr instance)
            => Instance = instance;

        public static partial NativeDoubleMatrix Create(double[] values, nuint order);
    }
}
