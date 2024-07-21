// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies the alpha of stroked object outlines.
/// </summary>
public sealed class DrawableStrokeOpacity : IDrawableStrokeOpacity, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableStrokeOpacity"/> class.
    /// </summary>
    /// <param name="opacity">The opacity.</param>
    public DrawableStrokeOpacity(Percentage opacity)
    {
        Opacity = opacity;
    }

    /// <summary>
    /// Gets or sets the opacity.
    /// </summary>
    public Percentage Opacity { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.StrokeOpacity((double)Opacity / 100);
}
