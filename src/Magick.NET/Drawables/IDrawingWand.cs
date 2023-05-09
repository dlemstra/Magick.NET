// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Interface for drawing on an wand.
/// </summary>
internal interface IDrawingWand
{
    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The wand to draw on.</param>
    void Draw(DrawingWand wand);
}
