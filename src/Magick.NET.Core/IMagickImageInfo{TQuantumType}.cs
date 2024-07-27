// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick;

/// <summary>
/// Interface that contains basic information about an image.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public partial interface IMagickImageInfo<TQuantumType> : IMagickImageInfo
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, uint offset, uint count, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName, IMagickReadSettings<TQuantumType>? readSettings);
}
