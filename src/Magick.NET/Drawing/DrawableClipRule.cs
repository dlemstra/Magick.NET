// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the polygon fill rule to be used by the clipping path.
/// </summary>
public sealed class DrawableClipRule : IDrawableClipRule, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableClipRule"/> class.
    /// </summary>
    /// <param name="fillRule">The rule to use when filling drawn objects.</param>
    public DrawableClipRule(FillRule fillRule)
    {
        FillRule = fillRule;
    }

    /// <summary>
    /// Gets the rule to use when filling drawn objects.
    /// </summary>
    public FillRule FillRule { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.ClipRule(FillRule);
}
