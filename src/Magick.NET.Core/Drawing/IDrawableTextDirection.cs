// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies the direction to be used when annotating with text.
/// </summary>
public interface IDrawableTextDirection : IDrawable
{
    /// <summary>
    /// Gets the direction to use.
    /// </summary>
    TextDirection Direction { get; }
}
