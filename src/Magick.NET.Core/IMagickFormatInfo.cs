// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// Interface that contains information about an image format.
    /// </summary>
    public interface IMagickFormatInfo : IEquatable<IMagickFormatInfo>
    {
        /// <summary>
        /// Gets a value indicating whether the format can be read multithreaded.
        /// </summary>
        bool CanReadMultithreaded { get; }

        /// <summary>
        /// Gets a value indicating whether the format can be written multithreaded.
        /// </summary>
        bool CanWriteMultithreaded { get; }

        /// <summary>
        /// Gets the description of the format.
        /// </summary>
        string? Description { get; }

        /// <summary>
        /// Gets the format.
        /// </summary>
        MagickFormat Format { get; }

        /// <summary>
        /// Gets a value indicating whether the format supports multiple frames.
        /// </summary>
        bool IsMultiFrame { get; }

        /// <summary>
        /// Gets a value indicating whether the format is readable.
        /// </summary>
        bool IsReadable { get; }

        /// <summary>
        /// Gets a value indicating whether the format is writable.
        /// </summary>
        bool IsWritable { get; }

        /// <summary>
        /// Gets the mime type.
        /// </summary>
        string? MimeType { get; }

        /// <summary>
        /// Gets the module.
        /// </summary>
        MagickFormat ModuleFormat { get; }

        /// <summary>
        /// Returns a string that represents the current format.
        /// </summary>
        /// <returns>A string that represents the current format.</returns>
        string ToString();

        /// <summary>
        /// Unregisters this format.
        /// </summary>
        /// <returns>True when the format was found and unregistered.</returns>
        bool Unregister();
    }
}