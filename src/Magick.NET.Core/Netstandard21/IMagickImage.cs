// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageMagick
{
    /// <content/>
    public partial interface IMagickImage : IDisposable
    {
        /// <summary>
        /// Reads only metadata and not the pixel data.
        /// </summary>
        /// <param name="data">The span of byte to read the information from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(ReadOnlySpan<byte> data);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The byte span to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(ReadOnlySpan<byte> data);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The byte span to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(ReadOnlySpan<byte> data, MagickFormat format);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadAsync(FileInfo file);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadAsync(FileInfo file, MagickFormat format);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadAsync(string fileName);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadAsync(string fileName, MagickFormat format);
    }
}

#endif
