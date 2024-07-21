// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a rectangle given two coordinates and using the current stroke, stroke width, and fill
/// settings.
/// </summary>
public sealed partial class DrawableRectangle : IDrawableRectangle, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableRectangle"/> class.
    /// </summary>
    /// <param name="upperLeftX">The upper left X coordinate.</param>
    /// <param name="upperLeftY">The upper left Y coordinate.</param>
    /// <param name="lowerRightX">The lower right X coordinate.</param>
    /// <param name="lowerRightY">The lower right Y coordinate.</param>
    public DrawableRectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY)
    {
        UpperLeftX = upperLeftX;
        UpperLeftY = upperLeftY;
        LowerRightX = lowerRightX;
        LowerRightY = lowerRightY;
    }

    /// <summary>
    /// Gets the upper left X coordinate.
    /// </summary>
    public double UpperLeftX { get; }

    /// <summary>
    /// Gets the upper left Y coordinate.
    /// </summary>
    public double UpperLeftY { get; }

    /// <summary>
    /// Gets the upper left X coordinate.
    /// </summary>
    public double LowerRightX { get; }

    /// <summary>
    /// Gets the upper left Y coordinate.
    /// </summary>
    public double LowerRightY { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Rectangle(UpperLeftX, UpperLeftY, LowerRightX, LowerRightY);
}
