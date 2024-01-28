// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the font pointsize to use when annotating with text.
/// </summary>
public interface IDrawableFontPointSize : IDrawable
{
    /// <summary>
    /// Gets the point size.
    /// </summary>
    double PointSize { get; }
}
