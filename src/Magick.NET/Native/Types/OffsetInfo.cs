// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal partial class OffsetInfo
{
    [NativeInterop(ManagedToNative = true)]
    private partial class NativeOffsetInfo : NativeInstance
    {
        public static partial NativeOffsetInfo Create();

        [Instance(SetsInstance = false)]
        public partial void SetX(nint value);

        [Instance(SetsInstance = false)]
        public partial void SetY(nint value);
    }
}
