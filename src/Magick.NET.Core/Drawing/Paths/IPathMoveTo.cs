// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Starts a new sub-path at the given coordinate using absolute coordinates. The current point
/// then becomes the specified coordinate.
/// </summary>
public interface IPathMoveTo : IPath
{
    /// <summary>
    /// Gets the coordinate.
    /// </summary>
    PointD Coordinate { get; }
}
