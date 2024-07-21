// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Associates a named clipping path with the image. Only the areas drawn on by the clipping path
/// will be modified as ssize_t as it remains in effect.
/// </summary>
public sealed class DrawableClipPath : IDrawableClipPath, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableClipPath"/> class.
    /// </summary>
    /// <param name="clipPath">The ID of the clip path.</param>
    public DrawableClipPath(string clipPath)
    {
        Throw.IfNullOrEmpty(nameof(clipPath), clipPath);

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
        => wand?.ClipPath(ClipPath);
}
