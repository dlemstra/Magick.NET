// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick.Factories;

/// <summary>
/// Class that can be used to create <see cref="IMagickImage{TQuantumType}"/> instances.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public partial interface IMagickImageFactory<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(byte[] data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(byte[] data, uint offset, uint count);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(byte[] data, uint offset, uint count, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(byte[] data, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(FileInfo file);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(FileInfo file, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="color">The color to fill the image with.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    IMagickImage<TQuantumType> Create(IMagickColor<TQuantumType> color, uint width, uint height);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(Stream stream);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(string fileName);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(string fileName, uint width, uint height);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Create(string fileName, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(FileInfo file);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(FileInfo file, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(FileInfo file, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(FileInfo file, IMagickReadSettings<TQuantumType> readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(FileInfo file, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(FileInfo file, IPixelReadSettings<TQuantumType> settings, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(string fileName);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(string fileName, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(string fileName, IMagickReadSettings<TQuantumType> readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(string fileName, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(string fileName, IPixelReadSettings<TQuantumType> settings, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(Stream stream);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(Stream stream, IMagickReadSettings<TQuantumType> readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(Stream stream, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task<IMagickImage<TQuantumType>> CreateAsync(Stream stream, IPixelReadSettings<TQuantumType> settings, CancellationToken cancellationToken);
}
