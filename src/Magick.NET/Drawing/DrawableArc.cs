// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws an arc falling within a specified bounding rectangle on the image.
/// </summary>
public sealed class DrawableArc : IDrawableArc, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableArc"/> class.
    /// </summary>
    /// <param name="startX">The starting X coordinate of the bounding rectangle.</param>
    /// <param name="startY">The starting Y coordinate of thebounding rectangle.</param>
    /// <param name="endX">The ending X coordinate of the bounding rectangle.</param>
    /// <param name="endY">The ending Y coordinate of the bounding rectangle.</param>
    /// <param name="startDegrees">The starting degrees of rotation.</param>
    /// <param name="endDegrees">The ending degrees of rotation.</param>
    public DrawableArc(double startX, double startY, double endX, double endY, double startDegrees, double endDegrees)
    {
        StartX = startX;
        StartY = startY;
        EndX = endX;
        EndY = endY;
        StartDegrees = startDegrees;
        EndDegrees = endDegrees;
    }

    /// <summary>
    /// Gets or sets the starting X coordinate of the bounding rectangle.
    /// </summary>
    public double StartX { get; set; }

    /// <summary>
    /// Gets or sets the starting Y coordinate of the bounding rectangle.
    /// </summary>
    public double StartY { get; set; }

    /// <summary>
    /// Gets or sets the ending X coordinate of the bounding rectangle.
    /// </summary>
    public double EndX { get; set; }

    /// <summary>
    /// Gets or sets the ending Y coordinate of the bounding rectangle.
    /// </summary>
    public double EndY { get; set; }

    /// <summary>
    /// Gets or sets the starting degrees of rotation.
    /// </summary>
    public double StartDegrees { get; set; }

    /// <summary>
    /// Gets or sets the ending degrees of rotation.
    /// </summary>
    public double EndDegrees { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Arc(StartX, StartY, EndX, EndY, StartDegrees, EndDegrees);
}
