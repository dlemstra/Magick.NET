// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats;

/// <summary>
/// Specifies the PNG compression filter.
/// </summary>
public enum PngCompressionFilter
{
    /// <summary>
    /// 0 - None: No filter.
    /// </summary>
    None,

    /// <summary>
    /// Sub: Subtracts the value of the pixel to the left.
    /// </summary>
    Sub,

    /// <summary>
    /// Up: Subtracts the value of the pixel above.
    /// </summary>
    Up,

    /// <summary>
    /// Average: Uses the average of the left and above pixels.
    /// </summary>
    Average,

    /// <summary>
    /// Paeth: A predictive filter using the Paeth algorithm.
    /// </summary>
    Paeth,
}
