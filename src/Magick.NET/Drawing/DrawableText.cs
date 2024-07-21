// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws text on the image.
/// </summary>
public sealed class DrawableText : IDrawableText, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableText"/> class.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="value">The text to draw.</param>
    public DrawableText(double x, double y, string value)
    {
        Throw.IfNullOrEmpty(nameof(value), value);

        X = x;
        Y = y;
        Value = value;
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
    /// Gets the text to draw.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Text(X, Y, Value);
}
