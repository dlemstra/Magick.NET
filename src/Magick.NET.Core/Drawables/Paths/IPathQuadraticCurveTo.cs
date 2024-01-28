// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a quadratic Bezier curve from the current point to (x, y) using (x1, y1) as the control
/// point using absolute coordinates. At the end of the command, the new current point becomes
/// the final (x, y) coordinate pair used in the polybezier.
/// </summary>
public interface IPathQuadraticCurveTo : IPath
{
    /// <summary>
    /// Gets the coordinate of control point.
    /// </summary>
    PointD ControlPoint { get; }

    /// <summary>
    /// Gets the coordinate of final point.
    /// </summary>
    PointD End { get; }
}
