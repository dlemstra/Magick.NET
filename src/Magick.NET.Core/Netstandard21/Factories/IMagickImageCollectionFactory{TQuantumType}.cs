// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;
using System.Buffers;

namespace ImageMagick.Factories;

/// <content />
public partial interface IMagickImageCollectionFactory<TQuantumType>
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(ReadOnlySequence<byte> data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(ReadOnlySequence<byte> data, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(ReadOnlySpan<byte> data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(ReadOnlySpan<byte> data, IMagickReadSettings<TQuantumType> readSettings);
}

#endif
