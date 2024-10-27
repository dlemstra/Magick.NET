// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal interface INativeInstance : IDisposable
{
    IntPtr Instance { get; }
}
