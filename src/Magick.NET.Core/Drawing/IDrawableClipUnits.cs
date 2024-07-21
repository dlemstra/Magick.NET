// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the interpretation of clip path units.
/// </summary>
public interface IDrawableClipUnits : IDrawable
{
    /// <summary>
    /// Gets the clip path units.
    /// </summary>
    ClipPathUnit Units { get; }
}
