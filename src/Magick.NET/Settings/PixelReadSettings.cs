// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <summary>
/// Class that contains setting for when pixels are read.
/// </summary>
public sealed class PixelReadSettings : IPixelReadSettings<QuantumType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PixelReadSettings"/> class.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="storageType">The pixel storage type.</param>
    /// <param name="mapping">The mapping of the pixels.</param>
    public PixelReadSettings(uint width, uint height, StorageType storageType, PixelMapping mapping)
        : this(width, height, storageType, mapping.ToString())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PixelReadSettings"/> class.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="storageType">The pixel storage type.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    public PixelReadSettings(uint width, uint height, StorageType storageType, string mapping)
    {
        ReadSettings = new MagickReadSettings
        {
            Width = width,
            Height = height,
        };
        StorageType = storageType;
        Mapping = mapping;
    }

    internal PixelReadSettings()
        => ReadSettings = new MagickReadSettings();

    /// <summary>
    /// Gets or sets the mapping of the pixels (e.g. RGB/RGBA/ARGB).
    /// </summary>
    public string? Mapping { get; set; }

    /// <summary>
    /// Gets or sets the pixel storage type.
    /// </summary>
    public StorageType StorageType { get; set; }

    /// <summary>
    /// Gets the settings to use when reading the image.
    /// </summary>
    public IMagickReadSettings<QuantumType> ReadSettings { get; }
}
