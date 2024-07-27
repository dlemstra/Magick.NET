// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick.Factories;

/// <summary>
/// Class that can be used to create <see cref="IMagickImageInfo"/> instances.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public partial interface IMagickImageInfoFactory<TQuantumType> : IMagickImageInfoFactory
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageInfo Create(byte[] data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageInfo Create(byte[] data, uint offset, uint count, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageInfo Create(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageInfo Create(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageInfo Create(string fileName, IMagickReadSettings<TQuantumType>? readSettings);
}
