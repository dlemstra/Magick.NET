// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
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

namespace ImageMagick;

/// <summary>
/// Class that contains basic information about an image.
/// </summary>
public sealed partial class MagickImageInfo : IMagickImageInfo<QuantumType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
    /// </summary>
    public MagickImageInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageInfo(byte[] data)
        : this()
        => Read(data);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageInfo(byte[] data, int offset, int count)
        : this()
        => Read(data, offset, count);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageInfo(FileInfo file)
        : this()
        => Read(file);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageInfo(Stream stream)
        : this()
        => Read(stream);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageInfo(string fileName)
        : this()
        => Read(fileName);

    /// <summary>
    /// Gets the color space of the image.
    /// </summary>
    public ColorSpace ColorSpace { get; private set; }

    /// <summary>
    /// Gets the compression method of the image.
    /// </summary>
    public CompressionMethod Compression { get; private set; }

    /// <summary>
    /// Gets the density of the image.
    /// </summary>
    public Density? Density { get; private set; }

    /// <summary>
    /// Gets the original file name of the image (only available if read from disk).
    /// </summary>
    public string? FileName { get; private set; }

    /// <summary>
    /// Gets the format of the image.
    /// </summary>
    public MagickFormat Format { get; private set; }

    /// <summary>
    /// Gets the height of the image.
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// Gets the type of interlacing.
    /// </summary>
    public Interlace Interlace { get; private set; }

    /// <summary>
    /// Gets the JPEG/MIFF/PNG compression level.
    /// </summary>
    public int Quality { get; private set; }

    /// <summary>
    /// Gets the width of the image.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <returns>A <see cref="IMagickImageInfo"/> iteration.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public static IEnumerable<IMagickImageInfo> ReadCollection(byte[] data)
    {
        using var images = new MagickImageCollection();
        images.Ping(data);
        foreach (var image in images)
        {
            var info = new MagickImageInfo();
            info.Initialize(image);
            yield return info;
        }
    }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A <see cref="IMagickImageInfo"/> iteration.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public static IEnumerable<IMagickImageInfo> ReadCollection(byte[] data, int offset, int count)
    {
        using var images = new MagickImageCollection();
        images.Ping(data, offset, count);
        foreach (var image in images)
        {
            var info = new MagickImageInfo();
            info.Initialize(image);
            yield return info;
        }
    }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <returns>A <see cref="IMagickImageInfo"/> iteration.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public static IEnumerable<IMagickImageInfo> ReadCollection(FileInfo file)
    {
        Throw.IfNull(nameof(file), file);

        return ReadCollection(file.FullName);
    }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A <see cref="IMagickImageInfo"/> iteration.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public static IEnumerable<IMagickImageInfo> ReadCollection(Stream stream)
    {
        using var images = new MagickImageCollection();
        images.Ping(stream);
        foreach (var image in images)
        {
            var info = new MagickImageInfo();
            info.Initialize(image);
            yield return info;
        }
    }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A <see cref="IMagickImageInfo"/> iteration.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public static IEnumerable<IMagickImageInfo> ReadCollection(string fileName)
    {
        using var images = new MagickImageCollection();
        images.Ping(fileName);
        foreach (var image in images)
        {
            var info = new MagickImageInfo();
            info.Initialize(image);
            yield return info;
        }
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data)
        => Read(data, null);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, IMagickReadSettings<QuantumType>? readSettings)
    {
        using var image = new MagickImage();
        image.Ping(data, readSettings);
        Initialize(image);
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, int offset, int count)
        => Read(data, offset, count, null);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, int offset, int count, IMagickReadSettings<QuantumType>? readSettings)
    {
        using var image = new MagickImage();
        image.Ping(data, offset, count, readSettings);
        Initialize(image);
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file)
        => Read(file, null);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
    {
        using var image = new MagickImage();
        image.Ping(file, readSettings);
        Initialize(image);
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(Stream stream)
        => Read(stream, null);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
    {
        using var image = new MagickImage();
        image.Ping(stream, readSettings);
        Initialize(image);
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName)
        => Read(fileName, null);

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName, IMagickReadSettings<QuantumType>? readSettings)
    {
        using var image = new MagickImage();
        image.Ping(fileName, readSettings);
        Initialize(image);
    }

    private void Initialize(IMagickImage<QuantumType> image)
    {
        ColorSpace = image.ColorSpace;
        Compression = image.Compression;
        Density = image.Density;
        FileName = image.FileName;
        Format = image.Format;
        Height = image.Height;
        Interlace = image.Interlace;
        Quality = image.Quality;
        Width = image.Width;
    }
}
