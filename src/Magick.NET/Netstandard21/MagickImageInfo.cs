// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.Buffers;
using System.Collections.Generic;

namespace ImageMagick;

/// <content />
public sealed partial class MagickImageInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the information from.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageInfo(ReadOnlySequence<byte> data)
        => Read(data);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
    /// </summary>
    /// <param name="data">The span of bytes to read the information from.</param>
    /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageInfo(ReadOnlySpan<byte> data)
        => Read(data);

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the information from.</param>
    /// <returns>A <see cref="IMagickImageInfo"/> iteration.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public static IReadOnlyCollection<IMagickImageInfo> ReadCollection(ReadOnlySequence<byte> data)
    {
        var result = new List<MagickImageInfo>();

        using var images = new MagickImageCollection();
        images.Ping(data);
        foreach (var image in images)
        {
            var info = new MagickImageInfo();
            info.Initialize(image);
            result.Add(info);
        }

        return result;
    }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="data">The span of bytes to read the information from.</param>
    /// <returns>A <see cref="IMagickImageInfo"/> iteration.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public static IReadOnlyCollection<IMagickImageInfo> ReadCollection(ReadOnlySpan<byte> data)
    {
        var result = new List<MagickImageInfo>();

        using var images = new MagickImageCollection();
        images.Ping(data);
        foreach (var image in images)
        {
            var info = new MagickImageInfo();
            info.Initialize(image);
            result.Add(info);
        }

        return result;
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(ReadOnlySequence<byte> data)
    {
        using var image = new MagickImage();
        image.Ping(data);
        Initialize(image);
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The span of bytes to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(ReadOnlySpan<byte> data)
    {
        using var image = new MagickImage();
        image.Ping(data);
        Initialize(image);
    }
}

#endif
