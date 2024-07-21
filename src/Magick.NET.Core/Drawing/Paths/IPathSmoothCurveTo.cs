// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a cubic Bezier curve from the current point to (x,y) using absolute coordinates. The
/// first control point is assumed to be the reflection of the second control point on the
/// previous command relative to the current point. (If there is no previous command or if the
/// previous command was not an PathCurveToAbs, PathCurveToRel, PathSmoothCurveToAbs or
/// PathSmoothCurveToRel, assume the first control point is coincident with the current point.)
/// (x2,y2) is the second control point (i.e., the control point at the end of the curve). At
/// the end of the command, the new current point becomes the final (x,y) coordinate pair used
/// in the polybezier.
/// </summary>
public interface IPathSmoothCurveTo : IPath
{
    /// <summary>
    /// Gets the coordinate of second point.
    /// </summary>
    PointD ControlPoint { get; }

    /// <summary>
    /// Gets the coordinate of final point.
    /// </summary>
    PointD End { get; }
}
