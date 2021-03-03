// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// The sparse color methods.
    /// </summary>
    public enum SparseColorMethod
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Barycentric.
        /// </summary>
        Barycentric = DistortMethod.Affine,

        /// <summary>
        /// Bilinear.
        /// </summary>
        Bilinear = DistortMethod.BilinearReverse,

        /// <summary>
        /// Polynomial.
        /// </summary>
        Polynomial = DistortMethod.Polynomial,

        /// <summary>
        /// Shepards.
        /// </summary>
        Shepards = DistortMethod.Shepards,

        /// <summary>
        /// Voronoi.
        /// </summary>
        Voronoi = DistortMethod.Sentinel,

        /// <summary>
        /// Inverse.
        /// </summary>
        Inverse = 19,

        /// <summary>
        /// Manhattan.
        /// </summary>
        Manhattan = 20,
    }
}