// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies a text alignment to be applied when annotating with text.
/// </summary>
public sealed class DrawableTextAlignment : IDrawableTextAlignment, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableTextAlignment"/> class.
    /// </summary>
    /// <param name="alignment">The text alignment.</param>
    public DrawableTextAlignment(TextAlignment alignment)
    {
        Alignment = alignment;
    }

    /// <summary>
    /// Gets or sets text alignment.
    /// </summary>
    public TextAlignment Alignment { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.TextAlignment(Alignment);
}
