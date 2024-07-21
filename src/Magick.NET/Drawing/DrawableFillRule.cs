// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the fill rule to use while drawing polygons.
/// </summary>
public sealed class DrawableFillRule : IDrawableFillRule, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableFillRule"/> class.
    /// </summary>
    /// <param name="fillRule">The rule to use when filling drawn objects.</param>
    public DrawableFillRule(FillRule fillRule)
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
        => wand?.FillRule(FillRule);
}
