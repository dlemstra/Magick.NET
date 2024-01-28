// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the shape to be used at the corners of paths (or other vector shapes) when they
/// are stroked.
/// </summary>
public interface IDrawableStrokeLineJoin : IDrawable
{
    /// <summary>
    /// Gets the line join.
    /// </summary>
    LineJoin LineJoin { get; }
}
