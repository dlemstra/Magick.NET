// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD

using System;
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
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadAsync(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadAsync(Stream stream, IMagickReadSettings<TQuantumType>? readSettings, CancellationToken cancellationToken);

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadPixelsAsync(Stream stream, IPixelReadSettings<TQuantumType>? settings);

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadPixelsAsync(Stream stream, IPixelReadSettings<TQuantumType>? settings, CancellationToken cancellationToken);
    }
}

#endif
