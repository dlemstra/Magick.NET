// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Applies the specified rotation to the current coordinate space.
/// </summary>
public sealed class DrawableRotation : IDrawableRotation, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableRotation"/> class.
    /// </summary>
    /// <param name="angle">The angle.</param>
    public DrawableRotation(double angle)
    {
        Angle = angle;
    }

    /// <summary>
    /// Gets the angle.
    /// </summary>
    public double Angle { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Rotation(Angle);
}
