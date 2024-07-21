// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies the direction to be used when annotating with text.
/// </summary>
public sealed class DrawableTextDirection : IDrawableTextDirection, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableTextDirection"/> class.
    /// </summary>
    /// <param name="direction">Direction to use.</param>
    public DrawableTextDirection(TextDirection direction)
    {
        Direction = direction;
    }

    /// <summary>
    /// Gets the direction to use.
    /// </summary>
    public TextDirection Direction { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.TextDirection(Direction);
}
