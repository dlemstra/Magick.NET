// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.Buffers;

namespace ImageMagick.Factories;

/// <content />
public partial interface IMagickImageFactory<TQuantumType>
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(ReadOnlySequence<byte> data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(ReadOnlySequence<byte> data, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(ReadOnlySpan<byte> data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(ReadOnlySpan<byte> data, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(ReadOnlySpan<byte> data, IPixelReadSettings<TQuantumType> settings);
}

#endif

