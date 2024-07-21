// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the polygon fill rule to be used by the clipping path.
/// </summary>
public interface IDrawableClipRule : IDrawable
{
    /// <summary>
    /// Gets the rule to use when filling drawn objects.
    /// </summary>
    FillRule FillRule { get; }
}
