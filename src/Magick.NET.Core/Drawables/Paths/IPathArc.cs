// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Draws an elliptical arc from the current point to (X, Y) using absolute coordinates. The size
/// and orientation of the ellipse are defined by two radii(RadiusX, RadiusY) and an RotationX,
/// which indicates how the ellipse as a whole is rotated relative to the current coordinate
/// system. The center of the ellipse is calculated automagically to satisfy the constraints
/// imposed by the other parameters. UseLargeArc and UseSweep contribute to the automatic
/// calculations and help determine how the arc is drawn. If UseLargeArc is true then draw the
/// larger of the available arcs. If UseSweep is true, then draw the arc matching a clock-wise
/// rotation.
/// </summary>
public interface IPathArc : IPath
{
    /// <summary>
    /// Gets the coordinates.
    /// </summary>
    IReadOnlyList<PathArc> Coordinates { get; }
}
