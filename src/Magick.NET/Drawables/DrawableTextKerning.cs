// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the spacing between characters in text.
/// </summary>
public sealed class DrawableTextKerning : IDrawableTextKerning, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableTextKerning"/> class.
    /// </summary>
    /// <param name="kerning">The kerning to use.</param>
    public DrawableTextKerning(double kerning)
    {
        Kerning = kerning;
    }

    /// <summary>
    /// Gets or sets the text kerning to use.
    /// </summary>
    public double Kerning { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.TextKerning(Kerning);
}
