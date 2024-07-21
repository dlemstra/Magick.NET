// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the gravity to use when drawing.
/// </summary>
public interface IDrawableGravity : IDrawable
{
    /// <summary>
    /// Gets the gravity.
    /// </summary>
    Gravity Gravity { get; }
}
