// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.Drawing;

/// <summary>
/// indicates that subsequent commands up to a DrawablePopPattern command comprise the definition
/// of a named pattern. The pattern space is assigned top left corner coordinates, a width and
/// height, and becomes its own drawing space. Anything which can be drawn may be used in a
/// pattern definition. Named patterns may be used as stroke or brush definitions.
/// </summary>
public sealed class DrawablePushPattern : IDrawablePushPattern, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePushPattern"/> class.
    /// </summary>
    /// <param name="id">The ID of the pattern.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public DrawablePushPattern(string id, double x, double y, double width, double height)
    {
        Id = id;
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Gets or sets the ID of the pattern.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PushPattern(Id, X, Y, Width, Height);
}
