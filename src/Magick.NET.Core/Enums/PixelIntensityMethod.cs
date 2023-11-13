// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the pixel intensity methods.
/// </summary>
public enum PixelIntensityMethod
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Average.
    /// </summary>
    Average,

    /// <summary>
    /// Brightness.
    /// </summary>
    Brightness,

    /// <summary>
    /// Lightness.
    /// </summary>
    Lightness,

    /// <summary>
    /// MS.
    /// </summary>
    MS,

    /// <summary>
    /// Rec601Luma.
    /// </summary>
    Rec601Luma,

    /// <summary>
    /// Rec601Luminance.
    /// </summary>
    Rec601Luminance,

    /// <summary>
    /// Rec709Luma.
    /// </summary>
    Rec709Luma,

    /// <summary>
    /// Rec709Luminance.
    /// </summary>
    Rec709Luminance,

    /// <summary>
    /// RMS.
    /// </summary>
    RMS,
}
