// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Skews the current coordinate system in the horizontal direction.
/// </summary>
public sealed class DrawableSkewX : IDrawableSkewX, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableSkewX"/> class.
    /// </summary>
    /// <param name="angle">The angle.</param>
    public DrawableSkewX(double angle)
    {
        Angle = angle;
    }

    /// <summary>
    /// Gets or sets the angle.
    /// </summary>
    public double Angle { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.SkewX(Angle);
}
