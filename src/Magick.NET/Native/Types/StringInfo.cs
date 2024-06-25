// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal partial class StringInfo
{
    [NativeInterop]
    private partial class NativeStringInfo : ConstNativeInstance
    {
        public partial IntPtr Datum_Get();

        public partial nuint Length_Get();
    }
}
