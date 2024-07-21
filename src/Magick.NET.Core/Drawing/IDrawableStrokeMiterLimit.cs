// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Specifies the miter limit. When two line segments meet at a sharp angle and miter joins have
/// been specified for 'DrawableStrokeLineJoin', it is possible for the miter to extend far
/// beyond the thickness of the line stroking the path. The 'DrawableStrokeMiterLimit' imposes a
/// limit on the ratio of the miter length to the 'DrawableStrokeLineWidth'.
/// </summary>
public interface IDrawableStrokeMiterLimit : IDrawable
{
    /// <summary>
    /// Gets the miter limit.
    /// </summary>
    int Miterlimit { get; }
}
