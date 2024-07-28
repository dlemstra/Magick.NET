// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;
using System.Buffers;

namespace ImageMagick;

/// <content/>
public partial interface IMagickImageCollection
{
    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(ReadOnlySequence<byte> data);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(ReadOnlySpan<byte> data);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(ReadOnlySequence<byte> data);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(ReadOnlySequence<byte> data, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(ReadOnlySpan<byte> data);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(ReadOnlySpan<byte> data, MagickFormat format);

    /// <summary>
    /// Writes the images to the specified buffer writter. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="bufferWriter">The buffer writer to write the images to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(IBufferWriter<byte> bufferWriter);

    /// <summary>
    /// Writes the images to the specified buffer writter. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="bufferWriter">The buffer writer to write the images to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(IBufferWriter<byte> bufferWriter, IWriteDefines defines);

    /// <summary>
    /// Writes the images to the specified buffer writter. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="bufferWriter">The buffer writer to write the images to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(IBufferWriter<byte> bufferWriter, MagickFormat format);
}

#endif
