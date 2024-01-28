// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Encapsulation of the DrawableDensity object.
/// </summary>
public sealed class DrawableDensity : IDrawableDensity, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableDensity"/> class.
    /// </summary>
    /// <param name="density">The vertical and horizontal resolution.</param>
    public DrawableDensity(double density)
        => Density = new PointD(density);

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableDensity"/> class.
    /// </summary>
    /// <param name="pointDensity">The vertical and horizontal resolution.</param>
    public DrawableDensity(PointD pointDensity)
        => Density = pointDensity;

    /// <summary>
    /// Gets or sets the vertical and horizontal resolution.
    /// </summary>
    public PointD Density { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Density(Density);
}
