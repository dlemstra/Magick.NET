// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the alpha to use when drawing using the fill color or fill texture.
/// </summary>
public sealed class DrawableFillOpacity : IDrawableFillOpacity, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableFillOpacity"/> class.
    /// </summary>
    /// <param name="opacity">The opacity.</param>
    public DrawableFillOpacity(Percentage opacity)
    {
        Opacity = opacity;
    }

    /// <summary>
    /// Gets or sets the alpha.
    /// </summary>
    public Percentage Opacity { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.FillOpacity(Opacity.ToDouble() / 100);
}
