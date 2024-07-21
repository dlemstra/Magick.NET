// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Clones the current drawing wand to create a new drawing wand. The original drawing wand(s)
/// may be returned to by invoking DrawablePopGraphicContext. The drawing wands are stored on a
/// drawing wand stack. For every Pop there must have already been an equivalent Push.
/// </summary>
public sealed class DrawablePushGraphicContext : IDrawablePushGraphicContext, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePushGraphicContext"/> class.
    /// </summary>
    public DrawablePushGraphicContext()
    {
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PushGraphicContext();
}
