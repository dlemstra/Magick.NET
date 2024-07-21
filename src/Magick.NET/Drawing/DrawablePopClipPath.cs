// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Terminates a clip path definition.
/// </summary>
public sealed class DrawablePopClipPath : IDrawablePopClipPath, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePopClipPath"/> class.
    /// </summary>
    public DrawablePopClipPath()
    {
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PopClipPath();
}
