// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a horizontal line path from the current point to the target point using absolute
/// coordinates. The target point then becomes the new current point.
/// </summary>
public sealed class PathLineToHorizontalAbs : IPathLineToHorizontal, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathLineToHorizontalAbs"/> class.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    public PathLineToHorizontalAbs(double x)
    {
        X = x;
    }

    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PathLineToHorizontalAbs(X);
}
