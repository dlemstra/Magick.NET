// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Controls whether stroked outlines are antialiased. Stroked outlines are antialiased by default.
/// When antialiasing is disabled stroked pixels are thresholded to determine if the stroke color
/// or underlying canvas color should be used.
/// </summary>
public interface IDrawableStrokeAntialias : IDrawable
{
    /// <summary>
    /// Gets a value indicating whether stroke antialiasing is enabled or disabled.
    /// </summary>
    bool IsEnabled { get; }
}
