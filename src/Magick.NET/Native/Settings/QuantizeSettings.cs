// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class QuantizeSettings
{
    [NativeInterop(ManagedToNative = true)]
    private unsafe sealed partial class NativeQuantizeSettings : NativeInstance
    {
        public NativeQuantizeSettings()
            => Instance = Create();

        public static partial IntPtr Create();

        [Instance(SetsInstance = false)]
        public partial void SetColors(nuint value);

        [Instance(SetsInstance = false)]
        public partial void SetColorSpace(ColorSpace value);

        [Instance(SetsInstance = false)]
        public partial void SetDitherMethod(DitherMethod value);

        [Instance(SetsInstance = false)]
        public partial void SetMeasureErrors(bool value);

        [Instance(SetsInstance = false)]
        public partial void SetTreeDepth(nuint value);
    }
}
