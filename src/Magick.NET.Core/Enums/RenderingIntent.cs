// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the types of rendering intent.
/// </summary>
public enum RenderingIntent
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Saturation.
    /// </summary>
    Saturation,

    /// <summary>
    /// Perceptual.
    /// </summary>
    Perceptual,

    /// <summary>
    /// Absolute.
    /// </summary>
    Absolute,

    /// <summary>
    /// Relative.
    /// </summary>
    Relative,
}
