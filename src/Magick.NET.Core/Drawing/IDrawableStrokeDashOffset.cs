// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the offset into the dash pattern to start the dash.
/// </summary>
public interface IDrawableStrokeDashOffset : IDrawable
{
    /// <summary>
    /// Gets the dash offset.
    /// </summary>
    double Offset { get; }
}
