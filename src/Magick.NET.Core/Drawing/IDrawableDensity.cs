// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Encapsulation of the DrawableDensity object.
/// </summary>
public interface IDrawableDensity : IDrawable
{
    /// <summary>
    /// Gets the vertical and horizontal resolution.
    /// </summary>
    PointD Density { get; }
}
