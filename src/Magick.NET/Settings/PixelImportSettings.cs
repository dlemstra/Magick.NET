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
/// Class that contains setting for when pixels are imported.
/// </summary>
public sealed class PixelImportSettings : IPixelImportSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PixelImportSettings"/> class.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="storageType">The pixel storage type.</param>
    /// <param name="mapping">The mapping of the pixels.</param>
    public PixelImportSettings(uint width, uint height, StorageType storageType, PixelMapping mapping)
        : this(width, height, storageType, mapping.ToString())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PixelImportSettings"/> class.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="storageType">The pixel storage type.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    public PixelImportSettings(uint width, uint height, StorageType storageType, string mapping)
        : this(0, 0, width, height, storageType, mapping)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PixelImportSettings"/> class.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="storageType">The pixel storage type.</param>
    /// <param name="mapping">The mapping of the pixels.</param>
    public PixelImportSettings(int x, int y, uint width, uint height, StorageType storageType, PixelMapping mapping)
        : this(x, y, width, height, storageType, mapping.ToString())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PixelImportSettings"/> class.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="storageType">The pixel storage type.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    public PixelImportSettings(int x, int y, uint width, uint height, StorageType storageType, string mapping)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        StorageType = storageType;
        Mapping = mapping;
    }

    /// <summary>
    /// Gets the height of the pixel area.
    /// </summary>
    public uint Height { get; }

    /// <summary>
    /// Gets the width of the pixel area.
    /// </summary>
    public uint Width { get; }

    /// <summary>
    /// Gets the X offset from origin.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the Y offset from origin.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Gets or sets the mapping of the pixels (e.g. RGB/RGBA/ARGB).
    /// </summary>
    public string Mapping { get; set; }

    /// <summary>
    /// Gets the pixel storage type.
    /// </summary>
    public StorageType StorageType { get; }
}
