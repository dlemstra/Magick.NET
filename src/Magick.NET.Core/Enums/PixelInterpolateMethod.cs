// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the pixel interpolate methods.
/// </summary>
public enum PixelInterpolateMethod
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
    /// Average9.
    /// </summary>
    Average9,

    /// <summary>
    /// Average16.
    /// </summary>
    Average16,

    /// <summary>
    /// Background.
    /// </summary>
    Background,

    /// <summary>
    /// Bilinear.
    /// </summary>
    Bilinear,

    /// <summary>
    /// Blend.
    /// </summary>
    Blend,

    /// <summary>
    /// Catrom.
    /// </summary>
    Catrom,

    /// <summary>
    /// Integer.
    /// </summary>
    Integer,

    /// <summary>
    /// Mesh.
    /// </summary>
    Mesh,

    /// <summary>
    /// Nearest.
    /// </summary>
    Nearest,

    /// <summary>
    /// Spline.
    /// </summary>
    Spline,
}
