// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for the compare operations.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface ICompareSettings<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets or sets the error metric to use.
        /// </summary>
        ErrorMetric Metric { get; set; }

        /// <summary>
        /// Gets or sets the color that emphasize pixel differences.
        /// </summary>
        IMagickColor<TQuantumType>? HighlightColor { get; set; }

        /// <summary>
        /// Gets or sets the color that de-emphasize pixel differences.
        /// </summary>
        IMagickColor<TQuantumType>? LowlightColor { get; set; }

        /// <summary>
        /// Gets or sets the color of pixels that are inside the read mask.
        /// </summary>
        IMagickColor<TQuantumType>? MasklightColor { get; set; }
    }
}
