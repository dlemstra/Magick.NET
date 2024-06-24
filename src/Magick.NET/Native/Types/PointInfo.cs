// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal partial class PointInfo
{
    public static PointInfo CreateInstance(IntPtr instance)
        => new PointInfo(instance);

    [NativeInterop]
    private partial class NativePointInfo : ConstNativeInstance
    {
        public partial double X_Get();

        public partial double Y_Get();
    }
}
