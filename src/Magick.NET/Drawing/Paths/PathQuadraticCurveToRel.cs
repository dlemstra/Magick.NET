// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a quadratic Bezier curve from the current point to (x, y) using (x1, y1) as the control
/// point using relative coordinates. At the end of the command, the new current point becomes
/// the final (x, y) coordinate pair used in the polybezier.
/// </summary>
public sealed class PathQuadraticCurveToRel : IPathQuadraticCurveTo, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathQuadraticCurveToRel"/> class.
    /// </summary>
    /// <param name="x1">X coordinate of control point.</param>
    /// <param name="y1">Y coordinate of control point.</param>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    public PathQuadraticCurveToRel(double x1, double y1, double x, double y)
      : this(new PointD(x1, y1), new PointD(x, y))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PathQuadraticCurveToRel"/> class.
    /// </summary>
    /// <param name="controlPoint">Coordinate of control point.</param>
    /// <param name="end">Coordinate of final point.</param>
    public PathQuadraticCurveToRel(PointD controlPoint, PointD end)
    {
        ControlPoint = controlPoint;
        End = end;
    }

    /// <summary>
    /// Gets the coordinate of control point.
    /// </summary>
    public PointD ControlPoint { get; }

    /// <summary>
    /// Gets the coordinate of final point.
    /// </summary>
    public PointD End { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PathQuadraticCurveToRel(ControlPoint, End);
}
