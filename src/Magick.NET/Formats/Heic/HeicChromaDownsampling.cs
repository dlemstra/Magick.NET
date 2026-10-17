// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats;

/// <summary>
/// Specifies heic chroma downsampling.
/// </summary>
public enum HeicChromaDownsampling
{
    /// <summary>
    /// Average.
    /// </summary>
    Average,

    /// <summary>
    /// Nearest neighbor.
    /// </summary>
    NearestNeighbor,

    /// <summary>
    /// Sharp YUV.
    /// </summary>
    SharpYUV,
}
