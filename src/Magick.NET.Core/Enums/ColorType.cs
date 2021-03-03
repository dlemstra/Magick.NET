// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
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
        /// GrayscaleAlpha.
        /// </summary>
        GrayscaleAlpha,

        /// <summary>
        /// Palette.
        /// </summary>
        Palette,

        /// <summary>
        /// PaletteAlpha.
        /// </summary>
        PaletteAlpha,

        /// <summary>
        /// TrueColor.
        /// </summary>
        TrueColor,

        /// <summary>
        /// TrueColorAlpha.
        /// </summary>
        TrueColorAlpha,

        /// <summary>
        /// ColorSeparation.
        /// </summary>
        ColorSeparation,

        /// <summary>
        /// ColorSeparationAlpha.
        /// </summary>
        ColorSeparationAlpha,

        /// <summary>
        /// Optimize.
        /// </summary>
        Optimize,

        /// <summary>
        /// PaletteBilevelAlpha.
        /// </summary>
        PaletteBilevelAlpha,
    }
}