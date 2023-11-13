// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the filter types.
/// </summary>
public enum FilterType
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Point.
    /// </summary>
    Point,

    /// <summary>
    /// Box.
    /// </summary>
    Box,

    /// <summary>
    /// Triangle.
    /// </summary>
    Triangle,

    /// <summary>
    /// Hermite.
    /// </summary>
    Hermite,

    /// <summary>
    /// Hann.
    /// </summary>
    Hann,

    /// <summary>
    /// Hamming.
    /// </summary>
    Hamming,

    /// <summary>
    /// Blackman.
    /// </summary>
    Blackman,

    /// <summary>
    /// Gaussian.
    /// </summary>
    Gaussian,

    /// <summary>
    /// Quadratic.
    /// </summary>
    Quadratic,

    /// <summary>
    /// Cubic.
    /// </summary>
    Cubic,

    /// <summary>
    /// Catrom.
    /// </summary>
    Catrom,

    /// <summary>
    /// Mitchell.
    /// </summary>
    Mitchell,

    /// <summary>
    /// Jinc.
    /// </summary>
    Jinc,

    /// <summary>
    /// Sinc.
    /// </summary>
    Sinc,

    /// <summary>
    /// Sinc fast.
    /// </summary>
    SincFast,

    /// <summary>
    /// Kaiser.
    /// </summary>
    Kaiser,

    /// <summary>
    /// Welch.
    /// </summary>
    Welch,

    /// <summary>
    /// Parzen.
    /// </summary>
    Parzen,

    /// <summary>
    /// Bohman.
    /// </summary>
    Bohman,

    /// <summary>
    /// Bartlett.
    /// </summary>
    Bartlett,

    /// <summary>
    /// Lagrange.
    /// </summary>
    Lagrange,

    /// <summary>
    /// Lanczos.
    /// </summary>
    Lanczos,

    /// <summary>
    /// Lanczos sharp.
    /// </summary>
    LanczosSharp,

    /// <summary>
    /// Lanczos 2.
    /// </summary>
    Lanczos2,

    /// <summary>
    /// Lanczos 2 sharp.
    /// </summary>
    Lanczos2Sharp,

    /// <summary>
    /// Robidoux.
    /// </summary>
    Robidoux,

    /// <summary>
    /// Robidoux sharp.
    /// </summary>
    RobidouxSharp,

    /// <summary>
    /// Cosine.
    /// </summary>
    Cosine,

    /// <summary>
    /// Spline.
    /// </summary>
    Spline,

    /// <summary>
    /// Lanczos radius.
    /// </summary>
    LanczosRadius,

    /// <summary>
    /// Cubic spline.
    /// </summary>
    CubicSpline,
}
