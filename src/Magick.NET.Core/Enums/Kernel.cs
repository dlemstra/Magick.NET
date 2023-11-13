// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the built-in kernels.
/// </summary>
public enum Kernel
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Unity.
    /// </summary>
    Unity,

    /// <summary>
    /// Gaussian.
    /// </summary>
    Gaussian,

    /// <summary>
    /// DoG.
    /// </summary>
    DoG,

    /// <summary>
    /// LoG.
    /// </summary>
    LoG,

    /// <summary>
    /// Blur.
    /// </summary>
    Blur,

    /// <summary>
    /// Comet.
    /// </summary>
    Comet,

    /// <summary>
    /// Binomial.
    /// </summary>
    Binomial,

    /// <summary>
    /// Laplacian.
    /// </summary>
    Laplacian,

    /// <summary>
    /// Sobel.
    /// </summary>
    Sobel,

    /// <summary>
    /// Frei chen.
    /// </summary>
    FreiChen,

    /// <summary>
    /// Roberts.
    /// </summary>
    Roberts,

    /// <summary>
    /// Prewitt.
    /// </summary>
    Prewitt,

    /// <summary>
    /// Compass.
    /// </summary>
    Compass,

    /// <summary>
    /// Kirsch.
    /// </summary>
    Kirsch,

    /// <summary>
    /// Diamond.
    /// </summary>
    Diamond,

    /// <summary>
    /// Square.
    /// </summary>
    Square,

    /// <summary>
    /// Rectangle.
    /// </summary>
    Rectangle,

    /// <summary>
    /// Octagon.
    /// </summary>
    Octagon,

    /// <summary>
    /// Disk.
    /// </summary>
    Disk,

    /// <summary>
    /// Plus.
    /// </summary>
    Plus,

    /// <summary>
    /// Cross.
    /// </summary>
    Cross,

    /// <summary>
    /// Ring.
    /// </summary>
    Ring,

    /// <summary>
    /// Peaks.
    /// </summary>
    Peaks,

    /// <summary>
    /// Edges.
    /// </summary>
    Edges,

    /// <summary>
    /// Corners.
    /// </summary>
    Corners,

    /// <summary>
    /// Diagonals.
    /// </summary>
    Diagonals,

    /// <summary>
    /// Line ends.
    /// </summary>
    LineEnds,

    /// <summary>
    /// Line junctions.
    /// </summary>
    LineJunctions,

    /// <summary>
    /// Ridges.
    /// </summary>
    Ridges,

    /// <summary>
    /// Convex hull.
    /// </summary>
    ConvexHull,

    /// <summary>
    /// Thin SE.
    /// </summary>
    ThinSE,

    /// <summary>
    /// Skeleton.
    /// </summary>
    Skeleton,

    /// <summary>
    /// Chebyshev.
    /// </summary>
    Chebyshev,

    /// <summary>
    /// Manhattan.
    /// </summary>
    Manhattan,

    /// <summary>
    /// Octagonal.
    /// </summary>
    Octagonal,

    /// <summary>
    /// Euclidean.
    /// </summary>
    Euclidean,

    /// <summary>
    /// User defined.
    /// </summary>
    UserDefined,
}
