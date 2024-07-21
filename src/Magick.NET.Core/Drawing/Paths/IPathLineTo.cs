// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a line path from the current point to the given coordinate using absolute coordinates.
/// The coordinate then becomes the new current point.
/// </summary>
public interface IPathLineTo : IPath
{
    /// <summary>
    /// Gets the coordinates.
    /// </summary>
    IReadOnlyList<PointD> Coordinates { get; }
}
