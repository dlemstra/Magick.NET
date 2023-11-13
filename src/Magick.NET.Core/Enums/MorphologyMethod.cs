// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the morphology methods.
/// </summary>
public enum MorphologyMethod
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Convolve.
    /// </summary>
    Convolve,

    /// <summary>
    /// Correlate.
    /// </summary>
    Correlate,

    /// <summary>
    /// Erode.
    /// </summary>
    Erode,

    /// <summary>
    /// Dilate.
    /// </summary>
    Dilate,

    /// <summary>
    /// Erode intensity.
    /// </summary>
    ErodeIntensity,

    /// <summary>
    /// Dilate intensity.
    /// </summary>
    DilateIntensity,

    /// <summary>
    /// Iterative distance.
    /// </summary>
    IterativeDistance,

    /// <summary>
    /// Open.
    /// </summary>
    Open,

    /// <summary>
    /// Close.
    /// </summary>
    Close,

    /// <summary>
    /// Open intensity.
    /// </summary>
    OpenIntensity,

    /// <summary>
    /// Close intensity.
    /// </summary>
    CloseIntensity,

    /// <summary>
    /// Smooth.
    /// </summary>
    Smooth,

    /// <summary>
    /// Edge in.
    /// </summary>
    EdgeIn,

    /// <summary>
    /// Edge out.
    /// </summary>
    EdgeOut,

    /// <summary>
    /// Edge.
    /// </summary>
    Edge,

    /// <summary>
    /// Top hat.
    /// </summary>
    TopHat,

    /// <summary>
    /// Bottom hat.
    /// </summary>
    BottomHat,

    /// <summary>
    /// Hit and miss.
    /// </summary>
    HitAndMiss,

    /// <summary>
    /// Thinning.
    /// </summary>
    Thinning,

    /// <summary>
    /// Thicken.
    /// </summary>
    Thicken,

    /// <summary>
    /// Distance.
    /// </summary>
    Distance,

    /// <summary>
    /// Voronoi.
    /// </summary>
    Voronoi,
}
