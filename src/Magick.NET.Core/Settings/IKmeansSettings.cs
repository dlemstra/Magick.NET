// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for the kmeans operation.
/// </summary>
public interface IKmeansSettings
{
    /// <summary>
    /// Gets or sets the seed clusters from color list (e.g. red;green;blue).
    /// </summary>
    string? SeedColors { get; set; }

    /// <summary>
    /// Gets or sets the number of colors to use as seeds.
    /// </summary>
    uint NumberColors { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of iterations while converging.
    /// </summary>
    uint MaxIterations { get; set; }

    /// <summary>
    /// Gets or sets the maximum tolerance.
    /// </summary>
    double Tolerance { get; set; }
}
