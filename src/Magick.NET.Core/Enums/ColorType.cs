// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the color type of the image.
/// </summary>
public enum ColorType
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Bilevel.
    /// </summary>
    Bilevel,

    /// <summary>
    /// Grayscale.
    /// </summary>
    Grayscale,

    /// <summary>
    /// Grayscale alpha.
    /// </summary>
    GrayscaleAlpha,

    /// <summary>
    /// Palette.
    /// </summary>
    Palette,

    /// <summary>
    /// Palette alpha.
    /// </summary>
    PaletteAlpha,

    /// <summary>
    /// TrueColor.
    /// </summary>
    TrueColor,

    /// <summary>
    /// TrueColor alpha.
    /// </summary>
    TrueColorAlpha,

    /// <summary>
    /// Color separation.
    /// </summary>
    ColorSeparation,

    /// <summary>
    /// Color separation alpha.
    /// </summary>
    ColorSeparationAlpha,

    /// <summary>
    /// Optimize.
    /// </summary>
    Optimize,

    /// <summary>
    /// Palette bilevel alpha.
    /// </summary>
    PaletteBilevelAlpha,
}
