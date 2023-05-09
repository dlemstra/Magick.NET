// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.Formats;

/// <summary>
/// Specifies jpeg tables mode that will be used when using jpeg compression in a tiff image.
/// </summary>
[Flags]
public enum TiffJpegTablesMode
{
    /// <summary>
    /// Unspecified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Include quantization tables.
    /// </summary>
    Quant = 1,

    /// <summary>
    /// Include Huffman tables.
    /// </summary>
    Huff = 2,
}
