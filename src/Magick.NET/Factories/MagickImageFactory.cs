// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Threading;
using System.Threading.Tasks;

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

/// <summary>
/// Class that can be used to create <see cref="IMagickImage{QuantumType}"/> instances.
/// </summary>
public sealed partial class MagickImageFactory : IMagickImageFactory<QuantumType>
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create()
        => new MagickImage();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType>"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(byte[] data)
        => new MagickImage(data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A new <see cref="IMagickImage{QuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(byte[] data, int offset, int count)
        => new MagickImage(data, offset, count);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(byte[] data, int offset, int count, IMagickReadSettings<QuantumType> readSettings)
        => new MagickImage(data, offset, count, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="IMagickImage{TQuantumType}"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(byte[] data, IMagickReadSettings<QuantumType> readSettings)
        => new MagickImage(data, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A new <see cref="MagickImage"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(FileInfo file)
        => new MagickImage(file);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(FileInfo file, IMagickReadSettings<QuantumType> readSettings)
        => new MagickImage(file, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="color">The color to fill the image with.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    public IMagickImage<QuantumType> Create(IMagickColor<QuantumType> color, int width, int height)
        => new MagickImage(color, width, height);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(Stream stream)
        => new MagickImage(stream);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(Stream stream, IMagickReadSettings<QuantumType> readSettings)
        => new MagickImage(stream, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(string fileName)
        => new MagickImage(fileName);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(string fileName, int width, int height)
        => new MagickImage(fileName, width, height);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Create(string fileName, IMagickReadSettings<QuantumType> readSettings)
        => new MagickImage(fileName, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(FileInfo file)
        => CreateAsync(file, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(FileInfo file, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadAsync(file, cancellationToken).ConfigureAwait(false);

        return image;
    }

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(FileInfo file, IMagickReadSettings<QuantumType> readSettings)
        => CreateAsync(file, readSettings, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(FileInfo file, IMagickReadSettings<QuantumType> readSettings, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadAsync(file, readSettings, cancellationToken).ConfigureAwait(false);

        return image;
    }

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(FileInfo file, IPixelReadSettings<QuantumType> settings)
        => CreateAsync(file, settings, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(FileInfo file, IPixelReadSettings<QuantumType> settings, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadPixelsAsync(file, settings, cancellationToken).ConfigureAwait(false);

        return image;
    }

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(Stream stream)
        => CreateAsync(stream, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(Stream stream, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadAsync(stream, cancellationToken).ConfigureAwait(false);

        return image;
    }

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(Stream stream, IMagickReadSettings<QuantumType> readSettings)
        => CreateAsync(stream, readSettings, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(Stream stream, IMagickReadSettings<QuantumType> readSettings, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadAsync(stream, readSettings, cancellationToken).ConfigureAwait(false);

        return image;
    }

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(Stream stream, IPixelReadSettings<QuantumType> settings)
        => CreateAsync(stream, settings, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(Stream stream, IPixelReadSettings<QuantumType> settings, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadPixelsAsync(stream, settings, cancellationToken).ConfigureAwait(false);

        return image;
    }

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(string fileName)
        => CreateAsync(fileName, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(string fileName, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadAsync(fileName, cancellationToken).ConfigureAwait(false);

        return image;
    }

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(string fileName, IMagickReadSettings<QuantumType> readSettings)
        => CreateAsync(fileName, readSettings, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(string fileName, IMagickReadSettings<QuantumType> readSettings, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadAsync(fileName, readSettings, cancellationToken).ConfigureAwait(false);

        return image;
    }

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task<IMagickImage<QuantumType>> CreateAsync(string fileName, IPixelReadSettings<QuantumType> settings)
        => CreateAsync(fileName, settings, CancellationToken.None);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task<IMagickImage<QuantumType>> CreateAsync(string fileName, IPixelReadSettings<QuantumType> settings, CancellationToken cancellationToken)
    {
        var image = new MagickImage();
        await image.ReadPixelsAsync(fileName, settings, cancellationToken).ConfigureAwait(false);

        return image;
    }
}
