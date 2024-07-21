// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a rounted rectangle given two coordinates, x &amp; y corner radiuses and using the current
/// stroke, stroke width, and fill settings.
/// </summary>
public interface IDrawableRoundRectangle : IDrawable
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
    /// Gets the lower right X coordinate.
    /// </summary>
    double LowerRightX { get; }

    /// <summary>
    /// Gets the lower right Y coordinate.
    /// </summary>
    double LowerRightY { get; }

    /// <summary>
    /// Gets the corner width.
    /// </summary>
    double CornerWidth { get; }

    /// <summary>
    /// Gets the corner height.
    /// </summary>
    double CornerHeight { get; }
}
