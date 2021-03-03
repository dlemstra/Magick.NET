// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to create <see cref="IMagickImageInfo"/> instances.
    /// </summary>
    public sealed class MagickImageInfoFactory : IMagickImageInfoFactory
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        public IMagickImageInfo Create()
            => new MagickImageInfo();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo Create(byte[] data)
            => new MagickImageInfo(data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo Create(byte[] data, int offset, int count)
            => new MagickImageInfo(data, offset, count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo Create(FileInfo file)
            => new MagickImageInfo(file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo Create(Stream stream)
            => new MagickImageInfo(stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo Create(string fileName)
            => new MagickImageInfo(fileName);
    }
}
