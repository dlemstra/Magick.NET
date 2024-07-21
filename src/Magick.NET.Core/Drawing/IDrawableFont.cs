// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the font family, style, weight and stretch to use when annotating with text.
/// </summary>
public interface IDrawableFont : IDrawable
{
    /// <summary>
    /// Gets the font family or the full path to the font file.
    /// </summary>
    string Family { get; }

    /// <summary>
    /// Gets the style of the font.
    /// </summary>
    FontStyleType Style { get; }

    /// <summary>
    /// Gets the weight of the font.
    /// </summary>
    FontWeight Weight { get; }

    /// <summary>
    /// Gets the font stretching.
    /// </summary>
    FontStretch Stretch { get; }
}
