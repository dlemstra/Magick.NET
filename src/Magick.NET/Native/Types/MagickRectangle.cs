// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal partial class MagickRectangle
{
    internal static MagickRectangle? CreateInstance(IntPtr instance)
    {
        if (instance == IntPtr.Zero)
            return null;

        using var nativeInstance = new NativeMagickRectangle(instance);
        return new MagickRectangle(nativeInstance);
    }

    private NativeMagickRectangle CreateNativeInstance()
    {
        var nativeInstance = NativeMagickRectangle.Create();
        nativeInstance.X_Set(X);
        nativeInstance.Y_Set(Y);
        nativeInstance.Width_Set(Width);
        nativeInstance.Height_Set(Height);

        return nativeInstance;
    }

    [NativeInterop(ManagedToNative = true)]
    private partial class NativeMagickRectangle : NativeInstance
    {
        public static partial NativeMagickRectangle Create();

        public static partial MagickRectangle? FromPageSize(string value);

        public partial nint X_Get();

        public partial void Y_Set(nint value);

        public partial nint Y_Get();

        public partial void X_Set(nint value);

        public partial nuint Width_Get();

        public partial void Width_Set(nuint value);

        public partial nuint Height_Get();

        public partial void Height_Set(nuint value);
    }
}
