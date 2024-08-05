// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for the complex operation.
/// </summary>
public interface IComplexSettings
{
    /// <summary>
    /// Gets the complex operator.
    /// </summary>
    ComplexOperator ComplexOperator { get; }

    /// <summary>
    /// Gets or sets the signal to noise ratio.
    /// </summary>
    double? SignalToNoiseRatio { get; set; }
}
