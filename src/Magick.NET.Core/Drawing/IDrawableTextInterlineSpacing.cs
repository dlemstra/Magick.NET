// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the spacing between line in text.
/// </summary>
public interface IDrawableTextInterlineSpacing : IDrawable
{
    /// <summary>
    /// Gets the spacing to use.
    /// </summary>
    double Spacing { get; }
}
