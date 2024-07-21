// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a circle on the image.
/// </summary>
public interface IDrawableCircle : IDrawable
{
    /// <summary>
    /// Gets the origin X coordinate.
    /// </summary>
    double OriginX { get; }

    /// <summary>
    /// Gets the origin X coordinate.
    /// </summary>
    double OriginY { get; }

    /// <summary>
    /// Gets the perimeter X coordinate.
    /// </summary>
    double PerimeterX { get; }

    /// <summary>
    /// Gets the perimeter X coordinate.
    /// </summary>
    double PerimeterY { get; }
}
