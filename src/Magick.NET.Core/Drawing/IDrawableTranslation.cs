// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Applies a translation to the current coordinate system which moves the coordinate system
/// origin to the specified coordinate.
/// </summary>
public interface IDrawableTranslation : IDrawable
{
    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    double Y { get; }
}
