// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
using ImageMagick.SourceGenerator;

namespace ImageMagick.ImageOptimizers;

/// <content />
public partial class JpegOptimizer
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate long ReadWriteStreamDelegate(IntPtr data, UIntPtr length, IntPtr user_data);

    [NativeInterop]
    private partial class NativeJpegOptimizer : NativeHelper
    {
        [Throws]
        public static partial void CompressFile(string input, string output, bool progressive, bool lossless, nuint quality);

        [Throws]
        public static partial void CompressStream(ReadWriteStreamDelegate reader, ReadWriteStreamDelegate writer, bool progressive, bool lossless, nuint quality);
    }
}
