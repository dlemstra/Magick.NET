// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a rectangle given two coordinates and using the current stroke, stroke width, and fill
/// settings.
/// </summary>
public interface IDrawableRectangle : IDrawable
{
    /// <summary>
    /// Gets the upper left X coordinate.
    /// </summary>
    double UpperLeftX { get; }

    /// <summary>
    /// Gets the upper left Y coordinate.
    /// </summary>
    double UpperLeftY { get; }

    /// <summary>
    /// Gets the upper left X coordinate.
    /// </summary>
    double LowerRightX { get; }

    /// <summary>
    /// Gets the upper left Y coordinate.
    /// </summary>
    double LowerRightY { get; }
}
