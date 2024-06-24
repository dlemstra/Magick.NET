// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class OpenCLDevice
{
    internal static OpenCLDevice CreateInstance(IntPtr instance)
    {
        var nativeInstance = new NativeOpenCLDevice(instance);
        return new OpenCLDevice(nativeInstance);
    }

    [NativeInterop]
    private partial class NativeOpenCLDevice : ConstNativeInstance
    {
        public static partial OpenCLKernelProfileRecord GetKernelProfileRecord(IntPtr list, nuint index);

        public partial double BenchmarkScore_Get();

        public partial OpenCLDeviceType DeviceType_Get();

        public partial bool IsEnabled_Get();

        public partial void IsEnabled_Set(bool value);

        public partial string Name_Get();

        public partial string Version_Get();

        public partial IntPtr GetKernelProfileRecords(out nuint length);

        public partial void SetProfileKernels(bool value);
    }
}
