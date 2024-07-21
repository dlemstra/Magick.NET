// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the width of the stroke used to draw object outlines.
/// </summary>
public interface IDrawableStrokeWidth : IDrawable
{
    /// <summary>
    /// Gets the width.
    /// </summary>
    double Width { get; }
}
