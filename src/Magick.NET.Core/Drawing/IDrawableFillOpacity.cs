// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the alpha to use when drawing using the fill color or fill texture.
/// </summary>
public interface IDrawableFillOpacity : IDrawable
{
    /// <summary>
    /// Gets the alpha.
    /// </summary>
    Percentage Opacity { get; }
}
