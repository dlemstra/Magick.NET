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
        public static partial NativeQuantizeSettings Create();

        public partial void SetColors(nuint value);

        public partial void SetColorSpace(ColorSpace value);

        public partial void SetDitherMethod(DitherMethod value);

        public partial void SetMeasureErrors(bool value);

        public partial void SetTreeDepth(nuint value);
    }
}
