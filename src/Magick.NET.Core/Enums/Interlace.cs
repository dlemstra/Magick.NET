// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the interlace types.
/// </summary>
public enum Interlace
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// No interlacing.
    /// </summary>
    NoInterlace,

    /// <summary>
    /// Line.
    /// </summary>
    Line,

    /// <summary>
    /// Plane.
    /// </summary>
    Plane,

    /// <summary>
    /// Partition.
    /// </summary>
    Partition,

    /// <summary>
    /// Gif.
    /// </summary>
    Gif,

    /// <summary>
    /// Jpeg.
    /// </summary>
    Jpeg,

    /// <summary>
    /// Png.
    /// </summary>
    Png,
}
