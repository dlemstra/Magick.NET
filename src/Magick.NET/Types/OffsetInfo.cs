// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

internal partial class OffsetInfo
{
    public OffsetInfo(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }

    public int Y { get; }

    public INativeInstance CreateNativeInstance()
    {
        var offsetInfo = NativeOffsetInfo.Create();
        offsetInfo.SetX(X);
        offsetInfo.SetY(Y);
        return offsetInfo;
    }
}
