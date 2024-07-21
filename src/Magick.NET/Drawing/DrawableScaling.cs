// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Adjusts the scaling factor to apply in the horizontal and vertical directions to the current
/// coordinate space.
/// </summary>
public sealed class DrawableScaling : IDrawableScaling, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableScaling"/> class.
    /// </summary>
    /// <param name="x">Horizontal scale factor.</param>
    /// <param name="y">Vertical scale factor.</param>
    public DrawableScaling(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Scaling(X, Y);
}
