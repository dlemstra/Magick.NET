// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a line on the image using the current stroke color, stroke alpha, and stroke width.
/// </summary>
public interface IDrawableLine : IDrawable
{
    /// <summary>
    /// Gets the starting X coordinate.
    /// </summary>
    double StartX { get; }

    /// <summary>
    /// Gets the starting Y coordinate.
    /// </summary>
    double StartY { get; }

    /// <summary>
    /// Gets the ending X coordinate.
    /// </summary>
    double EndX { get; }

    /// <summary>
    /// Gets the ending Y coordinate.
    /// </summary>
    double EndY { get; }
}
