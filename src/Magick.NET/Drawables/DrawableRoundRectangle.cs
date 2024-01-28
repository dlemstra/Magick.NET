// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a rounted rectangle given two coordinates, x &amp; y corner radiuses and using the current
/// stroke, stroke width, and fill settings.
/// </summary>
public sealed class DrawableRoundRectangle : IDrawableRoundRectangle, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableRoundRectangle"/> class.
    /// </summary>
    /// <param name="upperLeftX">The upper left X coordinate.</param>
    /// <param name="upperLeftY">The upper left Y coordinate.</param>
    /// <param name="lowerRightX">The lower right X coordinate.</param>
    /// <param name="lowerRightY">The lower right Y coordinate.</param>
    /// <param name="cornerWidth">The corner width.</param>
    /// <param name="cornerHeight">The corner height.</param>
    public DrawableRoundRectangle(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY, double cornerWidth, double cornerHeight)
    {
        UpperLeftX = upperLeftX;
        UpperLeftY = upperLeftY;
        LowerRightX = lowerRightX;
        LowerRightY = lowerRightY;
        CornerWidth = cornerWidth;
        CornerHeight = cornerHeight;
    }

    /// <summary>
    /// Gets or sets the upper left X coordinate.
    /// </summary>
    public double UpperLeftX { get; set; }

    /// <summary>
    /// Gets or sets the upper left Y coordinate.
    /// </summary>
    public double UpperLeftY { get; set; }

    /// <summary>
    /// Gets or sets the lower right X coordinate.
    /// </summary>
    public double LowerRightX { get; set; }

    /// <summary>
    /// Gets or sets the lower right Y coordinate.
    /// </summary>
    public double LowerRightY { get; set; }

    /// <summary>
    /// Gets or sets the corner width.
    /// </summary>
    public double CornerWidth { get; set; }

    /// <summary>
    /// Gets or sets the corner height.
    /// </summary>
    public double CornerHeight { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.RoundRectangle(UpperLeftX, UpperLeftY, LowerRightX, LowerRightY, CornerWidth, CornerHeight);
}
