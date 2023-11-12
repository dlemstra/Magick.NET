// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Chromaticity information.
/// </summary>
public interface IChromaticityInfo
{
    /// <summary>
    /// Gets the chromaticity blue primary point.
    /// </summary>
    IPrimaryInfo Blue { get; }

    /// <summary>
    /// Gets the chromaticity green primary point.
    /// </summary>
    IPrimaryInfo Green { get; }

    /// <summary>
    /// Gets the chromaticity red primary point.
    /// </summary>
    IPrimaryInfo Red { get; }

    /// <summary>
    /// Gets the chromaticity white primary point.
    /// </summary>
    IPrimaryInfo White { get; }
}
