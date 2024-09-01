// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specified the type of noise that should be added to the image.
/// </summary>
public enum NoiseType
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Uniform.
    /// </summary>
    Uniform,

    /// <summary>
    /// Gaussian.
    /// </summary>
    Gaussian,

    /// <summary>
    /// MultiplicativeGaussian.
    /// </summary>
    MultiplicativeGaussian,

    /// <summary>
    /// Impulse.
    /// </summary>
    Impulse,

    /// <summary>
    /// Laplacian.
    /// </summary>
    Laplacian,

    /// <summary>
    /// Poisson.
    /// </summary>
    Poisson,

    /// <summary>
    /// Random.
    /// </summary>
    Random,
}
