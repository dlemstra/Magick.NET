// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Interface that represents the quantum information of ImageMagick.
/// </summary>
public interface IQuantum
{
    /// <summary>
    /// Gets the quantum depth.
    /// </summary>
    uint Depth { get; }
}
