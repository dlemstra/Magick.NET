// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class OpenCLDevice
{
    [NativeInterop]
    private partial class NativeOpenCLDevice : ConstNativeInstance
    {
        public static partial IntPtr GetKernelProfileRecord(IntPtr list, uint index);

        public partial double BenchmarkScore_Get();

        public partial OpenCLDeviceType DeviceType_Get();

        public partial bool IsEnabled_Get();

        public partial void IsEnabled_Set(bool value);

        public partial string Name_Get();

        public partial string Version_Get();

        public partial IntPtr GetKernelProfileRecords(out uint length);

        public partial void SetProfileKernels(bool value);
    }
}
