// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a cubic Bezier curve from the current point to (x, y) using (x1, y1) as the control point
/// at the beginning of the curve and (x2, y2) as the control point at the end of the curve using
/// absolute coordinates. At the end of the command, the new current point becomes the final (x, y)
/// coordinate pair used in the polybezier.
/// </summary>
public sealed class PathCurveToAbs : IPathCurveTo, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathCurveToAbs"/> class.
    /// </summary>
    /// <param name="x1">X coordinate of control point for curve beginning.</param>
    /// <param name="y1">Y coordinate of control point for curve beginning.</param>
    /// <param name="x2">X coordinate of control point for curve ending.</param>
    /// <param name="y2">Y coordinate of control point for curve ending.</param>
    /// <param name="x">X coordinate of the end of the curve.</param>
    /// <param name="y">Y coordinate of the end of the curve.</param>
    public PathCurveToAbs(double x1, double y1, double x2, double y2, double x, double y)
      : this(new PointD(x1, y1), new PointD(x2, y2), new PointD(x, y))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PathCurveToAbs"/> class.
    /// </summary>
    /// <param name="controlPointStart">Coordinate of control point for curve beginning.</param>
    /// <param name="controlPointEnd">Coordinate of control point for curve ending.</param>
    /// <param name="end">Coordinate of the end of the curve.</param>
    public PathCurveToAbs(PointD controlPointStart, PointD controlPointEnd, PointD end)
    {
        ControlPointStart = controlPointStart;
        ControlPointEnd = controlPointEnd;
        End = end;
    }

    /// <summary>
    /// Gets the coordinate of control point for curve beginning.
    /// </summary>
    public PointD ControlPointStart { get; }

    /// <summary>
    /// Gets the coordinate of control point for curve ending.
    /// </summary>
    public PointD ControlPointEnd { get; }

    /// <summary>
    /// Gets the coordinate of the end of the curve.
    /// </summary>
    public PointD End { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PathCurveToAbs(ControlPointStart, ControlPointEnd, End);
}
