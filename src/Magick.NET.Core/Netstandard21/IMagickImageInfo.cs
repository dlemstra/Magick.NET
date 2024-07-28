// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;
using System.Buffers;

namespace ImageMagick;

/// <content />
public partial interface IMagickImageInfo
{
    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(ReadOnlySequence<byte> data);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The span of bytes to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(ReadOnlySpan<byte> data);
}

#endif
