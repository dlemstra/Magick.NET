// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Associates a named clipping path with the image. Only the areas drawn on by the clipping path
/// will be modified as ssize_t as it remains in effect.
/// </summary>
public interface IDrawableClipPath : IDrawable
{
    /// <summary>
    /// Gets the ID of the clip path.
    /// </summary>
    public string ClipPath { get; }
}
