// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a polyline using the current stroke, stroke width, and fill color or texture, using the
/// specified array of coordinates.
/// </summary>
public interface IDrawablePolyline : IDrawable
{
    /// <summary>
    /// Gets the coordinates.
    /// </summary>
    IReadOnlyList<PointD> Coordinates { get; }
}
