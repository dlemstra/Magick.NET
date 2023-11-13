// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the virtual pixel methods.
/// </summary>
public enum VirtualPixelMethod
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Background.
    /// </summary>
    Background,

    /// <summary>
    /// Dither.
    /// </summary>
    Dither,

    /// <summary>
    /// Edge.
    /// </summary>
    Edge,

    /// <summary>
    /// Mirror.
    /// </summary>
    Mirror,

    /// <summary>
    /// Random.
    /// </summary>
    Random,

    /// <summary>
    /// Tile.
    /// </summary>
    Tile,

    /// <summary>
    /// Transparent.
    /// </summary>
    Transparent,

    /// <summary>
    /// Mask.
    /// </summary>
    Mask,

    /// <summary>
    /// Black.
    /// </summary>
    Black,

    /// <summary>
    /// Gray.
    /// </summary>
    Gray,

    /// <summary>
    /// White.
    /// </summary>
    White,

    /// <summary>
    /// Horizontal tile.
    /// </summary>
    HorizontalTile,

    /// <summary>
    /// Vertical tile.
    /// </summary>
    VerticalTile,

    /// <summary>
    /// Horizontal tile edge.
    /// </summary>
    HorizontalTileEdge,

    /// <summary>
    /// Vertical tile edge.
    /// </summary>
    VerticalTileEdge,

    /// <summary>
    /// Checker tile.
    /// </summary>
    CheckerTile,
}
