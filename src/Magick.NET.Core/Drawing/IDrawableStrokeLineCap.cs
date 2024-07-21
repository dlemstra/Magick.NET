// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies the shape to be used at the end of open subpaths when they are stroked.
/// </summary>
public interface IDrawableStrokeLineCap : IDrawable
{
    /// <summary>
    /// Gets the line cap.
    /// </summary>
    LineCap LineCap { get; }
}
