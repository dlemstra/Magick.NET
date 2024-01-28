// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Draws a bezier curve through a set of points on the image.
/// </summary>
public interface IDrawableBezier : IDrawable
{
    /// <summary>
    /// Gets the coordinates.
    /// </summary>
    IReadOnlyCollection<PointD> Coordinates { get; }
}
