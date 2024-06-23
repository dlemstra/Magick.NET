// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class MagickGeometry
{
    [NativeInterop]
    private partial class NativeMagickGeometry : NativeInstance
    {
        public static partial NativeMagickGeometry Create();

        public partial nint X_Get();

        public partial nint Y_Get();

        public partial nuint Width_Get();

        public partial nuint Height_Get();

        public partial GeometryFlags Initialize(string value);
    }
}
