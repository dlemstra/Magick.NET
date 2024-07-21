// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a circle on the image.
/// </summary>
public sealed class DrawableCircle : IDrawableCircle, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableCircle"/> class.
    /// </summary>
    /// <param name="originX">The origin X coordinate.</param>
    /// <param name="originY">The origin Y coordinate.</param>
    /// <param name="perimeterX">The perimeter X coordinate.</param>
    /// <param name="perimeterY">The perimeter Y coordinate.</param>
    public DrawableCircle(double originX, double originY, double perimeterX, double perimeterY)
    {
        OriginX = originX;
        OriginY = originY;
        PerimeterX = perimeterX;
        PerimeterY = perimeterY;
    }

    /// <summary>
    /// Gets the origin X coordinate.
    /// </summary>
    public double OriginX { get; }

    /// <summary>
    /// Gets the origin X coordinate.
    /// </summary>
    public double OriginY { get; }

    /// <summary>
    /// Gets the perimeter X coordinate.
    /// </summary>
    public double PerimeterX { get; }

    /// <summary>
    /// Gets the perimeter X coordinate.
    /// </summary>
    public double PerimeterY { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Circle(OriginX, OriginY, PerimeterX, PerimeterY);
}
