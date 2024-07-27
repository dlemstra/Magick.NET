// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for the kmeans operation.
/// </summary>
public sealed class KmeansSettings : IKmeansSettings
{
    /// <summary>
    /// Gets or sets the seed clusters from color list (e.g. red;green;blue).
    /// </summary>
    public string? SeedColors { get; set; }

    /// <summary>
    /// Gets or sets the number of colors to use as seeds.
    /// </summary>
    public uint NumberColors { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of iterations while converging.
    /// </summary>
    public uint MaxIterations { get; set; } = 100;

    /// <summary>
    /// Gets or sets the maximum tolerance.
    /// </summary>
    public double Tolerance { get; set; } = 0.01;
}
