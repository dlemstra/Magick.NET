// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws an ellipse on the image.
/// </summary>
public interface IDrawableEllipse : IDrawable
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
    /// Gets the X radius.
    /// </summary>
    double RadiusX { get; }

    /// <summary>
    /// Gets the Y radius.
    /// </summary>
    double RadiusY { get; }

    /// <summary>
    /// Gets the starting degrees of rotation.
    /// </summary>
    double StartDegrees { get; }

    /// <summary>
    /// Gets the ending degrees of rotation.
    /// </summary>
    double EndDegrees { get; }
}
