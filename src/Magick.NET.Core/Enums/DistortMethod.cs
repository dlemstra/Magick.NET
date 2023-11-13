// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies distortion methods.
/// </summary>
public enum DistortMethod
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Affine.
    /// </summary>
    Affine,

    /// <summary>
    /// Affine projection.
    /// </summary>
    AffineProjection,

    /// <summary>
    /// Scale rotate translate.
    /// </summary>
    ScaleRotateTranslate,

    /// <summary>
    /// Perspective.
    /// </summary>
    Perspective,

    /// <summary>
    /// Perspective projection.
    /// </summary>
    PerspectiveProjection,

    /// <summary>
    /// Bilinear forward.
    /// </summary>
    BilinearForward,

    /// <summary>
    /// Bilinear reverse.
    /// </summary>
    BilinearReverse,

    /// <summary>
    /// Polynomial.
    /// </summary>
    Polynomial,

    /// <summary>
    /// Arc.
    /// </summary>
    Arc,

    /// <summary>
    /// Polar.
    /// </summary>
    Polar,

    /// <summary>
    /// De-polar.
    /// </summary>
    DePolar,

    /// <summary>
    /// Cylinder 2 plane.
    /// </summary>
    Cylinder2Plane,

    /// <summary>
    /// Plane 2 cylinder.
    /// </summary>
    Plane2Cylinder,

    /// <summary>
    /// Barrel.
    /// </summary>
    Barrel,

    /// <summary>
    /// Barrel inverse.
    /// </summary>
    BarrelInverse,

    /// <summary>
    /// Shepards.
    /// </summary>
    Shepards,

    /// <summary>
    /// Resize.
    /// </summary>
    Resize,

    /// <summary>
    /// Sentinel.
    /// </summary>
    Sentinel,

    /// <summary>
    /// Rigid affine.
    /// </summary>
    RigidAffine,
}
