// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Draws an elliptical arc from the current point to(X, Y). The size and orientation of the
/// ellipse are defined by two radii(RadiusX, RadiusY) and a RotationX, which indicates how the
/// ellipse as a whole is rotated relative to the current coordinate system. The center of the
/// ellipse is calculated automagically to satisfy the constraints imposed by the other
/// parameters. UseLargeArc and UseSweep contribute to the automatic calculations and help
/// determine how the arc is drawn. If UseLargeArc is true then draw the larger of the
/// available arcs. If UseSweep is true, then draw the arc matching a clock-wise rotation.
/// </summary>
public sealed class PathArc
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathArc"/> class.
    /// </summary>
    public PathArc()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PathArc"/> class.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="radiusX">The X radius.</param>
    /// <param name="radiusY">The Y radius.</param>
    /// <param name="rotationX">Indicates how the ellipse as a whole is rotated relative to the
    /// current coordinate system.</param>
    /// <param name="useLargeArc">If true then draw the larger of the available arcs.</param>
    /// <param name="useSweep">If true then draw the arc matching a clock-wise rotation.</param>
    public PathArc(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc, bool useSweep)
    {
        X = x;
        Y = y;
        RadiusX = radiusX;
        RadiusY = radiusY;
        RotationX = rotationX;
        UseLargeArc = useLargeArc;
        UseSweep = useSweep;
    }

    /// <summary>
    /// Gets or sets the X offset from origin.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y offset from origin.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the X radius.
    /// </summary>
    public double RadiusX { get; set; }

    /// <summary>
    /// Gets or sets the Y radius.
    /// </summary>
    public double RadiusY { get; set; }

    /// <summary>
    /// Gets or sets how the ellipse as a whole is rotated relative to the current coordinate system.
    /// </summary>
    public double RotationX { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whetherthe larger of the available arcs should be drawn.
    /// </summary>
    public bool UseLargeArc { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the arc should be drawn matching a clock-wise rotation.
    /// </summary>
    public bool UseSweep { get; set; }
}
