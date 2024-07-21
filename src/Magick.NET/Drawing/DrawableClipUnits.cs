// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the interpretation of clip path units.
/// </summary>
public sealed class DrawableClipUnits : IDrawableClipUnits, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableClipUnits"/> class.
    /// </summary>
    /// <param name="units">The clip path units.</param>
    public DrawableClipUnits(ClipPathUnit units)
    {
        Units = units;
    }

    /// <summary>
    /// Gets the clip path units.
    /// </summary>
    public ClipPathUnit Units { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.ClipUnits(Units);
}
