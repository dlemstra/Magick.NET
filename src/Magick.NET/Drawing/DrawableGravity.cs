// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the gravity to use when drawing.
/// </summary>
public sealed class DrawableGravity : IDrawableGravity, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableGravity"/> class.
    /// </summary>
    /// <param name="gravity">The gravity.</param>
    public DrawableGravity(Gravity gravity)
    {
        Gravity = gravity;
    }

    /// <summary>
    /// Gets the gravity.
    /// </summary>
    public Gravity Gravity { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Gravity(Gravity);
}
