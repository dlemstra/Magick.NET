// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a vertical line path from the current point to the target point using absolute
/// coordinates. The target point then becomes the new current point.
/// </summary>
public interface IPathLineToVertical : IPath
{
    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    double Y { get; }
}
