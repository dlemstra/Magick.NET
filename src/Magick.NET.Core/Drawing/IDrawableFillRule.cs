// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the fill rule to use while drawing polygons.
/// </summary>
public interface IDrawableFillRule : IDrawable
{
    /// <summary>
    /// Gets the rule to use when filling drawn objects.
    /// </summary>
    FillRule FillRule { get; }
}
