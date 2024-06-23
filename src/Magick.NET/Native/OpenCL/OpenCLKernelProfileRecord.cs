// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class OpenCLKernelProfileRecord
{
    private OpenCLKernelProfileRecord(NativeOpenCLKernelProfileRecord instance)
    {
        Name = instance.Name_Get();
        Count = (long)instance.Count_Get();
        MaximumDuration = (long)instance.MaximumDuration_Get();
        MinimumDuration = (long)instance.MinimumDuration_Get();
        TotalDuration = (long)instance.TotalDuration_Get();
    }

    internal static OpenCLKernelProfileRecord? CreateInstance(IntPtr instance)
    {
        if (instance == IntPtr.Zero)
            return null;

        var nativeInstance = new NativeOpenCLKernelProfileRecord(instance);
        return new OpenCLKernelProfileRecord(nativeInstance);
    }

    [NativeInterop]
    private partial class NativeOpenCLKernelProfileRecord : ConstNativeInstance
    {
        public NativeOpenCLKernelProfileRecord(IntPtr instance)
            => Instance = instance;

        public partial ulong Count_Get();

        public partial ulong MaximumDuration_Get();

        public partial ulong MinimumDuration_Get();

        public partial string Name_Get();

        public partial ulong TotalDuration_Get();
    }
}
