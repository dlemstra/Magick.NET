// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the spacing between words in text.
/// </summary>
public sealed class DrawableTextInterwordSpacing : IDrawableTextInterwordSpacing, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableTextInterwordSpacing"/> class.
    /// </summary>
    /// <param name="spacing">The spacing to use.</param>
    public DrawableTextInterwordSpacing(double spacing)
    {
        Spacing = spacing;
    }

    /// <summary>
    /// Gets the spacing to use.
    /// </summary>
    public double Spacing { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.TextInterwordSpacing(Spacing);
}
