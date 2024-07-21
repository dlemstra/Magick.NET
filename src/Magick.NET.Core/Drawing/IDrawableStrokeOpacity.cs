// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the alpha of stroked object outlines.
/// </summary>
public interface IDrawableStrokeOpacity : IDrawable
{
    /// <summary>
    /// Gets the opacity.
    /// </summary>
    Percentage Opacity { get; }
}
