// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies the miter limit. When two line segments meet at a sharp angle and miter joins have
/// been specified for 'DrawableStrokeLineJoin', it is possible for the miter to extend far
/// beyond the thickness of the line stroking the path. The 'DrawableStrokeMiterLimit' imposes a
/// limit on the ratio of the miter length to the 'DrawableStrokeLineWidth'.
/// </summary>
public sealed class DrawableStrokeMiterLimit : IDrawableStrokeMiterLimit, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableStrokeMiterLimit"/> class.
    /// </summary>
    /// <param name="miterlimit">The miter limit.</param>
    public DrawableStrokeMiterLimit(int miterlimit)
    {
        Miterlimit = miterlimit;
    }

    /// <summary>
    /// Gets the miter limit.
    /// </summary>
    public int Miterlimit { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.StrokeMiterLimit((uint)Miterlimit);
}
