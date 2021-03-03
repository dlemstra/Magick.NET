// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
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
        /// AffineProjection.
        /// </summary>
        AffineProjection,

        /// <summary>
        /// ScaleRotateTranslate.
        /// </summary>
        ScaleRotateTranslate,

        /// <summary>
        /// Perspective.
        /// </summary>
        Perspective,

        /// <summary>
        /// PerspectiveProjection.
        /// </summary>
        PerspectiveProjection,

        /// <summary>
        /// BilinearForward.
        /// </summary>
        BilinearForward,

        /// <summary>
        /// BilinearReverse.
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
        /// DePolar.
        /// </summary>
        DePolar,

        /// <summary>
        /// Cylinder2Plane.
        /// </summary>
        Cylinder2Plane,

        /// <summary>
        /// Plane2Cylinder.
        /// </summary>
        Plane2Cylinder,

        /// <summary>
        /// Barrel.
        /// </summary>
        Barrel,

        /// <summary>
        /// BarrelInverse.
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
        /// RigidAffine.
        /// </summary>
        RigidAffine,
    }
}