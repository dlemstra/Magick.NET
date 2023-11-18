// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the spacing between words in text.
/// </summary>
public sealed class DrawableTextInterwordSpacing : IDrawable, IDrawingWand
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
    /// Gets or sets the spacing to use.
    /// </summary>
    public double Spacing { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.TextInterwordSpacing(Spacing);
}
