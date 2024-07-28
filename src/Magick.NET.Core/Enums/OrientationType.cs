// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specified the photo orientation of the image.
/// </summary>
public enum OrientationType
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Top left.
    /// </summary>
    TopLeft,

    /// <summary>
    /// Top right.
    /// </summary>
    TopRight,

    /// <summary>
    /// Bottom right.
    /// </summary>
    BottomRight,

    /// <summary>
    /// Bottom left.
    /// </summary>
    BottomLeft,

    /// <summary>
    /// Left top.
    /// </summary>
    LeftTop,

    /// <summary>
    /// Right top.
    /// </summary>
    RightTop,

    /// <summary>
    /// Right bottom.
    /// </summary>
    RightBottom,

    /// <summary>
    /// Left bottom.
    /// </summary>
    LeftBottom,
}
