// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick
{
    /// <content />
    public partial interface IMagickImageFactory<TQuantumType>
        where TQuantumType : struct
    {
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
}
