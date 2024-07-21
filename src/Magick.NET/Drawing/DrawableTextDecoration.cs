// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies a decoration to be applied when annotating with text.
/// </summary>
public sealed class DrawableTextDecoration : IDrawableTextDecoration, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableTextDecoration"/> class.
    /// </summary>
    /// <param name="decoration">The text decoration.</param>
    public DrawableTextDecoration(TextDecoration decoration)
    {
        Decoration = decoration;
    }

    /// <summary>
    /// Gets or sets the text decoration.
    /// </summary>
    public TextDecoration Decoration { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.TextDecoration(Decoration);
}
