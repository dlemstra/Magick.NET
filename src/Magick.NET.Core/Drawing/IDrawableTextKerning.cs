// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the spacing between characters in text.
/// </summary>
public interface IDrawableTextKerning : IDrawable
{
    /// <summary>
    /// Gets the text kerning to use.
    /// </summary>
    double Kerning { get; }
}
