// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;

namespace ImageMagick;

/// <summary>
/// Draws a bezier curve through a set of points on the image.
/// </summary>
public sealed class DrawableBezier : IDrawableBezier, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableBezier"/> class.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    public DrawableBezier(params PointD[] coordinates)
        => Coordinates = new PointDCoordinates(coordinates, 3);

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableBezier"/> class.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    public DrawableBezier(IEnumerable<PointD> coordinates)
        => Coordinates = new PointDCoordinates(coordinates, 3);

    /// <summary>
    /// Gets the coordinates.
    /// </summary>
    public IReadOnlyCollection<PointD> Coordinates { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Bezier(Coordinates.ToList());
}
