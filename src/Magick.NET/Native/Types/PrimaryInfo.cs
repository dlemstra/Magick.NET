// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class PrimaryInfo
{
    internal static PrimaryInfo CreateInstance(IntPtr instance)
        => new PrimaryInfo(instance);

    private static INativeInstance CreateNativeInstance(IPrimaryInfo instance)
    {
        var nativeInstance = NativePrimaryInfo.Create();
        nativeInstance.X_Set(instance.X);
        nativeInstance.Y_Set(instance.Y);
        nativeInstance.Z_Set(instance.Z);

        return nativeInstance;
    }

    [NativeInterop(ManagedToNative = true)]
    private partial class NativePrimaryInfo : NativeInstance
    {
        public static partial NativePrimaryInfo Create();

        public partial double X_Get();

        public partial void X_Set(double value);

        public partial double Y_Get();

        public partial void Y_Set(double value);

        public partial double Z_Get();

        public partial void Z_Set(double value);
    }
}
