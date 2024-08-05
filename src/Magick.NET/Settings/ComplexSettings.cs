// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for the complex operation.
/// </summary>
public sealed class ComplexSettings : IComplexSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComplexSettings"/> class.
    /// </summary>
    /// <param name="complexOperator">The complex operator.</param>
    public ComplexSettings(ComplexOperator complexOperator)
    {
        ComplexOperator = complexOperator;
    }

    /// <summary>
    /// Gets the complex operator.
    /// </summary>
    public ComplexOperator ComplexOperator { get; }

    /// <summary>
    /// Gets or sets the signal to noise ratio.
    /// </summary>
    public double? SignalToNoiseRatio { get; set; }
}
