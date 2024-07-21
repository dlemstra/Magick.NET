// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the overall canvas size to be recorded with the drawing vector data. Usually this will
/// be specified using the same size as the canvas image. When the vector data is saved to SVG
/// or MVG formats, the viewbox is use to specify the size of the canvas image that a viewer
/// will render the vector data on.
/// </summary>
public interface IDrawableViewbox : IDrawable
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
