// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the paint methods.
/// </summary>
public enum PaintMethod
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Select the target pixel.
    /// </summary>
    Point,

    /// <summary>
    /// Select any pixel that matches the target pixel.
    /// </summary>
    Replace,

    /// <summary>
    /// Select the target pixel and matching neighbors.
    /// </summary>
    Floodfill,

    /// <summary>
    /// Select the target pixel and neighbors not matching border color.
    /// </summary>
    FillToBorder,

    /// <summary>
    /// Select all pixels.
    /// </summary>
    Reset,
}
