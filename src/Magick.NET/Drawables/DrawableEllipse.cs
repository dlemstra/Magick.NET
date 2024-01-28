// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws an ellipse on the image.
/// </summary>
public sealed class DrawableEllipse : IDrawableEllipse, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableEllipse"/> class.
    /// </summary>
    /// <param name="originX">The origin X coordinate.</param>
    /// <param name="originY">The origin Y coordinate.</param>
    /// <param name="radiusX">The X radius.</param>
    /// <param name="radiusY">The Y radius.</param>
    /// <param name="startDegrees">The starting degrees of rotation.</param>
    /// <param name="endDegrees">The ending degrees of rotation.</param>
    public DrawableEllipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees)
    {
        OriginX = originX;
        OriginY = originY;
        RadiusX = radiusX;
        RadiusY = radiusY;
        StartDegrees = startDegrees;
        EndDegrees = endDegrees;
    }

    /// <summary>
    /// Gets or sets the ending degrees of rotation.
    /// </summary>
    public double EndDegrees { get; set; }

    /// <summary>
    /// Gets or sets the origin X coordinate.
    /// </summary>
    public double OriginX { get; set; }

    /// <summary>
    /// Gets or sets the origin X coordinate.
    /// </summary>
    public double OriginY { get; set; }

    /// <summary>
    /// Gets or sets the X radius.
    /// </summary>
    public double RadiusX { get; set; }

    /// <summary>
    /// Gets or sets the Y radius.
    /// </summary>
    public double RadiusY { get; set; }

    /// <summary>
    /// Gets or sets the starting degrees of rotation.
    /// </summary>
    public double StartDegrees { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Ellipse(OriginX, OriginY, RadiusX, RadiusY, StartDegrees, EndDegrees);
}
