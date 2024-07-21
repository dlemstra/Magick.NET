// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws color on image using the current fill color, starting at specified position, and using
/// specified paint method.
/// </summary>
public interface IDrawableColor : IDrawable
{
    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    double Y { get; }

    /// <summary>
    /// Gets the PaintMethod to use.
    /// </summary>
    PaintMethod PaintMethod { get; }
}
