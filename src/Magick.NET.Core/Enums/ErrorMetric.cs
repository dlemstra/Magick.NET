// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the error metric types.
/// </summary>
public enum ErrorMetric
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Absolute.
    /// </summary>
    Absolute,

    /// <summary>
    /// Fuzz.
    /// </summary>
    Fuzz,

    /// <summary>
    /// Mean absolute.
    /// </summary>
    MeanAbsolute,

    /// <summary>
    /// Mean error per pixel.
    /// </summary>
    MeanErrorPerPixel,

    /// <summary>
    /// Mean squared.
    /// </summary>
    MeanSquared,

    /// <summary>
    /// Normalized cross correlation.
    /// </summary>
    NormalizedCrossCorrelation,

    /// <summary>
    /// Peak absolute.
    /// </summary>
    PeakAbsolute,

    /// <summary>
    /// Peak signal to noise ratio.
    /// </summary>
    PeakSignalToNoiseRatio,

    /// <summary>
    /// Perceptual hash.
    /// </summary>
    PerceptualHash,

    /// <summary>
    /// Root mean squared.
    /// </summary>
    RootMeanSquared,

    /// <summary>
    /// Structural similarity.
    /// </summary>
    StructuralSimilarity,

    /// <summary>
    /// Structural dissimilarity.
    /// </summary>
    StructuralDissimilarity,
}
