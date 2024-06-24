// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class OpenCLKernelProfileRecord
{
    internal static OpenCLKernelProfileRecord CreateInstance(IntPtr instance)
    {
        var nativeInstance = new NativeOpenCLKernelProfileRecord(instance);
        return new OpenCLKernelProfileRecord(nativeInstance);
    }

    [NativeInterop]
    private partial class NativeOpenCLKernelProfileRecord : ConstNativeInstance
    {
        public partial ulong Count_Get();

        public partial ulong MaximumDuration_Get();

        public partial ulong MinimumDuration_Get();

        public partial string Name_Get();

        public partial ulong TotalDuration_Get();
    }
}
