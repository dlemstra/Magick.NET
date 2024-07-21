// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Skews the current coordinate system in the vertical direction.
/// </summary>
public sealed class DrawableSkewY : IDrawableSkewY, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableSkewY"/> class.
    /// </summary>
    /// <param name="angle">The angle.</param>
    public DrawableSkewY(double angle)
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
        => wand?.SkewY(Angle);
}
