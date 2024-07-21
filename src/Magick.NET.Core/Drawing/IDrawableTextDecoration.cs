// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies a decoration to be applied when annotating with text.
/// </summary>
public interface IDrawableTextDecoration : IDrawable
{
    /// <summary>
    /// Gets the text decoration.
    /// </summary>
    TextDecoration Decoration { get; }
}
