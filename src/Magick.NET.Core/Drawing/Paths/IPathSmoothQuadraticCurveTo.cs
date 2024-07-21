// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a quadratic Bezier curve (using absolute coordinates) from the current point to (X, Y).
/// The control point is assumed to be the reflection of the control point on the previous
/// command relative to the current point. (If there is no previous command or if the previous
/// command was not a PathQuadraticCurveToAbs, PathQuadraticCurveToRel,
/// PathSmoothQuadraticCurveToAbs or PathSmoothQuadraticCurveToRel, assume the control point is
/// coincident with the current point.). At the end of the command, the new current point becomes
/// the final (X,Y) coordinate pair used in the polybezier.
/// </summary>
public interface IPathSmoothQuadraticCurveTo : IPath
{
    /// <summary>
    /// Gets the coordinate of final point.
    /// </summary>
    PointD End { get; }
}
