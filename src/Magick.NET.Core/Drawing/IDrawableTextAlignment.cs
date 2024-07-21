// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies a text alignment to be applied when annotating with text.
/// </summary>
public interface IDrawableTextAlignment : IDrawable
{
    /// <summary>
    /// Gets text alignment.
    /// </summary>
    TextAlignment Alignment { get; }
}
