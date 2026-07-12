// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal partial class MagickColorCollection
{
    [NativeInterop]
    private partial class NativeMagickColorCollection : NativeInstance
    {
        public partial IntPtr Get(nuint index);
    }
}
