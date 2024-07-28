// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

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
public sealed partial class MagickImageFactory
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(ReadOnlySequence<byte> data)
        => new MagickImage(data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType> readSettings)
        => new MagickImage(data, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(ReadOnlySpan<byte> data)
        => new MagickImage(data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType> readSettings)
        => new MagickImage(data, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(ReadOnlySpan<byte> data, IPixelReadSettings<QuantumType> settings)
        => new MagickImage(data, settings);
}

#endif
