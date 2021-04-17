// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies a limited set of the available dds compression methods.
    /// </summary>
    public enum DdsCompression
    {
        /// <summary>
        /// Do not compress the pixels.
        /// </summary>
        None,

        /// <summary>
        /// Use Dxt1 instead of the default compression.
        /// </summary>
        Dxt1,
    }
}