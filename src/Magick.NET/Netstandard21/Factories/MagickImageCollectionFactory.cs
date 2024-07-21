// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.Buffers;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.Factories;

/// <content />
public sealed partial class MagickImageCollectionFactory
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageCollection<QuantumType> Create(ReadOnlySequence<byte> data)
         => new MagickImageCollection(data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageCollection<QuantumType> Create(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType> readSettings)
         => new MagickImageCollection(data, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageCollection<QuantumType> Create(ReadOnlySpan<byte> data)
         => new MagickImageCollection(data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageCollection<QuantumType> Create(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType> readSettings)
         => new MagickImageCollection(data, readSettings);
}

#endif
