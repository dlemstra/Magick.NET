// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal partial class OffsetInfo
{
    private NativeOffsetInfo CreateNativeInstance()
    {
        var offsetInfo = NativeOffsetInfo.Create();
        offsetInfo.SetX(X);
        offsetInfo.SetY(Y);
        return offsetInfo;
    }

    [NativeInterop(ManagedToNative = true)]
    private partial class NativeOffsetInfo : NativeInstance
    {
        public static partial NativeOffsetInfo Create();

        public partial void SetX(nint value);

        public partial void SetY(nint value);
    }
}
