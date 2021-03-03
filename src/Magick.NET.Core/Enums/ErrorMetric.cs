// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
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
        /// MeanAbsolute.
        /// </summary>
        MeanAbsolute,

        /// <summary>
        /// MeanErrorPerPixel.
        /// </summary>
        MeanErrorPerPixel,

        /// <summary>
        /// MeanSquared.
        /// </summary>
        MeanSquared,

        /// <summary>
        /// NormalizedCrossCorrelation.
        /// </summary>
        NormalizedCrossCorrelation,

        /// <summary>
        /// PeakAbsolute.
        /// </summary>
        PeakAbsolute,

        /// <summary>
        /// PeakSignalToNoiseRatio.
        /// </summary>
        PeakSignalToNoiseRatio,

        /// <summary>
        /// PerceptualHash.
        /// </summary>
        PerceptualHash,

        /// <summary>
        /// RootMeanSquared.
        /// </summary>
        RootMeanSquared,

        /// <summary>
        /// StructuralSimilarity.
        /// </summary>
        StructuralSimilarity,

        /// <summary>
        /// StructuralDissimilarity.
        /// </summary>
        StructuralDissimilarity,
    }
}