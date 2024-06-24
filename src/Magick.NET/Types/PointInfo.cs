// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal partial class PointInfo
{
    private PointInfo(IntPtr instance)
    {
        var nativeInstance = new NativePointInfo(instance);
        X = nativeInstance.X_Get();
        Y = nativeInstance.Y_Get();
    }

    public double X { get; }

    public double Y { get; }

    public PointD ToPointD()
        => new PointD(X, Y);
}
