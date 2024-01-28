// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws color on image using the current fill color, starting at specified position, and using
/// specified paint method.
/// </summary>
public sealed class DrawableColor : IDrawableColor, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableColor"/> class.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="paintMethod">The paint method to use.</param>
    public DrawableColor(double x, double y, PaintMethod paintMethod)
    {
        X = x;
        Y = y;
        PaintMethod = paintMethod;
    }

    /// <summary>
    /// Gets or sets the PaintMethod to use.
    /// </summary>
    public PaintMethod PaintMethod { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Color(X, Y, PaintMethod);
}
