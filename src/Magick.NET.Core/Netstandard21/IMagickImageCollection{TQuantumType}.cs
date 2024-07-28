// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;
using System.Buffers;

namespace ImageMagick;

/// <content/>
public partial interface IMagickImageCollection<TQuantumType>
{
    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(ReadOnlySequence<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(ReadOnlySpan<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(ReadOnlySequence<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(ReadOnlySpan<byte> data, IMagickReadSettings<TQuantumType>? readSettings);
}

#endif
