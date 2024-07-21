// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

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
/// Class that can be used to create <see cref="IMagickImageInfo"/> instances.
/// </summary>
public sealed partial class MagickImageInfoFactory : IMagickImageInfoFactory<QuantumType>
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    public IMagickImageInfo Create()
        => new MagickImageInfo();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(byte[] data)
        => new MagickImageInfo(data);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(byte[] data, IMagickReadSettings<QuantumType>? readSettings)
        => new MagickImageInfo(data, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(byte[] data, int offset, int count)
        => Create(data, offset, count, null);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(byte[] data, int offset, int count, IMagickReadSettings<QuantumType>? readSettings)
        => new MagickImageInfo(data, offset, count, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(FileInfo file)
        => Create(file, null);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
        => new MagickImageInfo(file, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(Stream stream)
        => Create(stream, null);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
        => new MagickImageInfo(stream, readSettings);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(string fileName)
        => Create(fileName, null);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImageInfo Create(string fileName, IMagickReadSettings<QuantumType>? readSettings)
        => new MagickImageInfo(fileName, readSettings);
}
