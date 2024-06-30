// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal partial class PointInfoCollection : IDisposable
{
    private NativePointInfoCollection _nativeInstance;

    [NativeInterop]
    private partial class NativePointInfoCollection : NativeInstance
    {
        public static partial NativePointInfoCollection Create(nuint length);

        public partial double GetX(nint index);

        public partial double GetY(nint index);

        public partial void Set(nuint index, double x, double y);
    }
}
