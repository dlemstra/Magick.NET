// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ImageMagick
{
    /// <content/>
    public partial interface IMagickImageCollection<TQuantumType> : IMagickImageCollection, IList<IMagickImage<TQuantumType>>
        where TQuantumType : struct
    {
        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(ReadOnlySpan<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(ReadOnlySpan<byte> data, IMagickReadSettings<TQuantumType>? readSettings);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadAsync(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        Task ReadAsync(string fileName, IMagickReadSettings<TQuantumType>? readSettings);
    }
}

#endif
