// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a cubic Bezier curve from the current point to (x, y) using (x1, y1) as the control point
/// at the beginning of the curve and (x2, y2) as the control point at the end of the curve using
/// absolute coordinates. At the end of the command, the new current point becomes the final (x, y)
/// coordinate pair used in the polybezier.
/// </summary>
public interface IPathCurveTo : IPath
{
    /// <summary>
    /// Gets the coordinate of control point for curve beginning.
    /// </summary>
    PointD ControlPointStart { get; }

    /// <summary>
    /// Gets the coordinate of control point for curve ending.
    /// </summary>
    PointD ControlPointEnd { get; }

    /// <summary>
    /// Gets the coordinate of the end of the curve.
    /// </summary>
    PointD End { get; }
}
