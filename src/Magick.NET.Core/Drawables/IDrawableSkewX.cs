// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Skews the current coordinate system in the horizontal direction.
/// </summary>
public interface IDrawableSkewX : IDrawable
{
    /// <summary>
    /// Gets the angle.
    /// </summary>
    double Angle { get; }
}
