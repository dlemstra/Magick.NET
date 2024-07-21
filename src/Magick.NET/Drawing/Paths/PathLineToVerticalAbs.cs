// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a vertical line path from the current point to the target point using absolute
/// coordinates. The target point then becomes the new current point.
/// </summary>
public sealed class PathLineToVerticalAbs : IPathLineToVertical, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathLineToVerticalAbs"/> class.
    /// </summary>
    /// <param name="y">The Y coordinate.</param>
    public PathLineToVerticalAbs(double y)
    {
        Y = y;
    }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PathLineToVerticalAbs(Y);
}
