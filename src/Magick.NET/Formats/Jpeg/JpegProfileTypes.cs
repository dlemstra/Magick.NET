// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies profile types.
    /// </summary>
    [Flags]
    public enum JpegProfileTypes
    {
        /// <summary>
        /// App profile.
        /// </summary>
        App = 1,

        /// <summary>
        /// 8bim profile.
        /// </summary>
        EightBim = 2,

        /// <summary>
        /// Exif profile.
        /// </summary>
        Exif = 4,

        /// <summary>
        /// Icc profile.
        /// </summary>
        Icc = 8,

        /// <summary>
        /// Iptc profile.
        /// </summary>
        Iptc = 16,

        /// <summary>
        /// Xmp profile.
        /// </summary>
        Xmp = 32,
    }
}