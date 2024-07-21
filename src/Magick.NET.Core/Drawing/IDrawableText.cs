// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws text on the image.
/// </summary>
public interface IDrawableText : IDrawable
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
    /// Gets the text to draw.
    /// </summary>
    string Value { get; }
}
