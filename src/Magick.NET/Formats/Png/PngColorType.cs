// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats;

/// <summary>
/// Specifies the chunks to be included or excluded in the PNG image.
/// This is a flags enumeration, allowing a bitwise combination of its member values.
/// </summary>
internal enum PngColorType
{
    /// <summary>
    /// Grayscale color type.
    /// </summary>
    Grayscale = 0,

    /// <summary>
    /// RGB color type.
    /// </summary>
    RGB = 2,

    /// <summary>
    /// Indexed color type.
    /// </summary>
    Indexed = 3,

    /// <summary>
    /// Grayscale with alpha color type.
    /// </summary>
    GrayMatte = 4,

    /// <summary>
    /// 
    RGBMatte = 6,
}

