// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Draws a polyline using the current stroke, stroke width, and fill color or texture, using the
/// specified array of coordinates.
/// </summary>
public sealed class DrawablePolyline : IDrawablePolyline, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePolyline"/> class.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    public DrawablePolyline(params PointD[] coordinates)
        => Coordinates = new PointDCoordinates(coordinates, 3);

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePolyline"/> class.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    public DrawablePolyline(IEnumerable<PointD> coordinates)
        => Coordinates = new PointDCoordinates(coordinates, 3);

    /// <summary>
    /// Gets the coordinates.
    /// </summary>
    public IReadOnlyList<PointD> Coordinates { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Polyline(Coordinates);
}
