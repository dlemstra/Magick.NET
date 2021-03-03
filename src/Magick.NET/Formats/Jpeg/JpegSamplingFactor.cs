// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies the sampling factor.
    /// </summary>
    public enum JpegSamplingFactor
    {
        /// <summary>
        /// 4:4:4.
        /// </summary>
        Ratio444,

        /// <summary>
        /// 4:2:2.
        /// </summary>
        Ratio422,

        /// <summary>
        /// 4:1:1.
        /// </summary>
        Ratio411,

        /// <summary>
        /// 4:4:0.
        /// </summary>
        Ratio440,

        /// <summary>
        /// 4:2:0.
        /// </summary>
        Ratio420,

        /// <summary>
        /// 4:1:0.
        /// </summary>
        Ratio410,
    }
}