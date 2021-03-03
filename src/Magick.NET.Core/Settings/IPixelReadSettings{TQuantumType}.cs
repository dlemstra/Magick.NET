// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for when pixels are read.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IPixelReadSettings<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets or sets the mapping of the pixels (e.g. RGB/RGBA/ARGB).
        /// </summary>
        string? Mapping { get; set; }

        /// <summary>
        /// Gets or sets the pixel storage type.
        /// </summary>
        StorageType StorageType { get; set; }

        /// <summary>
        /// Gets the settings to use when reading the image.
        /// </summary>
        IMagickReadSettings<TQuantumType> ReadSettings { get; }
    }
}