// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Applies the specified rotation to the current coordinate space.
/// </summary>
public interface IDrawableRotation : IDrawable
{
    /// <summary>
    /// Gets the angle.
    /// </summary>
    double Angle { get; }
}
