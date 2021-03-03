// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Defines the dng output colors.
    /// </summary>
    public enum DngOutputColor
    {
        /// <summary>
        /// Raw color (unique to each camera).
        /// </summary>
        Raw = 0,

        /// <summary>
        /// sRGB D65 (default).
        /// </summary>
        SRGB = 1,

        /// <summary>
        /// Adobe RGB (1998) D65.
        /// </summary>
        AdobeRGB = 2,

        /// <summary>
        /// Wide Gamut RGB D65.
        /// </summary>
        WideGamutRGB = 3,

        /// <summary>
        /// Kodak ProPhoto RGB D65.
        /// </summary>
        KodakProPhotoRGB = 4,

        /// <summary>
        /// XYZ.
        /// </summary>
        XYZ,
    }
}
