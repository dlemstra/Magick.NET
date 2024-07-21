// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws an arc falling within a specified bounding rectangle on the image.
/// </summary>
public interface IDrawableArc : IDrawable
{
    /// <summary>
    /// Gets the starting X coordinate of the bounding rectangle.
    /// </summary>
    double StartX { get; }

    /// <summary>
    /// Gets the starting Y coordinate of the bounding rectangle.
    /// </summary>
    double StartY { get; }

    /// <summary>
    /// Gets the ending X coordinate of the bounding rectangle.
    /// </summary>
    double EndX { get; }

    /// <summary>
    /// Gets the ending Y coordinate of the bounding rectangle.
    /// </summary>
    double EndY { get; }

    /// <summary>
    /// Gets  the ending degrees of rotation.
    /// </summary>
    double EndDegrees { get; }

    /// <summary>
    /// Gets the starting degrees of rotation.
    /// </summary>
    public double StartDegrees { get; }
}
