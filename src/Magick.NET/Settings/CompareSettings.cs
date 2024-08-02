// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <summary>
/// Class that contains setting for the compare operations.
/// </summary>
public sealed class CompareSettings : ICompareSettings<QuantumType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompareSettings"/> class.
    /// </summary>
    /// <param name="metric">The error metric to use.</param>
    public CompareSettings(ErrorMetric metric)
        => Metric = metric;

    /// <summary>
    /// Gets the error metric to use.
    /// </summary>
    public ErrorMetric Metric { get; }

    /// <summary>
    /// Gets or sets the color that emphasize pixel differences.
    /// </summary>
    public IMagickColor<QuantumType>? HighlightColor { get; set; }

    /// <summary>
    /// Gets or sets the color that de-emphasize pixel differences.
    /// </summary>
    public IMagickColor<QuantumType>? LowlightColor { get; set; }

    /// <summary>
    /// Gets or sets the color of pixels that are inside the read mask.
    /// </summary>
    public IMagickColor<QuantumType>? MasklightColor { get; set; }
}
