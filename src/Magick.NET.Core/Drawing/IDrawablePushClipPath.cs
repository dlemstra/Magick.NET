// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Starts a clip path definition which is comprized of any number of drawing commands and
/// terminated by a DrawablePopClipPath.
/// </summary>
public interface IDrawablePushClipPath : IDrawable
{
    /// <summary>
    /// Gets the ID of the clip path.
    /// </summary>
    public string ClipPath { get; }
}
