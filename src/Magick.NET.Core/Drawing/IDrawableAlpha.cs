// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Adjusts the current affine transformation matrix with the specified affine transformation
/// matrix. Note that the current affine transform is adjusted rather than replaced.
/// </summary>
public interface IDrawableAlpha : IDrawable
{
    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    double Y { get; }

    /// <summary>
    /// Getsthe <see cref="PaintMethod"/> to use.
    /// </summary>
    PaintMethod PaintMethod { get; }
}
