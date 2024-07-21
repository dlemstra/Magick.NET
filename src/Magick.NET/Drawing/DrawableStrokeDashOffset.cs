// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies the offset into the dash pattern to start the dash.
/// </summary>
public sealed class DrawableStrokeDashOffset : IDrawableStrokeDashOffset, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableStrokeDashOffset"/> class.
    /// </summary>
    /// <param name="offset">The dash offset.</param>
    public DrawableStrokeDashOffset(double offset)
    {
        Offset = offset;
    }

    /// <summary>
    /// Gets or sets the dash offset.
    /// </summary>
    public double Offset { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.StrokeDashOffset(Offset);
}
