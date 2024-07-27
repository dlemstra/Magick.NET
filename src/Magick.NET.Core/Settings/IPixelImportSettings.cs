// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for when pixels are imported.
/// </summary>
public interface IPixelImportSettings
{
    /// <summary>
    /// Gets the height of the pixel area.
    /// </summary>
    uint Height { get; }

    /// <summary>
    /// Gets the width of the pixel area.
    /// </summary>
    uint Width { get; }

    /// <summary>
    /// Gets the X offset from origin.
    /// </summary>
    int X { get; }

    /// <summary>
    /// Gets the Y offset from origin.
    /// </summary>
    int Y { get; }

    /// <summary>
    /// Gets or sets the mapping of the pixels (e.g. RGB/RGBA/ARGB).
    /// </summary>
    string Mapping { get; set; }

    /// <summary>
    /// Gets the pixel storage type.
    /// </summary>
    StorageType StorageType { get; }
}
