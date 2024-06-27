// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats;

/// <summary>
/// Specifies the PNG compression strategy.
/// </summary>
public enum PngCompressionStrategy
{
    /// <summary>
    /// Use the Huffman compression.
    /// </summary>
    HuffmanOnly,

    /// <summary>
    /// Compression algorithm with filtering.
    /// </summary>
    Filtered,

    /// <summary>
    /// Use the Run-Length Encoding compression.
    /// </summary>
    RLE,

    /// <summary>
    /// Use a fixed strategy for compression.
    /// </summary>
    Fixed,

    /// <summary>
    /// Use the default compression strategy.
    /// </summary>
    Default,

    /// <summary>
    /// Adaptive filtering is used when quality is greater than 50 and the image does not have a color map; otherwise, no filtering is used.
    /// </summary>
    Adaptive,

    /// <summary>
    /// Adaptive filtering with minimum-sum-of-absolute-values is used.
    /// </summary>
    AdaptiveMinimumSum,

    /// <summary>
    /// LOCO color transformation (intrapixel differencing) and adaptive filtering with minimum-sum-of-absolute-values are used. Only applicable if the output is MNG.
    /// </summary>
    LOCO,

    /// <summary>
    /// The zlib Z_RLE compression strategy (or the Z_HUFFMAN_ONLY strategy when compression level is 0) is used with adaptive PNG filtering.
    /// </summary>
    ZRLEAdaptive,

    /// <summary>
    /// The zlib Z_RLE compression strategy (or the Z_HUFFMAN_ONLY strategy when compression level is 0) is used with no PNG filtering.
    /// </summary>
    ZRLENoFilter,
}
