// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class OpenCL
{
    [NativeInterop]
    private static partial class NativeOpenCL
    {
        public static partial IntPtr GetDevices(out nuint length);

        public static partial OpenCLDevice GetDevice(IntPtr list, nuint index);

        public static partial bool GetEnabled();

        public static partial bool SetEnabled(bool value);
    }
}
