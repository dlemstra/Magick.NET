// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick.Factories;

/// <summary>
/// Class that can be used to create <see cref="IMagickImageCollection{TQuantumType}"/> instances.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public partial interface IMagickImageCollectionFactory<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    IMagickImageCollection<TQuantumType> Create();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(byte[] data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(byte[] data, int offset, int count);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(byte[] data, int offset, int count, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(byte[] data, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(FileInfo file);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(FileInfo file, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="images">The images to add to the collection.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(IEnumerable<IMagickImage<TQuantumType>> images);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(Stream stream);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(string fileName);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImageCollection<TQuantumType> Create(string fileName, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(FileInfo file);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(FileInfo file, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(FileInfo file, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(FileInfo file, IMagickReadSettings<TQuantumType> readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(string fileName);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(string fileName, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(string fileName, IMagickReadSettings<TQuantumType> readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(Stream stream);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImageCollection<TQuantumType>> CreateAsync(Stream stream, IMagickReadSettings<TQuantumType> readSettings, CancellationToken cancellationToken);
}
