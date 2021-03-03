// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// Interface for a native instance.
    /// </summary>
    internal interface INativeInstance : IDisposable
    {
        /// <summary>
        /// Gets a pointer to the native instance.
        /// </summary>
        IntPtr Instance { get; }
    }
}