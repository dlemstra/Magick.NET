// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Terminates a pattern definition.
/// </summary>
public sealed class DrawablePopPattern : IDrawablePopPattern, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePopPattern"/> class.
    /// </summary>
    public DrawablePopPattern()
    {
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PopPattern();
}
