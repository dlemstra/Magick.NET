// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a horizontal line path from the current point to the target point using relative
/// coordinates. The target point then becomes the new current point.
/// </summary>
public sealed class PathLineToHorizontalRel : IPathLineToHorizontal, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathLineToHorizontalRel"/> class.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    public PathLineToHorizontalRel(double x)
    {
        X = x;
    }

    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PathLineToHorizontalRel(X);
}
