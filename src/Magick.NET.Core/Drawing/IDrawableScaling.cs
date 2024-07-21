// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Adjusts the scaling factor to apply in the horizontal and vertical directions to the current
/// coordinate space.
/// </summary>
public interface IDrawableScaling : IDrawable
{
    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    double Y { get; }
}
