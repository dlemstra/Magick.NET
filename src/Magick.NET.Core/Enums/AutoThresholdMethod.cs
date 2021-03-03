// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Specifies the auto threshold methods.
    /// </summary>
    public enum AutoThresholdMethod
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Kapur.
        /// </summary>
        Kapur,

        /// <summary>
        /// OTSU.
        /// </summary>
        OTSU,

        /// <summary>
        /// Triangle.
        /// </summary>
        Triangle,
    }
}
