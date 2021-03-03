// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Specifies dither methods.
    /// </summary>
    public enum DitherMethod
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// No.
        /// </summary>
        No,

        /// <summary>
        /// Riemersma.
        /// </summary>
        Riemersma,

        /// <summary>
        /// FloydSteinberg.
        /// </summary>
        FloydSteinberg,
    }
}