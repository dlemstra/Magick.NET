// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for quantize operations.
/// </summary>
public interface IQuantizeSettings
{
    /// <summary>
    /// Gets or sets the maximum number of colors to quantize to.
    /// </summary>
    uint Colors { get; set; }

    /// <summary>
    /// Gets or sets the colorspace to quantize in.
    /// </summary>
    ColorSpace ColorSpace { get; set; }

    /// <summary>
    /// Gets or sets the dither method to use.
    /// </summary>
    DitherMethod? DitherMethod { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether errors should be measured.
    /// </summary>
    bool MeasureErrors { get; set; }

    /// <summary>
    /// Gets or sets the quantization tree-depth.
    /// </summary>
    uint TreeDepth { get; set; }
}
