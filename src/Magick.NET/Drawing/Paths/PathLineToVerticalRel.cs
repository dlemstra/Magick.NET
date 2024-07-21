// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a vertical line path from the current point to the target point using relative
/// coordinates. The target point then becomes the new current point.
/// </summary>
public sealed class PathLineToVerticalRel : IPathLineToVertical, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathLineToVerticalRel"/> class.
    /// </summary>
    /// <param name="y">The Y coordinate.</param>
    public PathLineToVerticalRel(double y)
    {
        Y = y;
    }

    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PathLineToVerticalRel(Y);
}
