// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Draws a polygon using the current stroke, stroke width, and fill color or texture, using the
/// specified array of coordinates.
/// </summary>
public interface IDrawablePolygon : IDrawable
{
    /// <summary>
    /// Gets the coordinates.
    /// </summary>
    IReadOnlyList<PointD> Coordinates { get; }
}
