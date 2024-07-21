// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Draws a horizontal line path from the current point to the target point using absolute
/// coordinates. The target point then becomes the new current point.
/// </summary>
public interface IPathLineToHorizontal : IPath
{
    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    double X { get; }
}
