// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick
{
    /// <content/>
    public partial interface IMagickImage<TQuantumType> : IMagickImage, IEquatable<IMagickImage<TQuantumType>>, IComparable<IMagickImage<TQuantumType>>
        where TQuantumType : struct
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

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadPixelsAsync(FileInfo file, IPixelReadSettings<TQuantumType>? settings);

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadPixelsAsync(FileInfo file, IPixelReadSettings<TQuantumType>? settings, CancellationToken cancellationToken);

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadPixelsAsync(string fileName, IPixelReadSettings<TQuantumType>? settings);

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadPixelsAsync(string fileName, IPixelReadSettings<TQuantumType>? settings, CancellationToken cancellationToken);
    }
}

#endif
