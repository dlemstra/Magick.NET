// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.Buffers;

namespace ImageMagick
{
    /// <content/>
    public partial interface IMagickImage<TQuantumType>
    {
        /// <summary>
        /// Reads only metadata and not the pixel data.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the information from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(ReadOnlySequence<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

        /// <summary>
        /// Reads only metadata and not the pixel data.
        /// </summary>
        /// <param name="data">The span of bytes to read the information from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(ReadOnlySpan<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(ReadOnlySequence<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(ReadOnlySpan<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void ReadPixels(ReadOnlySpan<byte> data, IPixelReadSettings<TQuantumType>? settings);

#if !Q8
        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The span of quantum to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void ReadPixels(ReadOnlySpan<TQuantumType> data, IPixelReadSettings<TQuantumType>? settings);
#endif
    }
}

#endif
