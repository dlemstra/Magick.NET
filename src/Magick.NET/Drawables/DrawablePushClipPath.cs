// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Starts a clip path definition which is comprized of any number of drawing commands and
/// terminated by a DrawablePopClipPath.
/// </summary>
public sealed class DrawablePushClipPath : IDrawablePushClipPath, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePushClipPath"/> class.
    /// </summary>
    /// <param name="clipPath">The ID of the clip path.</param>
    public DrawablePushClipPath(string clipPath)
    {
        ClipPath = clipPath;
    }

    /// <summary>
    /// Gets or sets the ID of the clip path.
    /// </summary>
    public string ClipPath { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.PushClipPath(ClipPath);
}
