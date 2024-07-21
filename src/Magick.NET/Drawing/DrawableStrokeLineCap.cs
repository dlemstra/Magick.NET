// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies the shape to be used at the end of open subpaths when they are stroked.
/// </summary>
public sealed class DrawableStrokeLineCap : IDrawableStrokeLineCap, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableStrokeLineCap"/> class.
    /// </summary>
    /// <param name="lineCap">The line cap.</param>
    public DrawableStrokeLineCap(LineCap lineCap)
    {
        LineCap = lineCap;
    }

    /// <summary>
    /// Gets the line cap.
    /// </summary>
    public LineCap LineCap { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.StrokeLineCap(LineCap);
}
