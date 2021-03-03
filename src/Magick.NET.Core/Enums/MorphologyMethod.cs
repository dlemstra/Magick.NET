// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
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
        /// ErodeIntensity.
        /// </summary>
        ErodeIntensity,

        /// <summary>
        /// DilateIntensity.
        /// </summary>
        DilateIntensity,

        /// <summary>
        /// IterativeDistance.
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
        /// OpenIntensity.
        /// </summary>
        OpenIntensity,

        /// <summary>
        /// CloseIntensity.
        /// </summary>
        CloseIntensity,

        /// <summary>
        /// Smooth.
        /// </summary>
        Smooth,

        /// <summary>
        /// EdgeIn.
        /// </summary>
        EdgeIn,

        /// <summary>
        /// EdgeOut.
        /// </summary>
        EdgeOut,

        /// <summary>
        /// Edge.
        /// </summary>
        Edge,

        /// <summary>
        /// TopHat.
        /// </summary>
        TopHat,

        /// <summary>
        /// BottomHat.
        /// </summary>
        BottomHat,

        /// <summary>
        /// HitAndMiss.
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
}