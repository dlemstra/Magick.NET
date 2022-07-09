// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick.ImageOptimizers
{
    /// <summary>
    /// Interface for classes that can optimize an image.
    /// </summary>
    public interface IImageOptimizer
    {
        /// <summary>
        /// Gets the format that the optimizer supports.
        /// </summary>
        IMagickFormatInfo Format { get; }

        /// <summary>
        /// Gets or sets a value indicating whether various compression types will be used to find
        /// the smallest file. This process will take extra time because the file has to be written
        /// multiple times.
        /// </summary>
        bool OptimalCompression { get; set; }

        /// <summary>
        /// Performs compression on the specified file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="file">The image file to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        bool Compress(FileInfo file);

        /// <summary>
        /// Performs compression on the specified file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        bool Compress(string fileName);

        /// <summary>
        /// Performs compression on the specified stream. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new size is not
        /// smaller the stream won't be overwritten.
        /// </summary>
        /// <param name="stream">The stream of the image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        bool Compress(Stream stream);

        /// <summary>
        /// Performs lossless compression on the specified file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="file">The image file to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        bool LosslessCompress(FileInfo file);

        /// <summary>
        /// Performs lossless compression on the specified file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        bool LosslessCompress(string fileName);

        /// <summary>
        /// Performs lossless compression on the specified stream. If the new stream size is not smaller
        /// the stream won't be overwritten.
        /// </summary>
        /// <param name="stream">The stream of the image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        bool LosslessCompress(Stream stream);
    }
}
