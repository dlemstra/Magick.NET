// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies WebP preprocessing options.
    /// </summary>
    public enum WebPPreprocessing
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// SegmentSmooth.
        /// </summary>
        SegmentSmooth = 1,

        /// <summary>
        /// PseudoRandom.
        /// </summary>
        PseudoRandom = 2,
    }
}
