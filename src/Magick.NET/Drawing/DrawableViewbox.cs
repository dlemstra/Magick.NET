// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the overall canvas size to be recorded with the drawing vector data. Usually this will
/// be specified using the same size as the canvas image. When the vector data is saved to SVG
/// or MVG formats, the viewbox is use to specify the size of the canvas image that a viewer
/// will render the vector data on.
/// </summary>
public sealed partial class DrawableViewbox : IDrawableViewbox, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableViewbox"/> class.
    /// </summary>
    /// <param name="upperLeftX">The upper left X coordinate.</param>
    /// <param name="upperLeftY">The upper left Y coordinate.</param>
    /// <param name="lowerRightX">The lower right X coordinate.</param>
    /// <param name="lowerRightY">The lower right Y coordinate.</param>
    public DrawableViewbox(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY)
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
        => wand?.Viewbox(UpperLeftX, UpperLeftY, LowerRightX, LowerRightY);
}
