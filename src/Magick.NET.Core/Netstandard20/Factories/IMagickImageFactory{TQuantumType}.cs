// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD

using System.IO;
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
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task<IMagickImage<TQuantumType>> CreateAsync(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task<IMagickImage<TQuantumType>> CreateAsync(Stream stream, IPixelReadSettings<TQuantumType> settings);
    }
}

#endif
