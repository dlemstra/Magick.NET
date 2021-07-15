// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD

using System.IO;
using System.Threading.Tasks;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <content />
    public sealed partial class MagickImageCollectionFactory : IMagickImageCollectionFactory<QuantumType>
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task<IMagickImageCollection<QuantumType>> CreateAsync(Stream stream)
        {
            var images = new MagickImageCollection();
            await images.ReadAsync(stream).ConfigureAwait(false);

            return images;
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task<IMagickImageCollection<QuantumType>> CreateAsync(Stream stream, IMagickReadSettings<QuantumType> readSettings)
        {
            var images = new MagickImageCollection();
            await images.ReadAsync(stream, readSettings).ConfigureAwait(false);

            return images;
        }
    }
}

#endif
