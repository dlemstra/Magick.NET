// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the spacing between words in text.
/// </summary>
public interface IDrawableTextInterwordSpacing : IDrawable
{
    /// <summary>
    /// Gets the spacing to use.
    /// </summary>
    double Spacing { get; }
}
